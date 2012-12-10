using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ObApi.Models;
using ObCore.Models;
using System.Diagnostics;

namespace ObApi.Controllers {

	public class NotificationsController : ApiController {
		// GET api/notifications
		// todo: use ObAuthorizationRequired attribute from ObMobile
		// todo: allow client to request a range of notifications
		public HttpResponseMessage Get(string notificationType = "all", int skip=0, int take=25) {
			Trace.WriteLine(String.Format("Hello, world! From NotifcationsController#Get notificationType={0} skip={1} take={2}", notificationType, skip, take));
			Trace.WriteLine(String.Format("And furthermore, HttpContext.Current.Request[notificationType]={0}", HttpContext.Current.Request["notificationType"]));
			Trace.WriteLine(String.Format("And finally, HttpContext.Current.Request.QueryString[notificationType]={0}", HttpContext.Current.Request.QueryString["notificationType"]));
			int? memberId = HttpContext.Current.MemberId();
			if (!memberId.HasValue) return Request.CreateMissingAuthorizationTokenResponse();

			Notification.NotificationType nt;
			IEnumerable<Notification> notifications;
			try {
				nt = Notification.ToNotificationType(notificationType);
				notifications = Notification.Find(memberId.Value, skip, take, nt);
			}
			catch (Exception e) {
				return Request.CreateResponse<string>(HttpStatusCode.BadRequest, e.Message);
			}
			
			return Request.CreateResponse<IEnumerable<ObCore.Models.Notification>>(HttpStatusCode.OK, notifications).WithObApiDefaults();
			
		}

		/*
		// GET api/notifications/5
		public string Get(int id) {
			return "value";
		}

		// POST api/notifications
		public void Post([FromBody]string value) {
		}

		// PUT api/notifications/5
		public void Put(int id, [FromBody]string value) {
		}

		// DELETE api/notifications/5
		public void Delete(int id) {
		}
		*/
	}
}
