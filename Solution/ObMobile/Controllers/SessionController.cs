using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObCore;
using ObCore.Helpers;
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
			Trace.Write(String.Format("Session.IsObLoggedIn? {0}", Session.IsObLoggedIn()), "SessionController.Create");
			if (Session.IsObLoggedIn()) {
				RedirectToAction("Index", "Home");
			}
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
			string loginToken;
			AuthenticationResult authResult = Security.Authenticate(Request.Form["login"], Request.Form["password"], Request.ServerVariables["REMOTE_ADDR"] , Request.ServerVariables["HTTP_URL"]);
			if (authResult.AuthenticationResultCode != Security.AuthenticationResultCode.Success) {
				TempData.AddErrorMessage("Sorry!", "We couldn't find that login and password. Maybe you mistyped some shit.");
				return View();
			}

			// Store member in in session
			HttpContext.MakeAuthenticatedAsFuck(authResult, true);
			return RedirectToAction("Index", "Home");


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
