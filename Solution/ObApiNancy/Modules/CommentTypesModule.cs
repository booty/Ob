using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Security;
using ObCore.Models;


namespace ObApiNancy {
	public class CommentTypesModule: Nancy.NancyModule {
		public CommentTypesModule()
			: base("/api/CommentTypes") {
				

				// Get a list of all comments for this member, newest first 
				Get["/"] = p => {
					// If not logged in, they only get non-adult, active comments
					if (Context.CurrentUser==null) {
						return Response.AsJson(CommentType.FindAll(false, false));
					}
					
					CommentType.Order orderBy = CommentType.Order.ByAdultLast;
					if (Request.Query.orderBy.HasValue) {
						if (Request.Query.orderBy.Value.Equals("name")) orderBy = CommentType.Order.ByName;
						if (Request.Query.orderBy.Value.Equals("adultFirst")) orderBy = CommentType.Order.ByAdultFirst;

					}
					return Response.AsJson(CommentType.FindAll(Context.CurrentOtakuBootyMember().IsAdult, false, orderBy));
					
				};

				Get[@"/(?<idCommentType>\d+)"] = p => {
					int skip, take, id;
					if (!Int32.TryParse(p.idCommentType, out id)) return Response.AsJson("Not found.",HttpStatusCode.NotFound);
					if (Context.CurrentUser == null) {
						return Response.AsJson(CommentType.Find(id, false));
					}
				
					return Response.AsJson(CommentType.Find(id, Context.CurrentOtakuBootyMember().IsAdult));	
				};
		}
	}
}