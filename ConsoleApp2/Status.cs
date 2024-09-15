using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
	internal class Status
	{
		SQLAdapter adapter;

		public Status(SQLAdapter adapter)
		{
			this.adapter = adapter;
		}

		//itemStatus CRUD

		//Adds given items to itemStatus as part of given order with given timestamp
		//Assumes deliveryStatus is "Hasn't started"
		public bool AddItem(List<string> items, int orderId, DateTime timeStampRecieved)
		{
			return false;
		}

		//Returns all items that are part of the given order
		public DataTable GetStatus(int orderId)
		{
			string where = "orderid = " + orderId.ToString();
			return  adapter.Read(null, new List<string> { "shop.dbo.itemStatus" }, new List<string> { where }, null, null, null);
		}

		//Sets the delivery status for all items with given orderId
		public bool SetStatus(int orderId, string status)
		{
			return false;
		}

		//Allows managers to remove items from shipments
		public bool RemoveItem(int orderId, string itemname)
		{
			return false;
		}
	}
}
