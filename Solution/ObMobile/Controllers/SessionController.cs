using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObCore.Models;
using ObMobile.Helpers;

namespace ObMobile.Controllers {
	public class SessionController : Controller {
		//
		// GET: /Session/

		public ActionResult Index() {
			return View();
		}

		//
		// GET: /Session/Details/5

		public ActionResult Details(int id) {
			return View();
		}

		//
		// GET: /Session/Create

		public ActionResult Create() {
			return View();
		}

		//
		// POST: /Session/Create

		[HttpPost]
		public ActionResult Create(FormCollection collection) {
			//try {
				// Did they forget one or the other?
				if ((String.IsNullOrWhiteSpace(Request.Form["login"])) || (String.IsNullOrWhiteSpace(Request.Form["password"]))) {
					TempData.AddInfoMessage("Protip!", "You should probably enter your login and password, and <em>then</em> click the button. Right?");
					return View();
				}
				// Try the login
				Member member = Member.AttemptLogin(Request.Form["login"], Request.Form["password"]);
				if (member == null) {
					TempData.AddErrorMessage("Sorry!","We couldn't find that login and password. Maybe you mistyped some shit.");
					return View();
				}
				Session[SessionVars.CurrentObMember] = member;
				return RedirectToAction("Index","Home");
			//}
			//catch (Exception e) {
			//	TempData["Error"] = String.Format("<strong>Whoops.</strong> We fucked up: {0} ",e.Message);
			//	return View();
			//}
		}

		//
		// GET: /Session/Edit/5

		public ActionResult Edit(int id) {
			return View();
		}

		//
		// POST: /Session/Edit/5

		[HttpPost]
		public ActionResult Edit(int id, FormCollection collection) {
			try {
				// TODO: Add update logic here

				return RedirectToAction("Index");
			}
			catch {
				return View();
			}
		}

		//
		// GET: /Session/Delete/5

		public ActionResult Delete(int id) {
			return View();
		}

		//
		// POST: /Session/Delete/5

		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection) {
			try {
				// TODO: Add delete logic here

				return RedirectToAction("Index");
			}
			catch {
				return View();
			}
		}
	}
}
