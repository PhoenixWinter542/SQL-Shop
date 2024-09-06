using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
	public class SQLAdapter
	{
		private enum SQLType
		{
			SQLSERVER
		};
		private SQLType type;

		SQLAdapter(string typeName)
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

		public DataSet query(List<string> selectColNames, List<string> fromTableName, List<string> whereConds, string groupBy, string having, string orderBy)
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
		public static DataSet SQLSERVER(List<string> selectColNames, List<string> fromTableName, List<string> whereConds, string groupBy, string having, string orderBy)
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
			query += "\n";

			//FROM
			if (fromTableName == null)
				return null;
			query += "FROM";
			foreach(string tableName in fromTableName)
			{
				query += " " + tableName;
			}
			query += "\n";

			//WHERE
			if(whereConds != null)
			{
				query += "WHERE";
				foreach (string conditions in whereConds)
				{
					query += " " + conditions;
				}
				query += "\n";
			}

			//GROUP BY
			if (groupBy == null && having != null)
				return null;
			if (groupBy != null)
			{
				query += "GROUP BY " + groupBy + "\n";
			}

			//HAVING
			if(having != null)
			{
				query += "HAVING " + having + "\n";
			}

			//ORDER BY
			if(orderBy != null)
			{
				query += "ORDER BY" + orderBy + ";";
			}

			Console.WriteLine(query);

			return null;
		}
	}
}
