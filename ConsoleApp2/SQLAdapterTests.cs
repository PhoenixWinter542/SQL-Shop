using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
	internal static class SQLAdapterTests
	{
		public static void AddTests(ref TestFramework test)
		{
			//test.showAll = false;
			//test.stopOnFail = true;
			string group = "TransactTests";
			test.AddTest(group, CountUsers);
			test.AddTest(group, CountMenu);
			test.AddTest(group, CountOrders);
			test.AddTest(group, CountStatus);
			test.AddTest(group, ReadUser);
			test.AddTest(group, AddUser);
			test.AddTest(group, UpdateRows);
			test.AddTest(group, DeleteRows);
		}

		public static bool CountUsers()
		{
			SQLAdapter test = new SQLAdapter("TRANSACT", true);

			DataTable results = test.Read(new List<string> { "Count(*)" }, new List<string> {"shop.dbo.users"}, null, null, null, null);

			if (results != null && 0 != results.Rows.Count)
				if (27951 == (int)results.Rows[0][0])
					return true;
			return false;
		}

		public static bool CountMenu()
		{
			SQLAdapter test = new SQLAdapter("TRANSACT", true);

			DataTable results = test.Read(new List<string> { "Count(*)" }, new List<string> { "shop.dbo.menu" }, null, null, null, null);

			if (results != null && 0 != results.Rows.Count)
				if (13 == (int)results.Rows[0][0])
					return true;
			return false;
		}

		public static bool CountOrders()
		{
			SQLAdapter test = new SQLAdapter("TRANSACT", true);

			DataTable results = test.Read(new List<string> { "Count(*)" }, new List<string> { "shop.dbo.orders" }, null, null, null, null);

			if (results != null && 0 != results.Rows.Count)
				if (86654 == (int)results.Rows[0][0])
					return true;
			return false;
		}

		public static bool CountStatus()
		{
			SQLAdapter test = new SQLAdapter("TRANSACT", true);

			DataTable results = test.Read(new List<string> { "Count(*)" }, new List<string> { "shop.dbo.itemstatus" }, null, null, null, null);

			if (results != null && 0 != results.Rows.Count)
				if (129563 == (int)results.Rows[0][0])
					return true;
			return false;
		}
		public static bool ReadUser()
		{
			SQLAdapter test = new SQLAdapter("TRANSACT", true);

			DataTable results = test.Read(null, new List<string> { "shop.dbo.users" }, new List<string> { "login = 'Joana'" }, null, null, null);
			if (results != null && 0 != results.Rows.Count)
				if ("+1(720)615-6594" == results.Rows[0][1].ToString())
					return true;
			return false;
		}

		//Tests adding string values
		public static bool AddUser()
		{
			SQLAdapter test = new SQLAdapter("TRANSACT", true);

			DataTable results = test.Read(null, new List<string> { "shop.dbo.users" }, new List<string> { "login = 'x123sgse8'" }, null, null, null);
			if (results != null && 0 < results.Rows.Count)	//User should not exist
					return false;

			int rowsEffected = test.Create("shop.dbo.users", new List<(string, string)> { ("login", "'x123sgse8'" ), ("phoneNum", "'+1(234)567-8901'"), ("password", "'password'"), ("type", "'Customer'") });
			if (rowsEffected != 1)
				return false;

			results = test.Read(null, new List<string> { "shop.dbo.users" }, new List<string> { "login = 'x123sgse8'" }, null, null, null);
			if (results != null && 0 < results.Rows.Count) 
				return true;

			return false;
		}


		public static bool UpdateRows()
		{
			SQLAdapter test = new SQLAdapter("TRANSACT", true);

			int rowsEffected = test.Update("shop.dbo.itemStatus", new List<string> { "deliveryStatus = 'Finished'" }, new List<string> { "orderId = '84454'" });
			if (rowsEffected != 3)
				return false;
			
			DataTable results = test.Read(new List<string> { "deliveryStatus" }, new List<string> { "shop.dbo.itemStatus" }, new List<string> { "orderId = '84454'" }, null, null, null);
			if (results != null && 0 < results.Rows.Count)
				foreach (DataRow row in results.Rows)
				{
					if ("Finished" != row[0].ToString())
						return false;
				}

			return true;
		}

		public static bool DeleteRows()
		{
			SQLAdapter test = new SQLAdapter("TRANSACT", true);

			int rowsEffected = test.Delete("shop.dbo.orders", new List<string> { "login = 'Gillian'" });
			if (rowsEffected != 7)
				return false;

			DataTable results = test.Read(null, new List<string> { "shop.dbo.orders" }, new List<string> { "login = 'Gillian'" }, null, null, null);
			if (results != null && 0 < results.Rows.Count)
				return false;

			return true;
		}
	}
}
