using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
	internal class APILayer
	{
		//0 - SQL Server
		private int database;
		public APILayer(int serverType)
		{
			switch (serverType) {
				//Add future types as cases without breaks
				case 0:
					database = serverType;
					break;
				default:
					database = 0;
					break;
			}
		}

		//Item CRUD

		public bool AddItem()
		{
			switch (database)
			{
				case 0:	//SQL Server
					return ItemServ.AddItem();

				default:
					return false;
			}
		}

		/*
		 * The list of bools chooses which fields to include in the search
		 *  list order
		 *	0 - itemname
		 *	1 - type
		 *	2 - price
		 *	3 - description
		 *	4 - imageurl
		 */
		public DataSet GetItems(List<bool> colsToGet, string itemname, string type, float price, string description, string imageurl)
		{
			switch (database)
			{
				case 0:	//SQL Server
					return ItemServ.GetItems(colsToGet, itemname, type, price, description, imageurl);

				default:
					return null;
			}
		}

		public bool UpdateItem()
		{
			switch (database)
			{
				case 0:	//SQL Server
					return ItemServ.UpdateItem();

				default:
					return false;
			}
		}

		public bool DeleteItem()
		{
			switch (database)
			{
				case 0:	//SQL Server
					return ItemServ.DeleteItem();

				default:
					return false;
			}
		}

		//Order CRUD

		//Adds order to order table, Adds order items to itemStatus table
		public bool AddOrder(string login, bool paid, DateTime timeStampRecived, float total)
		{
			switch (database)
			{
				case 0: //SQL Server
					return OrderServ.AddOrder(login, paid, timeStampRecived, total);

				default:
					return false;
			}
		}

		/*
		 * The list of bools chooses which fields to include in the search
		 *  list order
		 *	0 - orderId
		 *	1 - login
		 *	2 - paid
		 *	3 - total
		 */
		public DataSet GetOrders(List<bool> colsToGet, int orderId, string login, bool paid, float total)
		{
			switch (database)
			{
				case 0: //SQL Server
					return OrderServ.GetOrders(colsToGet, orderId, login, paid, total);

				default:
					return null;
			}
		}

		public bool PayOrder(int orderId)
		{
			switch (database)
			{
				case 0: //SQL Server
					return OrderServ.PayOrder(orderId);

				default:
					return false;
			}
		}

		public bool CancelOrder(int orderId)
		{
			switch (database)
			{
				case 0: //SQL Server
					return OrderServ.CancelOrder(orderId);

				default:
					return false;
			}
		}

		//Profile CRUD

		/*
		 * 0 - login failed
		 * 1 - login succeeded (not manager)
		 * 2 - login succeeded (manager)
		 */
		public int Login()
		{
			switch (database)
			{
				case 0: //SQL Server
					return ProfileServ.Login();

				default:
					return 0;
			}
		}

		//User CRUD
		public bool AddProfile()
		{
			switch (database)
			{
				case 0: //SQL Server
					return ProfileServ.AddProfile();

				default:
					return false;
			}
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
		public DataSet GetProfiles(List<bool> colsToGet, string login, string phoneNum, string password, string type)
		{
			switch (database)
			{
				case 0: //SQL Server
					return ProfileServ.GetProfiles(colsToGet, login, phoneNum, password, type);

				default:
					return null;
			}
		}

		public bool UpdateProfile()
		{
			switch (database)
			{
				case 0: //SQL Server
					return ProfileServ.UpdateProfile();

				default:
					return false;
			}
		}

		public bool DeleteProfile()
		{
			switch (database)
			{
				case 0: //SQL Server
					return ProfileServ.DeleteProfile();

				default:
					return false;
			}
		}
	}
}
