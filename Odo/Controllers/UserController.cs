using Odo.Models;
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

		public ActionResult Save(int id)
		{
			using (OdoEntities oe = new OdoEntities())
			{
				var userObject = oe.Users.Where(a => a.UserId == id).FirstOrDefault();
				return View();
			}
		}
	}


}