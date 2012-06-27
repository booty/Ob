using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObCore.Models;

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
			try {
				Member member = Member.AttemptLogin(Request.Form["login"], Request.Form["password"]);
				if (member == null) {
					TempData["Error"]="Yeah, you fucked up.";
					return View();
				}
				return RedirectToAction("Index");
			}
			catch (Exception e) {
				TempData["Error"] = e.Message;
				return View();
			}
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
