using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Odo.Models;

namespace Odo.Controllers
{
	public class OrganizationController : Controller
	{
		// GET: Organization
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Save(int id)
		{
			using (OdoEntities oe = new OdoEntities())
			{
				var organization = oe.Organizations.Where(a => a.OrganizationId == id).FirstOrDefault();

				return View(organization);
			}
		}
	}
}