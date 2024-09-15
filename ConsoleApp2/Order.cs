using System;
using System.Collections.Generic;
using System.Data;

namespace Shop
{
	internal class Order
	{
		SQLAdapter adapter;

		public Order(SQLAdapter adapter)
		{
			this.adapter = adapter;
		}

		//Adds order to order table, Adds order items to itemStatus table
		//orderId is auto populated as an Identity column
		public bool AddOrder(string login, bool paid, DateTime timeStampRecived, float total, List<string> items)
		{
			return false;
		}

		//Accessors
		public DataTable GetOrder(int orderId)
		{
			return null;
		}

		public DataTable GetPaidOrders()
		{
			return null;
		}

		public DataTable GetUnpaidOrders()
		{
			return null;
		}

		//Setters
		public bool SetOrderPaid(int orderId)
		{
			return false;
		}
	}
}