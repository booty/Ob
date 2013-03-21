using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Security;
using ObCore.Models;


namespace ObApiNancy {
	public class CommentsModule: Nancy.NancyModule {
		public CommentsModule()
			: base("/api/Comments") {
				this.RequiresAuthentication();

				// return a single comment, either to or from this member
				Get[@"/"] = p => {
					int skip = 0, take = 25;

					if (Request.Query.skip.HasValue) Int32.TryParse(Request.Query.skip.Value, out skip);
					if (Request.Query.take.HasValue) Int32.TryParse(Request.Query.take.Value, out take);

					if (skip > 100) skip = 100; //todo: should be in web.config

					return Response.AsJson( Comment.FindToMember( Context.CurrentOtakuBootyMember().IdMember,skip,take));
				};


		}
	}
}