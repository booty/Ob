using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using ObCore.Models;
using HttpStatusCode = System.Net.HttpStatusCode;

namespace ObApiNancy {
	public class PrivateMessagesModule : Nancy.NancyModule {
		public PrivateMessagesModule()
			: base("/api/privatemessages") {

			// A list of messages, newest to oldest
			Get["/"] = p => {
				int? skip = Helpers.GetNullableInt(Request.Query, "skip") ?? 0;
				int? take = Helpers.GetNullableInt(Request.Query, "take") ?? 20;
				string type = (Request.Query.type.HasValue) ? Request.Query.type.HasValue : "received";

				if (type.ToLower().Equals("sent")) {
					return Response.AsJson(PrivateMessage.FindBySender(Context.CurrentOtakuBootyMember().IdMember, skip.Value, take.Value));
				}

				if (type.ToLower().Equals("received")) {
					return Response.AsJson(PrivateMessage.FindByRecipient(Context.CurrentOtakuBootyMember().IdMember, skip.Value, take.Value));
				}

				return Response.AsJson("If you supply a 'type' parameter, it should have a value of 'sent' or 'received'.", Nancy.HttpStatusCode.BadRequest);


			};

			Get[@"/(?<idMessage>\d+)"] = p => {
				PrivateMessage pm = PrivateMessage.FindForMember(Context.CurrentOtakuBootyMember().IdMember, p.idMessage, true);
				if (pm == null) return Response.AsJson("Message not found. Either that message doesn't exist, or you don't have permission to read it.", Nancy.HttpStatusCode.NotFound);
				return Response.AsJson(pm);
			};
		}
	}
}