using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ObCore;
using ObCore.Models;

namespace ObApi.Controllers {
	public class ForumsController : ApiController {
		/// <summary>
		/// Lists forums.
		/// </summary>
		/// <returns>A list of forums, or (if "id" is supplied) a single forum with a list of threads</returns>
		public HttpResponseMessage Get(int id=0, int skip = 0, int take = 25, string includeSticky = "true") {
			int? memberId = HttpContext.Current.MemberId();
			bool sticky = (!includeSticky.Equals("false") && !includeSticky.Equals("0"));
	
			if (id>0) {
				// User specified a single forum; list the threads in that forum 
				Forum result;

				if (memberId.HasValue) {
					MemberPermissionLevel? mpm = Member.PermissionLevel(memberId.Value);
					// todo: respect adult-ness of current member
					if (mpm.HasValue)
						result = Forum.FindWithThreads(mpm.Value, id, true, false, skip, take);
					else
						result = Forum.FindWithThreads(MemberPermissionLevel.Unauthenticated, id, false, sticky, skip, take);
				}
				else {
					result = Forum.FindWithThreads(MemberPermissionLevel.Unauthenticated, id, false, sticky, skip, take);
				}
				if (result == null) return Request.CreateResponse(HttpStatusCode.NotFound, "That forum doesn't exist, or you don't have permission to view it.");
				return Request.CreateResponse<Forum>(HttpStatusCode.OK, result).WithObApiPublicDefaults();
			}
			else {
				// User didn't supply a forum; just list all forums
				if (memberId.HasValue) {
					var mpl = Member.PermissionLevel(memberId.Value);
					if (mpl.HasValue) return Request.CreateResponse<List<Forum>>(HttpStatusCode.OK, Forum.FindNoPoco(mpl.Value)).WithObApiPrivateDefaults();
				}
				return Request.CreateResponse<List<Forum>>(HttpStatusCode.OK, Forum.FindNoPoco(MemberPermissionLevel.Unauthenticated)).WithObApiPublicDefaults();
				//return Request.CreateResponse<List<Forum>>(HttpStatusCode.OK, Forum.Find(MemberPermissionLevel.Unauthenticated)).WithObApiPublicDefaults();
			}
		}

		// POST api/forum
		public void Post([FromBody]string value) {
		}

		// PUT api/forum/5
		public void Put(int id, [FromBody]string value) {
		}

		// DELETE api/forum/5
		public void Delete(int id) {
		}
	}
}
