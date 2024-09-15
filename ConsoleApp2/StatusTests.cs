using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
	internal class StatusTests
	{
		private static bool CompareRow(DataRow row, int orderid, string itemname, DateTime timeStampRecieved, string deliveryStatus)
		{
			if(row == null)
				return false;
			if (orderid != (int)row[0])
				return false;
			if(itemname != (string)row[1])
				return false;
			if(timeStampRecieved != (DateTime)row[2])
				return false;
			if(deliveryStatus != (string)row[3])
				return false;

			return true;
		}

		public static void AddTests(ref TestFramework test)
		{
			//test.showAll = false;
			//test.stopOnFail = true;
			string group = "StatusTests";
			test.AddTest(group, GetTest);
		}

		public static bool GetTest()
		{
			DataTable results = Status.GetStatus(20);
			if (results == null || 2 != results.Rows.Count)
				return false;

			if (false == CompareRow(results.Rows[0], 20, "7up", DateTime.Parse("2016-09-10 13:14:00.0000000"), "Finished"))
				return false;
			if (false == CompareRow(results.Rows[1], 20, "Orange Juice", DateTime.Parse("2016-09-10 13:14:00.0000000"), "Hasn't started"))
				return false;

			return true;
		}
	}
}
