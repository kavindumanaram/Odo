using Odo.Models;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace Odo.Controllers
{
	public class UserController : Controller
	{
		// GET: User
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
			bool status = false;

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
				//	status = true;
				}
				else
				{
					//save
					try
					{
						oe.Users.Add(usr);
						oe.SaveChanges();
					}
					catch (DbEntityValidationException dbEx)
					{
						foreach (var validationErrors in dbEx.EntityValidationErrors)
						{
							foreach (var validationError in validationErrors.ValidationErrors)
							{
								System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
							}
						}
					}
					//	status = true;
				}
			//	oe.SaveChanges();
			//	status = true;
			}

			if(status)
				return View("index");
			else
				return Json(status, JsonRequestBehavior.AllowGet);
		}
	}


}