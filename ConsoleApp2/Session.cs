using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
	internal class Session
	{
		Status status;
		Profile profile;
		Order order;
		Item item;
		SQLAdapter adapter;

		enum UserType
		{
			Customer,
			Admin
		}

		string user;
		UserType userType;

		public Session()
		{
			this.adapter = new SQLAdapter("TRANSACT");

			this.status = new Status(adapter);
			this.profile = new Profile(adapter);
			this.order = new Order(adapter);
			this.item = new Item(adapter);
		}

		//----------------------------- Manager Only ----------------------------------------

		//Profile
		public DataRow GetPhone(string user)
		{
			if (user != this.user && UserType.Customer == userType)
				return null;

			return profile.GetPhone(user);
		}

		public bool MakeAdmin(string user)	//Can't change account type of own account
		{
			if (user == this.user || UserType.Customer == userType)
				return false;

			return profile.MakeAdmin(user);
		}

		public bool MakeCust(string user)	//Can't change account type of own account
		{
			if (user == this.user || UserType.Customer == userType)
				return false;

			return profile.MakeCust(user);
		}

		public bool DeleteUser(string user)
		{
			if (user != this.user && UserType.Customer == userType)
				return false;

			return profile.DeleteUser(user);
		}

		//Item
		public bool AddItem(string itemname, string type, float price, string description, string imageurl)
		{
			if (UserType.Customer == userType)
				return false;

			return item.AddItem(itemname, type, price, description, imageurl);
		}

		public bool UpdateItem(string itemname, string type, float price, string description, string imageurl)
		{
			if (UserType.Customer == userType)
				return false;

			return item.UpdateItem(itemname, type, price, description, imageurl);
		}

		public bool DeleteItem(string itemname)
		{
			if (UserType.Customer == userType)
				return false;

			return item.DeleteItem(itemname);
		}

		//Order
		public DataTable GetPaidOrders()
		{
			if (UserType.Customer == userType)
				return null;

			return order.GetPaidOrders();
		}

		public DataTable GetUnpaidOrders()
		{
			if (UserType.Customer == userType)
				return null;

			return order.GetUnpaidOrders();
		}
		
		public bool SetOrderPaid(int orderId)
		{
			if (UserType.Customer == userType)
				return false;

			return order.SetOrderPaid(orderId);
		}

		//Status
		public bool SetStatus(int orderId, string status)
		{
			if (UserType.Customer == userType)
				return false;

			return this.status.SetStatus(orderId, status);
		}

		public bool RemoveItem(int orderId, string itemname)
		{
			if (UserType.Customer == userType)
				return false;

			return status.RemoveItem(orderId, itemname);
		}

		public bool AddItem(List<string> items, int orderId, DateTime timeStampRecieved)
		{
			if (UserType.Customer == userType)
				return false;

			return status.AddItem(items, orderId, timeStampRecieved);
		}

		//----------------------------------------------------------------------------------------

		//Item
		public DataTable GetItems(List<bool> colsToGet, string itemname, string type, float price)
		{
			return item.GetItems(colsToGet, itemname, type, price);
		}

		//Order
		public bool AddOrder(bool paid, float total, List<string> items)
		{
			return order.AddOrder(user, paid, DateTime.Now, total, items);
		}
		
		//Profile
		public bool CreateUser(string newUser, string phoneNum, string password)
		{
			if (null != user)
				logout();

			if(true == profile.CreateUser(newUser, phoneNum, password))
			{
				user = newUser;
				userType = UserType.Customer;
				return true;
			}
			return false;
		}

		public bool UpdateLogin(string newUser)
		{
			if (true == profile.UpdateLogin(newUser, user))
			{
				user = newUser;
				return true;
			}
			
			return false;
		}

		public bool ChangePhone(string phoneNumber)
		{
			return profile.ChangePhone(user, phoneNumber);
		}

		public bool ChangePassword(string user, string password)
		{
			if (user != this.user && UserType.Customer == userType)
				return false;

			return profile.ChangePassword(user, password);
		}

		/*
		 * 0 - login failed
		 * 1 - login succeeded (not manager)
		 * 2 - login succeeded (manager)
		 */
		public bool Login(string user, string password)
		{
			int type = profile.Login(user, password);
			
			if (type == 1)
			{
				userType = UserType.Customer;
			}
			else if(type == 2)
			{
				userType = UserType.Admin;
			}
			else
			{
				return false;
			}

			this.user = user;
			return true;
		}

		public bool logout()
		{
			adapter.Commit();
			userType = UserType.Customer;
			user = null;

			return true;
		}

		public bool AddFav(string fav)
		{
			return profile.AddFav(user, fav);
		}

		public bool RemoveFav(string fav)
		{
			return profile.RemoveFav(user, fav);
		}

		public DataRow GetFavs()
		{
			return profile.GetFavs(user);
		}

		//Status
		public DataTable GetStatus(int orderId)
		{
			DataTable dt = order.GetOrder(orderId);
			if (UserType.Admin == userType || user == dt.Rows[0][1].ToString())
			{
				return status.GetStatus(orderId);
			}
			return null;
		}
	}
}
