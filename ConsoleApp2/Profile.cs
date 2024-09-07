using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
	internal static class Profile
	{
		/*
		 * 0 - login failed
		 * 1 - login succeeded (not manager)
		 * 2 - login succeeded (manager)
		 */
		public static int Login()
		{
			return 0;
		}

		//User CRUD
		public static bool AddProfile()
		{
			return true;
		}

		/*
		 * The list of bools chooses which fields to include in the search
		 *  list order
		 *	0 - login
		 *	1 - phoneNum
		 *	2 - password
		 *	3 - favItems
		 *	4 - type
		 */
		public static DataSet GetProfiles(List<bool> colsToGet, string login, string phoneNum, string password, string type)
		{
			return null;
		}

		public static bool UpdateProfile()
		{
			return true;
		}

		public static bool DeleteProfile()
		{
			return true;
		}
	}
}
