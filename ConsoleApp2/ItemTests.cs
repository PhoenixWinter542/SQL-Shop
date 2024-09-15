using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
	internal static class ItemTests
	{
		public static void AddTests(ref TestFramework test)
		{
			//test.showAll = false;
			//test.stopOnFail = true;
			string group = "ItemTests";
			test.AddTest(group, GetTest);
		}

		public static bool GetTest()
		{

			return false;
		}
	}
}
