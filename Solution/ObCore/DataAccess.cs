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
	/// you care about ORM-lite functionality.
	/// 
	/// These functions are NOT SQL injection safe. 
	/// Potentially hostile input should be sanitized before sending it here (=
	/// </summary>
	public class DataAccess {
		// Defaults to the last connection string. If there are no connection strings, defaults to String.Empty
		public  string ConnectionString = (ConfigurationManager.ConnectionStrings.Count>0) ? ConfigurationManager.ConnectionStrings[ConfigurationManager.ConnectionStrings.Count-1].ConnectionString : string.Empty;
		public SqlConnection Connection;

		public  SqlConnection GetConnection() {
			if (String.IsNullOrEmpty(ConnectionString)) 
				throw new ConfigurationErrorsException("Couldn't find any connection strings in your configuration file, and you didn't set DataAccess.ConnectionString manually");

			Connection = Connection ?? new SqlConnection(ConnectionString);
			if ((Connection.State != ConnectionState.Open) && (Connection.State!=ConnectionState.Connecting)) Connection.Open();
			return Connection;
		}



		//todo: handle non-int primary keys
		//todo: handle no rows returned
		public  DataRow GetUpdateableRow(string tableName, string primaryKeyColumnName, int primaryKeyValue) {
			Trace.Write(string.Format("{0} {1}", tableName, primaryKeyValue), "DataAccess.GetUpdateableRow");
			DataTable dt =
					GetDataTable(String.Format("select * from {0} where {1}={2}", tableName, primaryKeyColumnName, primaryKeyValue));
			return dt.Rows[0];
		}

		public  void ExecuteNonQuery(string cmd) {
			Trace.Write(cmd, "ExecuteNonQuery(string)");
			GetCommand(cmd).ExecuteNonQuery();
			Trace.Write("...done", "ExecuteNonQuery");
		}

		public  DataSet GetDataSet(string cmd) {
			SqlConnection conn;
			var ds = new DataSet();
			using (SqlDataAdapter da = GetDataAdapter(cmd, out conn)) {
				da.Fill(ds);
			}
			return ds;
		}

		public  DataRow GetDataRow(SqlCommand cmd) {
			var dt = new DataTable();
			cmd.Connection = GetConnection();
			var da = new SqlDataAdapter(cmd);
			da.Fill(dt);
			return dt.Rows.Count > 0 ? dt.Rows[0] : null;
		}

		public  DataRow GetDataRow(string cmd) {
			return GetDataRow(GetCommand(cmd));
		}

		public  DataTable GetDataTable(SqlCommand cmd) {
			var dt = new DataTable();
			cmd.Connection = GetConnection();
			var da = new SqlDataAdapter(cmd);
			da.Fill(dt);
			return dt;
		}

		public  DataTable GetDataTable(string cmd) {
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
		public  SqlDataAdapter GetDataAdapter(string cmd, out SqlConnection conn) {
			Trace.Write(cmd, "DataAccess.GetDataAdapter");
			conn = GetConnection();
			return new SqlDataAdapter(cmd, conn);
		}

		public  SqlCommand GetCommand() {
			return GetCommand(GetConnection());
		}

		public  SqlCommand GetCommand(SqlConnection conn) {
			using (var myCommand = new SqlCommand()) {
				myCommand.Connection = conn;
				return myCommand;
			}
		}

		public  SqlCommand GetCommand(string cmd) {
			Trace.Write(cmd, "DataAccess.GetCommand");
			return new SqlCommand(cmd, GetConnection());
		}

		public  SqlCommand GetCommandStoredProcedure(string storedProcedureName) {
			return GetStoredProcedureCommand(storedProcedureName, GetConnection());
		}

		public  SqlCommand GetStoredProcedureCommand(string storedProcedureName, SqlConnection conn) {
			SqlCommand myCommand = GetCommand(conn);
			myCommand.CommandType = CommandType.StoredProcedure;
			myCommand.CommandText = storedProcedureName;
			return myCommand;
		}

		public  int GetScalarInt(string cmd) {
			SqlCommand foo = GetCommand(cmd);
			return GetScalarInt(foo);
		}

		public int GetScalarInt(SqlCommand cmd) {
			object result = cmd.ExecuteScalar();
			return (int)result;
		}

		public int? GetScalarIntNullable(string sql) {
			SqlCommand cmd = GetCommand(sql);
			object result = cmd.ExecuteScalar();
			if (result == DBNull.Value) return null;
			return (int) result;
		}


		public int? GetScalarIntNullable(SqlCommand cmd) {
			object result = cmd.ExecuteScalar();
			if (result == DBNull.Value) return null;
			return (int)result;
		}

		public  string GetScalarString(string cmd) {
			SqlCommand foo = GetCommand(cmd);
			var result = foo.ExecuteScalar();
			return result == null ? string.Empty : result.ToString();
		}

		public  DateTime GetScalarDateTime(string cmd) {
			SqlCommand foo = GetCommand(cmd);
			return (DateTime)foo.ExecuteScalar();
		}

		public  object GetScalar(SqlCommand cmd) {
			return cmd.ExecuteScalar();
		}

		public  SqlDataReader GetDataReader(string cmd) {
			Trace.Write(cmd, "DataAccess.GetDataReader");
			SqlCommand sqlCommand = GetCommand(cmd);
			SqlDataReader dr;
			try {
				Trace.Write(String.Format("Start execute: {0}", cmd), "DataAccess.GetDataReader");
				dr = sqlCommand.ExecuteReader();
				Trace.Write(String.Format("Start execute: {0}", cmd), "DataAccess.GetDataReader");
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

		public  SqlDataReader GetDataReader(SqlCommand cmd) {
			SqlDataReader reader;
			try {
				Trace.Write(String.Format("Start execute: {0}", cmd.CommandText), "DataAccess.GetDataReader");
				reader = cmd.ExecuteReader();
				reader.Read(); //????? not like GetDataReader(string)
				Trace.Write(String.Format("Start execute: {0}", cmd.CommandText), "DataAccess.GetDataReader");
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

	}

}