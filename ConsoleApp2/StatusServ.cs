using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
	internal static class StatusServ
	{
		//itemStatus CRUD

		//Adds given items to itemStatus as part of given order with given timestamp
		//Assumes deliveryStatus is "Hasn't started"
		public static bool AddItem(List<string> items, int orderId, DateTime timeStampRecieved)
		{
			return true;
		}

		//Returns all items that are part of the given order
		public static DataSet GetStatus(int orderId)
		{
			return null;
		}

		//Sets the delivery status for all items with given orderId
		public static bool SetStatus(int orderId, string status)
		{
			return true;
		}

		//Allows managers to remove items from shipments
		public static bool RemoveItem(int orderId, string itemname)
		{
			return true;
		}
	}
}
