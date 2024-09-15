using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
	internal class Profile
	{
		SQLAdapter adapter;

		public Profile(SQLAdapter adapter)
		{
			this.adapter = adapter;
		}

		public bool DeleteUser(string newUser)
		{
			return false;
		}

		public bool CreateUser(string newUser, string phoneNum, string password)
		{
			return false;
		}

		//Login
		public bool UpdateLogin(string newUser, string oldUser)
		{
			//check if new login is already in use
			//update login if not
			return false;
		}


		//Phone Number
		public DataRow GetPhone(string user)
		{
			return null;
		}

		public bool ChangePhone(string user, string phoneNumber)
		{
			return false;
		}

		//Password
		public bool ChangePassword(string user, string password)
		{
			return false;
		}

		//User Type
		public bool MakeAdmin(string user)
		{
			return false;
		}
		public bool MakeCust(string user)
		{
			return false;
		}

		/*
		 * 0 - login failed
		 * 1 - login succeeded (not manager)
		 * 2 - login succeeded (manager)
		 */
		public int Login(string user, string password)
		{
			return 0;
		}

		//Favorites
		public bool AddFav(string user, string fav)
		{
			return false;
		}

		public bool RemoveFav(string user, string fav)
		{
			return false;
		}

		public DataRow GetFavs(string user)
		{
			return null;
		}
	}
}
