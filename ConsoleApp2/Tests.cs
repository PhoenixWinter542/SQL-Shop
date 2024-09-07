using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
	internal static class Tests
	{
		public static void Run()
		{
			TestFramework tests = new TestFramework();

			SQLAdapterTests.AddTests(ref tests);
			//StatusTests.AddTests(ref tests);
			//ItemTests.AddTests(ref tests);
			//ProfileTests.AddTests(ref tests);
			//OrderTests.AddTests(ref tests);

			tests.RunTests();
		}
	}
}
