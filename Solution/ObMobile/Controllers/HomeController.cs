using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObCore.Models;

namespace ObMobile.Controllers {
	public class HomeController : Controller {
		public ActionResult Index() {
			
			
			var notifications = Notification.Fetch(1238);

			var messages = from n in notifications
									 where n.EventType == "Comment" || n.EventType == "Private Message"
									 select n;

			ViewBag.Notifications = notifications.Take(25);
			ViewBag.Messages = messages.Take(20);

			return View();
		}

		public ActionResult About() {
			return View();
		}
	}
}
