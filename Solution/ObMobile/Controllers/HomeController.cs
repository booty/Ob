using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObCore;
using ObMobile.Attributes;
using ObCore.Models;
using ObCore.Helpers;
using ObMobile.Helpers;
namespace ObMobile.Controllers {
	public class HomeController : Controller {
		
		[ObAuthorizationRequired(AuthorizationRequirement.IsAuthenticated)]
		public ActionResult Index() {
			var notifications = Notification.Fetch(Session.CurrentObMember().IdMember);
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
