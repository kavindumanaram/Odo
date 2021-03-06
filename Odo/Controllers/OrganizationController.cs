﻿using System;
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

		[HttpGet]
		public ActionResult GetOrganizations()
		{
			List<Organization> organizationList;
			using (OdoEntities oe = new OdoEntities())
			{
				organizationList = oe.Organizations.OrderBy(a => a.OrganizationId).ToList();
			}
			return Json(organizationList, JsonRequestBehavior.AllowGet);
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

		[HttpGet]
		public ActionResult Delete(string organizationId)
		{
			bool status = false;
			using (OdoEntities oe = new OdoEntities())
			{
				int temp;
				if (int.TryParse(organizationId, out temp))
				{
					var organizationobject = oe.Organizations.Where(a => a.OrganizationId == temp).FirstOrDefault();
					if (organizationobject != null)
					{
						oe.Organizations.Remove(organizationobject);
					}
					oe.SaveChanges();
					status = true;
				}
			}

			if (status)
			{
				return View("Index");
			}
			else
			{
				return Json("Somethig went wrong !", JsonRequestBehavior.AllowGet);
			}
		}

		[HttpPost]
		public ActionResult Edit(Organization org)
		{
			using (OdoEntities oe = new OdoEntities())
			{
				var organizationObject = oe.Organizations.Where(a => a.OrganizationId == org.OrganizationId).FirstOrDefault();
				if (organizationObject != null)
				{
					organizationObject.Name = org.Name;
					organizationObject.Address = org.Address;
				}
				oe.SaveChanges();
			}
			return View("index");
		}
	}
}