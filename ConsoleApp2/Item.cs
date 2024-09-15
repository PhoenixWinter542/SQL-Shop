using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
	internal class Item
	{
		SQLAdapter adapter;

		public Item(SQLAdapter adapter)
		{
			this.adapter = adapter;
		}

		public bool AddItem(string itemname, string type, float price, string description, string imageurl)
		{
			return false;
		}

		/*
		 * The list of bools chooses which fields to include in the search
		 *  list order
		 *	0 - itemname
		 *	1 - type
		 *	2 - price
		 */
		public DataTable GetItems(List<bool> colsToGet, string itemname, string type, float price)
		{
			return null;
		}

		public bool UpdateItem(string itemname, string type, float price, string description, string imageurl)
		{
			return false;
		}

		public bool DeleteItem(string itemname)
		{
			return false;
		}
	}
}
