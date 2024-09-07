using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
	internal static class Item
	{
		//Item CRUD

		public static bool AddItem()
		{
			return true;
		}

		/*
		 * The list of bools chooses which fields to include in the search
		 *  list order
		 *	0 - itemname
		 *	1 - type
		 *	2 - price
		 *	3 - description
		 *	4 - imageurl
		 */
		public static DataSet GetItems(List<bool> colsToGet, string itemname, string type, float price, string description, string imageurl)
		{
			return null;
		}

		public static bool UpdateItem()
		{
			return true;
		}

		public static bool DeleteItem()
		{
			return true;
		}
	}
}
