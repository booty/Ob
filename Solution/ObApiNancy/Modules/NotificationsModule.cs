using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Security;
using ObCore.Models;
using HttpStatusCode = System.Net.HttpStatusCode;

namespace ObApiNancy {
	public class NotificationsModule : Nancy.NancyModule {
		public NotificationsModule()
			: base("/api/notifications") {

			this.RequiresAuthentication();
			Get["/"] = p => {
				int skip = 0, take = 25;

				if (Request.Query.skip.HasValue) Int32.TryParse(Request.Query.skip.Value, out skip);
				if (Request.Query.take.HasValue) Int32.TryParse(Request.Query.take.Value, out take);
				string notificationType = Request.Query.notificationType.Value;

				Notification.NotificationType nt;
				if (!Request.Query.notificationType.HasValue) {
					nt = Notification.NotificationType.All;
				}
				else {
					try {
						nt = Notification.ToNotificationType(notificationType);
					}
					catch (Exception e) {
						return Response.AsJson(e.Message, Nancy.HttpStatusCode.BadRequest);
					}
				}

				if (take>50) take=50;
				var notifications = Notification.Find(Context.CurrentOtakuBootyMember().IdMember, skip, take, nt);
				return Response.AsJson(notifications);

			};
		}
	}
}