using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
	public class TestFramework
	{
		//variables
		List<List<Func<bool>>> testSets;
		List<string> setNames;
		public bool showAll = true;
		public bool stopOnFail = false;
		public string indent = "\t";

		//constructors
		public TestFramework() { }
		public TestFramework(bool showAll, bool stopOnFail)
		{
			this.showAll = showAll;
			this.stopOnFail = stopOnFail;
		}

		//functions
		public void AddTest(string setName, Func<bool> test)
		{
			//If set exists, add test to it
			for(int i = 0; i < setNames.Count; i++)
			{
				if (setName.Equals(setNames[i]))
				{
					testSets[i].Add(test);
					return;
				}
			}

			//Create new set and add test to it
			testSets.Add(new List<Func<bool>> { test });
			setNames.Add(setName);
		}

		private void TestPass()
		{
			if (showAll)
			{
				Console.ForegroundColor = ConsoleColor.DarkGreen;
				Console.Write(indent + "PASSED");
				Console.ResetColor();
			}
		}

		private void TestFail()
		{
			if (showAll)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("\n" + indent + indent + indent + "FAILED");
				Console.ResetColor();
			}
		}

		//Returns true if ALL tests in the set pass, false if ANY fail
		private bool RunSet(List<Func<bool>> testSet)
		{
			bool allPassed = true;
			foreach (Func<bool> test in testSet)
			{
				if (showAll)
					Console.Write(indent + indent + "Running " + test.Method.Name);
				if (true == test())
				{
					TestPass();
				}
				else
				{
					TestFail();
					allPassed = false;
					if (stopOnFail)
						return false;
				}
				if (showAll)
					Console.WriteLine();
			}
			return allPassed;
		}

		public void runTests()
		{
			bool allPassed = true;
			Console.WriteLine("Starting Tests");
			for (int i = 0; i < testSets.Count; i++)
			{
				Console.WriteLine("Running " + setNames[i]);
				if (true == RunSet(testSets[i]))
				{
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine(indent + "PASSED");
					Console.ResetColor();
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\n" + indent + "FAILED");
					Console.ResetColor();
					allPassed = false;
					if(stopOnFail)
						break;
				}
			}

			if (allPassed)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("All Tests PASSED");
				Console.ResetColor();
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Tests FAILED");
				Console.ResetColor();
			}
		}

	}
}
