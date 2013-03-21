using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using ObCore;
using ObCore.Models;

namespace ObApiNancy {
	public class ForumsModule : Nancy.NancyModule {
		public ForumsModule() : base("/api/forums") {
			// returns a list of forums visible to the current user
			Get["/"] = p => {
				return Response.AsJson(Forum.Find(Context.CurrentOtakuBootyPermissionLevel()));
			};

			// returns a single forum, optionally with a list of threads
			Get[@"/(?<idForum>\d+)"] = p => {
				int skip=0, take=25;
				if (Request.Query.skip.HasValue) Int32.TryParse(Request.Query.skip.Value, out skip);
				if (Request.Query.take.HasValue) Int32.TryParse(Request.Query.take.Value, out take);
				Member member = Context.CurrentOtakuBootyMember();
				var includeSticky = Request.Query.includeSticky;

				bool sticky = ((includeSticky.HasValue) && !includeSticky.Equals("false") && !includeSticky.Equals("0"));
				Forum result;
				if (member == null) {
					result = Forum.FindWithThreads(MemberPermissionLevel.Unauthenticated, p.idForum, false, sticky, skip, take);
				}
				else {
					result = Forum.FindWithThreads(member.MemberPermissionLevel, p.idForum, member.IsAdult, sticky, skip, take);
				}

				if (result == null) return Response.AsJson("That forum doesn't exist, or you don't have permission to view it.", HttpStatusCode.NotFound);
				return Response.AsJson(result);
			};
		}
	}
}