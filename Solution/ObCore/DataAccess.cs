using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ObCore {
	/// <summary>
	/// Convenience extension methods for DataTables and other objects
	/// </summary>
	public static class DataExtensions {
		public static string GetString(this DataRow dr, string columnName) {
			return GetString(dr, columnName, null);
		}

		public static string GetString(this DataRow dr, string columnName, string defaultValue) {
			if (dr.Table.Columns.Contains(columnName)) return dr[columnName].ToString();
			return defaultValue;
		}

		

		public static int? GetNullableInt(this DataRow dr, string columnName) {
			try {
				return Convert.ToInt32(dr[columnName]);
			}
			catch (Exception) {
				return null;
			}
		}

		public static bool? GetNullableBool(this DataRow dr, string columnName) {
			try {
				return Convert.ToBoolean(dr[columnName]);
			}
			catch (Exception) {
				return null;
			}
		}

		public static DateTime? GetDateTime(this DataRow dr, string columnName) {
			try {
				return (DateTime) dr[columnName];
			}
			catch (Exception) {
				return null;
			}
		}

		public static bool GetBool(this DataRow dr, string columnName, bool defaultValue) {
			try {
				return Convert.ToBoolean(dr[columnName]);
			}
			catch (Exception) {
				return defaultValue;
			}
		}

		public static SqlCommand AsText(this SqlCommand cmd) {
			cmd.CommandType=CommandType.Text;
			return cmd;
		}

		public static SqlCommand AsStoredProcedure(this SqlCommand cmd) {
			cmd.CommandType=CommandType.StoredProcedure;
			return cmd;
		}
	}


	/// <summary>
	/// Convenience functions to wrap common ADO.NET stuff
	/// This is entirely unrelated to the PetaPoco stuff
	/// Use THIS class when you just want to do straight database stuff; use PetaPoco when
	/// you care about ORM-lite functionality.
	/// 
	/// These functions are NOT SQL injection safe. 
	/// Potentially hostile input should be sanitized before sending it here (=
	/// (Protip: Use parameterized queries!)
	/// </summary>
	public class DataAccess {

		// Defaults to the last connection string. If there are no connection strings, defaults to String.Empty
		// public  string ConnectionString = (ConfigurationManager.ConnectionStrings.Count > 0) ? ConfigurationManager.ConnectionStrings[ConfigurationManager.ConnectionStrings.Count - 1].ConnectionString : string.Empty;
		public string ConnectionString {
			get {
				string result = (ConfigurationManager.ConnectionStrings[Environment.MachineName]
					?? ConfigurationManager.ConnectionStrings[ConfigurationManager.ConnectionStrings.Count - 1]).ConnectionString;
				if (String.IsNullOrEmpty(result))
					throw new ConfigurationErrorsException("Couldn't find any connection strings in your configuration file, and you didn't set DataAccess.ConnectionString manually");
				return result;
			}
		}

		//todo: handle non-int primary keys
		//todo: handle no rows returned
		/*
		public DataRow GetUpdateableRow(string tableName, string primaryKeyColumnName, int primaryKeyValue) {
			Trace.Write(string.Format("{0} {1}", tableName, primaryKeyValue), "DataAccess.GetUpdateableRow");
			DataTable dt =
					GetDataTable(String.Format("select * from {0} where {1}={2}", tableName, primaryKeyColumnName, primaryKeyValue));
			return dt.Rows[0];
		}
		 * */

		public SqlConnection GetConnection() {
			var conn = new SqlConnection(ConnectionString);
			Trace.WriteLine("Opening connection", "DataAccess.cs");
			conn.Open();
			Trace.WriteLine("Opened connection", "DataAccess.cs");
			return conn;
		}

		public SqlCommand GetCommand(string sql) {
			return new SqlCommand(sql);
		}

		public void ExecuteNonQuery(string sql) {
			using (var conn = GetConnection()) {
				using (var cmd = new SqlCommand(sql)) {
					cmd.Connection = conn;
					cmd.CommandType = CommandType.Text;
					cmd.CommandText = sql;
					cmd.ExecuteNonQuery();
				}
			}
		}

		public DataSet GetDataSet(string sql) {
			var ds = new DataSet();
			using (var conn = GetConnection()) {
				Trace.Write("Starting " + sql, "DataAccess#GetDataSet");
				using (var da = new SqlDataAdapter(sql, conn)) {
					da.Fill(ds);
				}
				Trace.Write("Completed " + sql, "DataAccess#GetDataSet");
			}
			return ds;
		}

		public DataSet GetDataSet(SqlCommand cmd) {
			var ds = new DataSet();
			using (var conn = GetConnection()) {
				cmd.Connection = conn;
				Trace.Write("Starting " + cmd.CommandText, "DataAccess#GetDataSet");
				using (var da = new SqlDataAdapter()) {
					da.SelectCommand = cmd;
					da.Fill(ds);
				}
				Trace.Write("Completed " + cmd.CommandText, "DataAccess#GetDataSet");
			}
			return ds;
		}

		public DataRow GetDataRow(SqlCommand cmd) {
			using (var conn = GetConnection()) {
				cmd.Connection = conn;
				using (var da = new SqlDataAdapter()) {
					da.SelectCommand = cmd;
					using (var dt = new DataTable()) {
						da.Fill(dt);
						if (dt.Rows.Count == 0) return null;
						return dt.Rows[0];
					}
				}
			}
		}

		public DataRow GetDataRow(string sql) {
			using (var cmd = new SqlCommand(sql)) {
				cmd.CommandType = CommandType.Text;
				return GetDataRow(cmd);
			}
		}

		public DataTable GetDataTable(SqlCommand cmd) {
			var dt = new DataTable();
			using (var conn = new SqlConnection(ConnectionString)) {
				cmd.Connection = conn;
				conn.Open();
				using (var da = new SqlDataAdapter()) {
					da.SelectCommand = cmd;
					da.Fill(dt);
				}
				conn.Close();
				conn.Dispose();
			}
			return dt;
		}

		public DataTable GetDataTable(string sql) {
			var dt = new DataTable();
			using (var conn = new SqlConnection(ConnectionString)) {
				using (var cmd = new SqlCommand(sql, conn)) {
					cmd.CommandType = CommandType.Text;
					cmd.Connection = conn;
					conn.Open();
					using (var da = new SqlDataAdapter()) {
						da.SelectCommand = cmd;
						da.Fill(dt);
					}
					conn.Close();
					conn.Dispose();
				}
			}
			return dt;
		}

		public int? GetScalarInt(string sql) {
			using (var conn = new SqlConnection(ConnectionString)) {
				using (var cmd = new SqlCommand(sql, conn)) {
					conn.Open();
					cmd.CommandType = CommandType.Text;
					var result = cmd.ExecuteScalar();
					try {
						return Convert.ToInt32(result);
					}
					catch (Exception)
					{
						return null;
					}
				}
			}
		}

		public int? GetScalarInt(SqlCommand cmd) {
			using (var conn = GetConnection()) {
				cmd.Connection = conn;
				var result = cmd.ExecuteScalar();
				try {
					return Convert.ToInt32(result);
				}
				catch (Exception e) {
					return null;
				}
			}
		}

		public string GetScalarString(string sql) {
			using (var conn = GetConnection()) {
				using (var cmd = new SqlCommand(sql, conn)) {
					cmd.CommandType = CommandType.Text;
					var result = cmd.ExecuteScalar();
					try {
						return result.ToString();
					}
					catch (Exception) {
						return String.Empty;
					}
				}
			}
		}

		public string GetScalarString(SqlCommand cmd) {
			using (var conn = GetConnection()) {
				cmd.Connection = conn;
				var result = cmd.ExecuteScalar();
				try {
					return result.ToString();
				}
				catch (Exception) {
					return String.Empty;
				}
			}
		}



		public DateTime? GetScalarDateTime(string sql) {
			using (var conn = new SqlConnection(ConnectionString)) {
				using (var cmd = new SqlCommand(sql, conn)) {
					var result = cmd.ExecuteScalar();
					try {
						return Convert.ToDateTime(result);
					}
					catch (Exception) {
						return null;
					}
				}
			}
		}

		public object GetScalar(SqlCommand cmd) {
			using (var conn = GetConnection()) {
				cmd.Connection = conn;
				return cmd.ExecuteScalar();
			}
		}
	}
}