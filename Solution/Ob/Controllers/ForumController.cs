using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ob.Controllers {
	public class ForumController : Controller {
		// Show default forum (all threads)
		// GET: /Forum/
		public string Index(int? pageNumber) {
			return String.Format("Showing default forum view (all threads) page #{0}", pageNumber );
		}

		//
		// GET: /Forum/Details/5
		public string Details(int id) {
			return String.Format("Listing threads for forum #{0}", id);
		}

		public string ThreadsPostedIn(int idMember, int? pageNumber) {
			return string.Format("Threads that member #{0} posted in, page #{1}", idMember, pageNumber);
		}

		public string Search() {
			return string.Format("Let's forum search!");
		}

		//
		// GET: /Forum/Create
		/*
		public ActionResult Create() {
			return View();
		}*/

		//
		// POST: /Forum/Create
		/*
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
		*/

		//
		// GET: /Forum/Edit/5
		/*
		public string Edit(int id) {
			return View();
		}
		*/

		//
		// POST: /Forum/Edit/5
		/*
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
		*/

		/*
		//
		// GET: /Forum/Delete/5

		public ActionResult Delete(int id) {
			return View();
		}
		*/
		//
		// POST: /Forum/Delete/5
		/*
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
		 * */
	}
}
