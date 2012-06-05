using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObCore.Models;

namespace ObMobile.Controllers {
	public class MessageController : Controller {
		//
		// GET: /Message/
		public ActionResult Index() {
			List<MessageNotification> mn = MessageNotification.Fetch(1238);
			return View(mn);
		}

		//
		// GET: /Message/Details/5

		public ActionResult Details(int id) {
			return View();
		}

		//
		// GET: /Message/Create

		public ActionResult Create() {
			return View();
		}

		//
		// POST: /Message/Create

		[HttpPost]
		public ActionResult Create(FormCollection collection) {
			try {
				// TODO: Add insert logic here

				return RedirectToAction("Index");
			}
			catch {
				return View();
			}
		}

		//
		// GET: /Message/Edit/5

		public ActionResult Edit(int id) {
			return View();
		}

		//
		// POST: /Message/Edit/5

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
		// GET: /Message/Delete/5

		public ActionResult Delete(int id) {
			return View();
		}

		//
		// POST: /Message/Delete/5

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
