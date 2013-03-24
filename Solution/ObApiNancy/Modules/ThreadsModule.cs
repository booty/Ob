using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using ObCore.Models;
using ObApiNancy;

namespace ObApiNancy {
	public class ThreadsModule : Nancy.NancyModule {


		public ThreadsModule()
			: base("/api/threads") {
				// Return a single thread and its replies
				Get[@"/(?<idThread>\d+)"] = p => {
					int? skip = Helpers.GetNullableInt(Request.Query, "skip") ?? 0;
					int? take = Helpers.GetNullableInt(Request.Query, "take") ?? 20;
					Thread thread;
					thread = Thread.FindThreadWithReplies(Context.CurrentOtakuBootyMember(), p.idThread, skip.Value, take.Value);
					return Response.AsJson(thread);
				};
		}
	}
}