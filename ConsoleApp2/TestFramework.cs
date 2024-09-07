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
		List<List<Func<bool>>> testSets = new List<List<Func<bool>>>();
		List<string> setNames = new List<string>();
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
				Console.Write(indent + "FAILED");
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
					Console.Write("\n" + indent + indent + test.Method.Name);
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
			}
			return allPassed;
		}

		public void RunTests()
		{
			bool allPassed = true;
			Console.Write("Starting Tests");
			if (false == showAll)
				Console.Write("\n");
			for (int i = 0; i < testSets.Count; i++)
			{
				if (showAll)
				{
					Console.Write("\n\n" + indent);
					Console.BackgroundColor = ConsoleColor.DarkGray;
					Console.ForegroundColor = ConsoleColor.White;
					Console.Write("Running " + setNames[i]);
					Console.ResetColor();
				}

				bool setPassed = RunSet(testSets[i]);
				Console.Write("\n" + indent);
				if (true == setPassed)
				{
					Console.ForegroundColor = ConsoleColor.White;
					Console.BackgroundColor = ConsoleColor.DarkGreen;
					Console.Write(setNames[i]);
					Console.ResetColor();
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write(" PASSED");
					Console.ResetColor();
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.White;
					Console.BackgroundColor = ConsoleColor.DarkRed;
					Console.Write(setNames[i]);
					Console.ResetColor();
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write(" FAILED");
					Console.ResetColor();
					allPassed = false;
					if(stopOnFail)
						break;
				}
			}

			Console.Write("\n\n");
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
