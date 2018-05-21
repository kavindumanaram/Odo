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

		[HttpGet]
	    public ActionResult Save(int id)
	    {
		    using (OdoEntities oe = new OdoEntities())
		    {
			    var organizationObject = oe.Organizations.Where(org => org.OrganizationId == id).FirstOrDefault();
			    return View(organizationObject);
		    }
	    }

	    [HttpPost]
	    public ActionResult Save(Organization emp)
	    {
		    bool status = false;
		    if (ModelState.IsValid)
		    {
			    using (OdoEntities dc = new OdoEntities())
			    {
				    if (emp.OrganizationId > 0)
				    {
					    //Edit 
					    var v = dc.Organizations.Where(a => a.OrganizationId == emp.OrganizationId).FirstOrDefault();
					    if (v != null)
					    {
						    v.Name = emp.Name;
						    v.Address = emp.Address;
					    }
				    }
				    else
				    {
					    //Save
					    dc.Organizations.Add(emp);
				    }
				    dc.SaveChanges();
				    status = true;
			    }
		    }
		    return new JsonResult { Data = new { status = status } };
	    }
	}
}