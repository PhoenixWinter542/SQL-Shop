using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Shop
{
	public class SQLAdapter : IDisposable
	{
		private readonly bool testing = false;

		private string connectionString = "server=DESKTOP-SV6S892;trusted_connection=Yes";
		private enum SQLType
		{
			TRANSACT
		};
		private readonly SQLType type;
		private SqlConnection conn;
		private SqlTransaction transaction;

		public SQLAdapter(string typeName)
		{
			switch (typeName)
			{
				case "TRANSACT":
					type = SQLType.TRANSACT;
					break;
				default:
					type = SQLType.TRANSACT;
					break;
			}
			conn = new SqlConnection(connectionString);
			conn.Open();
		}

		public SQLAdapter(string typeName, bool testing) : this(typeName)
		{
			this.testing = testing;
			transaction = conn.BeginTransaction();
		}

		public void Dispose()
		{
			if(testing)
				transaction.Rollback();
			else
				transaction.Commit();
			conn.Close();
		}

		public DataTable Read(List<string> selectColNames, List<string> fromTableName, List<string> whereConds, string groupBy, string having, string orderBy)
		{
			switch (type)
			{
				case SQLType.TRANSACT:
					return TransactRead(selectColNames, fromTableName, whereConds, groupBy, having, orderBy);
				default:
					return null;
			}
		}

		public void Commit()
		{
			transaction.Commit();
			transaction = conn.BeginTransaction();
		}

		//Returns number of rows effected
		public int Create(string table, List<(string, string)> data)
		{
			switch (type)
			{
				case SQLType.TRANSACT:
					return TransactCreate(table, data);
				default:
					return -1;
			}
		}

		public int Update(string table, List<string> set, List<string> where)
		{
			switch (type)
			{
				case SQLType.TRANSACT:
					return TransactUpdate(table, set, where);
				default:
					return -1;
			}
		}

		public int Delete(string table, List<string> where)
		{
			switch (type)
			{
				case SQLType.TRANSACT:
					return TransactDelete(table, where);
				default:
					return -1;
			}
		}

		/*
		 * select	- null means select all columns
		 * from		- null not allowed
		 * where	- null skips field, AND/OR must be part of any conds after first
		 * groupBy	- null skips field, having must also be null
		 * having	- null skips field
		 * orderBy	- null skips field
		 */
		private DataTable TransactRead(List<string> selectColNames, List<string> fromTableName, List<string> whereConds, string groupBy, string having, string orderBy)
		{
			//SELECT
			string query = "SELECT";
			if (selectColNames == null)
				query += " *";
			else
				foreach (string colName in selectColNames)
				{
					query += " " + colName;
				}

			//FROM
			query += "\n";
			if (fromTableName == null)
				return null;
			query += "FROM";
			foreach(string tableName in fromTableName)
			{
				query += " " + tableName;
			}

			//WHERE
			if(whereConds != null)
			{
				query += "\n";
				query += "WHERE";
				foreach (string conditions in whereConds)
				{
					query += " " + conditions;
				}
			}

			//GROUP BY
			if (groupBy == null && having != null)
				return null;
			if (groupBy != null)
			{
				query += "\nGROUP BY " + groupBy;
			}

			//HAVING
			if(having != null)
			{
				query += "\nHAVING " + having;
			}

			//ORDER BY
			if(orderBy != null)
			{
				query += "\nORDER BY" + orderBy;
			}
			query += ";";

			try
			{
				SqlDataAdapter da = new SqlDataAdapter(query, conn);
				da.SelectCommand.Transaction = transaction;
				DataSet ds = new DataSet();

				da.Fill(ds);

				return ds.Tables[0];
			}
			catch
			{
				return null;
			}
		}

		private int TransactCreate(string table, List<(string column, string value)> data)
		{
			string query = "INSERT " + table + " (";

			if (null == data)
				return -1;

			query += data[0].column;

			for(int i = 1; i < data.Count; i++)
			{
				query += ", " + data[i].column;
			}

			query += ")\nVALUES (" + data[0].value;

			for(int i = 1; i < data.Count; i++)
			{
				query += ", " + data[i].value;
			}

			query += ");";

			//Console.WriteLine("\n" + query);

			try
			{
				SqlCommand cmd = new SqlCommand(query, conn);
				cmd.Transaction = transaction;

				return cmd.ExecuteNonQuery();
			}
			catch
			{
				return -1;
			}
		}

		private int TransactUpdate(string table, List<string> set, List<string> where)
		{
			string query = "Update " + table + "\n";

			if (null == set)
				return -1;

			query += "SET " + set[0];
			for(int i = 1; i < set.Count; i++)
			{
				query += ", " + set[i];
			}

			if (null != where)
			{
				query += "\n";
				query += "WHERE";
				foreach (string conditions in where)
				{
					query += " " + conditions;
				}
			}
			query += ";";



			try
			{
				SqlCommand cmd = new SqlCommand(query, conn);
				cmd.Transaction = transaction;

				return cmd.ExecuteNonQuery();
			}
			catch
			{
				return -1;
			}
		}

		private int TransactDelete(string table, List<string> where)
		{
			string query = "DELETE " + table;

			if (null != where)
			{
				query += "\n";
				query += "WHERE";
				foreach (string conditions in where)
				{
					query += " " + conditions;
				}
			}
			query += ";";

			try
			{
				SqlCommand cmd = new SqlCommand(query, conn);
				cmd.Transaction = transaction;

				return cmd.ExecuteNonQuery();
			}
			catch
			{
				return -1;
			}
		}
	}
}
