using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace ObCore {
	/// <summary>
	/// Convenience functions to wrap common ADO.NET stuff
	/// This is entirely unrelated to the PetaPoco stuff
	/// Use THIS class when you just want to do straight database stuff; use PetaPoco when
	/// you care about ORM-lite functionality 
	/// </summary>
	public static class DataAccess {
		public static string ConnectionString = String.Empty;
		public static SqlConnection Connection;

		//todo: handle non-int primary keys
		//todo: handle no rows returned
		public static DataRow GetUpdateableRow(string tableName, string primaryKeyColumnName, int primaryKeyValue) {
			Trace.Write(string.Format("{0} {1}", tableName, primaryKeyValue), "DataAccess.GetUpdateableRow");
			DataTable dt =
					GetDataTable(String.Format("select * from {0} where {1}={2}", tableName, primaryKeyColumnName, primaryKeyValue));
			return dt.Rows[0];
		}

		public static void ExecuteNonQuery(string cmd) {
			Trace.Write(cmd, "ExecuteNonQuery(string)");
			GetCommand(cmd).ExecuteNonQuery();
			Trace.Write("...done", "ExecuteNonQuery");
		}

		public static DataSet GetDataSet(string cmd) {
			SqlConnection conn;
			var ds = new DataSet();
			using (SqlDataAdapter da = GetDataAdapter(cmd, out conn))
				da.Fill(ds);

			return ds;
		}

		public static DataRow GetDataRow(SqlCommand cmd) {
			var dt = new DataTable();
			cmd.Connection = GetConnection();
			var da = new SqlDataAdapter(cmd);
			da.Fill(dt);
			return dt.Rows.Count > 0 ? dt.Rows[0] : null;
		}

		public static DataRow GetDataRow(string cmd) {
			return GetDataRow(GetCommand(cmd));
		}

		public static DataTable GetDataTable(SqlCommand cmd) {
			var dt = new DataTable();
			cmd.Connection = GetConnection();
			var da = new SqlDataAdapter(cmd);
			da.Fill(dt);
			return dt;
		}

		public static DataTable GetDataTable(string cmd) {
			Trace.Write(cmd, "DataAccess.GetDataTable");
			SqlConnection conn;
			var dt = new DataTable();
			SqlDataAdapter da = GetDataAdapter(cmd, out conn);
			da.Fill(dt);
			conn.Close();
			Trace.Write("...done " + cmd, "DataAccess.GetDataTable");

			return dt;
		}

		// todo: if conn is supplied, don't create new connection
		public static SqlDataAdapter GetDataAdapter(string cmd, out SqlConnection conn) {
			Trace.Write(cmd, "DataAccess.GetDataAdapter");
			conn = GetConnection();
			return new SqlDataAdapter(cmd, conn);
		}

		public static SqlCommand GetCommand() {
			return GetCommand(GetConnection());
		}

		public static SqlCommand GetCommand(SqlConnection conn) {
			using (var myCommand = new SqlCommand()) {
				myCommand.Connection = conn;
				return myCommand;
			}
		}

		public static SqlCommand GetCommand(string cmd) {
			Trace.Write(cmd, "DataAccess.GetCommand");
			return new SqlCommand(cmd, GetConnection());
		}

		public static SqlCommand GetCommandStoredProcedure(string storedProcedureName) {
			return GetStoredProcedureCommand(storedProcedureName, GetConnection());
		}

		public static SqlCommand GetStoredProcedureCommand(string storedProcedureName, SqlConnection conn) {
			SqlCommand myCommand = GetCommand(conn);
			myCommand.CommandType = CommandType.StoredProcedure;
			myCommand.CommandText = storedProcedureName;
			return myCommand;
		}

		public static SqlConnection GetConnection() {
			//if (ConnectionString == String.Empty) ConnectionString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
			ConnectionString = ConfigurationManager.ConnectionStrings["epace"].ConnectionString;
			if (Connection == null) Connection = new SqlConnection(ConnectionString);
			if (Connection.State != ConnectionState.Open) Connection.Open();
			return Connection;
		}

		public static int GetScalarInt(string cmd) {
			SqlCommand foo = GetCommand(cmd);
			return GetScalarInt(foo);
			//object result = foo.ExecuteScalar();
			//return (int)result;
		}

		public static int GetScalarInt(SqlCommand cmd) {
			object result = cmd.ExecuteScalar();
			return (int)result;
		}

		public static string GetScalarString(string cmd) {
			SqlCommand foo = GetCommand(cmd);
			var result = foo.ExecuteScalar();
			return result == null ? string.Empty : result.ToString();
		}

		public static DateTime GetScalarDateTime(string cmd) {
			SqlCommand foo = GetCommand(cmd);
			return (DateTime)foo.ExecuteScalar();
		}

		public static object GetScalar(SqlCommand cmd) {
			return cmd.ExecuteScalar();
		}

		public static SqlDataReader GetDataReader(string cmd) {
			Trace.Write(cmd, "DataAccess.GetDataReader");
			SqlCommand sqlCommand = GetCommand(cmd);
			SqlDataReader dr;
			try {
				dr = sqlCommand.ExecuteReader();
			}
			catch (Exception e) {
				sqlCommand.Connection.Close();
				Trace.Write(String.Format("Errored on this sql: {0}", cmd), "GetDataReader");
				throw;
			}
			finally {
				sqlCommand.Dispose();
			}
			Trace.Write("...done " + cmd, "DataAccess.GetDataReader");
			return dr;
		}

		public static SqlDataReader GetDataReader(SqlCommand cmd) {
			Trace.Write(cmd.CommandText, "DataAccess.GetDataReader");
			SqlDataReader reader;
			try {
				reader = cmd.ExecuteReader();
				reader.Read(); //????? not like GetDataReader(string)
			}
			catch (Exception e) {
				cmd.Connection.Close();
				throw;
			}
			finally {
				cmd.Dispose();
			}

			return reader;
		}


		private static string QuoteIfNeeded(this string s) {
			if ((s.Contains("\"")) || (s.Contains(","))) return "\"" + s.Replace("\"", "\"\"") + "\"";
			return s;
		}

		/* This stuff should really be replaced with functions 
		 * that leverage a mature library like CsvHelpers or something
		 * */

		/*
		public static string ToCsv(this DataTable dt) {
			return dt.ToDelimited(",");
		}


		public static string ToDelimited(this DataTable dt, string delimiter) {
			var s = new StringBuilder(String.Empty);

			// write header
			for (int x = 0; x < dt.Columns.Count; x++) {
				if (!dt.Columns[x].ColumnName.Equals("ip_address")) {
					s.Append(dt.Columns[x].ColumnName.QuoteIfNeeded());
					if (x < dt.Columns.Count - 1) s.Append(delimiter);
				}
			}
			s.Append(Environment.NewLine);

			// write rows
			foreach (DataRow dr in dt.Rows) {
				for (int x = 0; x < dt.Columns.Count; x++) {
					if (!dt.Columns[x].ColumnName.Equals("ip_address")) {
						s.Append(dr[x].ToString().QuoteIfNeeded());
						if (x < dt.Columns.Count - 1) s.Append(delimiter);
					}
				}
				s.Append(Environment.NewLine);
			}

			return s.ToString();

		}
		*/
	}

}