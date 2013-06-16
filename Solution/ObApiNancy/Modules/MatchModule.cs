using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using ObCore.Models;

namespace ObApiNancy.Modules {
	public class MatchModule : Nancy.NancyModule {
		public MatchModule()
			: base("/api/matches") {
			Get["/"] = p => {
				if (Context.CurrentUser == null) {
					return Response.AsText("fuck");
				}
				return Response.AsJson(Match.FindAllFromUser(Context.CurrentOtakuBootyMember().IdMember));
			};
		}
	}
}