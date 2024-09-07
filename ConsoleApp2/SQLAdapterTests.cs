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
			test.AddTest("SQLAdapterTests", CountUsers);
			test.AddTest("SQLAdapterTests", CountMenu);
			test.AddTest("SQLAdapterTests", CountOrders);
			test.AddTest("SQLAdapterTests", CountStatus);
		}

		public static bool CountUsers()
		{
			SQLAdapter test = new SQLAdapter("SQLSERVER");
			test.testing = true;

			DataTable results = test.query(new List<string> { "Count(*)" }, new List<string> {"shop.dbo.users"}, null, null, null, null);

			if (results != null)
				if (27951 == (int)results.Rows[0][0])
					return true;
			return false;
		}

		public static bool CountMenu()
		{
			SQLAdapter test = new SQLAdapter("SQLSERVER");
			test.testing = true;

			DataTable results = test.query(new List<string> { "Count(*)" }, new List<string> { "shop.dbo.menu" }, null, null, null, null);

			if (results != null)
				if (13 == (int)results.Rows[0][0])
					return true;
			return false;
		}

		public static bool CountOrders()
		{
			SQLAdapter test = new SQLAdapter("SQLSERVER");
			test.testing = true;

			DataTable results = test.query(new List<string> { "Count(*)" }, new List<string> { "shop.dbo.orders" }, null, null, null, null);

			if (results != null)
				if (86654 == (int)results.Rows[0][0])
					return true;
			return false;
		}

		public static bool CountStatus()
		{
			SQLAdapter test = new SQLAdapter("SQLSERVER");
			test.testing = true;

			DataTable results = test.query(new List<string> { "Count(*)" }, new List<string> { "shop.dbo.itemstatus" }, null, null, null, null);

			if (results != null)
				if (129563 == (int)results.Rows[0][0])
					return true;
			return false;
		}
	}
}
