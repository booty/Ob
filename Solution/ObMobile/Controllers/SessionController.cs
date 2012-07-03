using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
			Member member = Member.AttemptLogin(Request.Form["login"], Request.Form["password"], Request.ServerVariables["REMOTE_ADDR"] , Request.ServerVariables["HTTP_URL"], out loginToken);
			if (member == null) {
				TempData.AddErrorMessage("Sorry!", "We couldn't find that login and password. Maybe you mistyped some shit.");
				return View();
			}

			// Store member in in session
			Session[SessionVars.CurrentObMember] = member;
			Trace.Write("Farting!!!!");
			if (Response.Cookies != null) {
				Response.Cookies.Add(new HttpCookie("token", loginToken));
				Response.Cookies["token"].Expires = DateTime.Now + new TimeSpan(ConfigurationManager.AppSettings.ValueOrDefault("LoginTokenCookieExpirationDays", 30), 0, 0,0);
			}
		
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
