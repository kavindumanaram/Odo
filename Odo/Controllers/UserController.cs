using Odo.Models;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace Odo.Controllers
{
	public class UserController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public ActionResult Save(int id)
		{
			using (OdoEntities oe = new OdoEntities())
			{
				var userObject = oe.Users.Where(a => a.UserId == id).FirstOrDefault();
				return View();
			}
		}

		[HttpPost]
		public ActionResult Save(User usr)
		{
			using (OdoEntities oe = new OdoEntities())
			{
				if (usr.UserId > 0)
				{
					var UserObject = oe.Users.Where(a => a.UserId == usr.UserId).FirstOrDefault();
					UserObject.FirstName = usr.FirstName;
					UserObject.LastName = usr.LastName;
					UserObject.Email = usr.Email;
					UserObject.Address = usr.Address;
					UserObject.LastActive = usr.LastActive;
				}
				else
				{
					oe.Users.Add(usr);
				}
				oe.SaveChanges();
			}
			return View("index");
		}

		public ActionResult GetUsers()
		{
			List<User> users = null;
			using (OdoEntities oe = new OdoEntities())
			{
				users = oe.Users.OrderBy(a => a.UserId).ToList();
			}

			return Json(users, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Delete(string userid)
		{
			int temp = 0;
			if (int.TryParse(userid, out temp))
			{
				using (OdoEntities oe = new OdoEntities())
				{
					var userobject = oe.Users.Where(a => a.UserId == temp).FirstOrDefault();
					oe.Users.Remove(userobject);
					oe.SaveChanges();
				}
			}

			return View("index");
		}
	}


}