using System;
using System.Collections.Generic;
using System.Data;

namespace Shop
{
	internal static class OrderServ
	{
		//Order CRUD

		//Adds order to order table, Adds order items to itemStatus table
		public static bool AddOrder(string login, bool paid, DateTime timeStampRecived, float total)
		{
			return true;
		}

		/*
		 * The list of bools chooses which fields to include in the search
		 *  list order
		 *	0 - orderId
		 *	1 - login
		 *	2 - paid
		 *	3 - total
		 */
		public static DataSet GetOrders(List<bool> colsToGet, int orderId, string login, bool paid, float total)
		{
			return null;
		}

		public static bool PayOrder(int orderId)
		{
			return true;
		}

		public static bool CancelOrder(int orderId)
		{
			return true;
		}

	}
}