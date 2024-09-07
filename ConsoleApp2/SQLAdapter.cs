using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Shop
{
	public class SQLAdapter
	{
		public bool testing = false;

		private string connectionString = "server=DESKTOP-SV6S892;trusted_connection=Yes";
		private enum SQLType
		{
			SQLSERVER
		};
		private SQLType type;

		public SQLAdapter(string typeName)
		{
			switch (typeName)
			{
				case "SQLSERVER":
					type = SQLType.SQLSERVER;
					break;
				default:
					type = SQLType.SQLSERVER;
					break;
			}
		}

		public DataTable query(List<string> selectColNames, List<string> fromTableName, List<string> whereConds, string groupBy, string having, string orderBy)
		{
			switch (type)
			{
				case SQLType.SQLSERVER:
					return SQLSERVER(selectColNames, fromTableName, whereConds, groupBy, having, orderBy);
				default:
					return null;
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
		public  DataTable SQLSERVER(List<string> selectColNames, List<string> fromTableName, List<string> whereConds, string groupBy, string having, string orderBy)
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

			//Console.WriteLine("\n" + query);

			SqlConnection conn = new SqlConnection(connectionString);
			conn.Open();
			SqlTransaction transaction = conn.BeginTransaction();
			SqlDataAdapter da = new SqlDataAdapter(query, conn);
			da.SelectCommand.Transaction = transaction;
			DataSet ds = new DataSet();

			da.Fill(ds);

			if (true == testing)
				transaction.Rollback();
			else
				transaction.Commit();
			conn.Close();
			return ds.Tables[0];
		}
	}
}
