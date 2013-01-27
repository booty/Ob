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
	public class ThreadsController : ApiController {


		// GET api/threads/5
		public HttpResponseMessage Get(int id, int skip=0, int take=20) {			
			Thread thread;

			int? memberId = HttpContext.Current.MemberId();
			if (memberId.HasValue) {
				var member = Member.Find(memberId.Value);
				thread = Thread.FindThreadWithReplies(member, id, skip, take);
				if (thread==null) return Request.CreateErrorResponse(HttpStatusCode.NotFound, "That thread doesn't exist, or you don't have permission to see it.").WithObApiPrivateDefaults();
				return Request.CreateResponse<Thread>(HttpStatusCode.OK, thread).WithObApiPrivateDefaults();
			}
			else {
				thread = Thread.FindThreadWithReplies(MemberPermissionLevel.Unauthenticated, id, false, skip, take);
				if (thread==null) return Request.CreateErrorResponse(HttpStatusCode.NotFound, "That thread doesn't exist, or you don't have permission to see it.").WithObApiPrivateDefaults();
				return Request.CreateResponse<Thread>(HttpStatusCode.OK, thread).WithObApiPublicDefaults();
			}

			

		}

		// POST api/threads
		public void Post([FromBody]string value) {
		}

		// PUT api/threads/5
		public void Put(int id, [FromBody]string value) {
		}

		// DELETE api/threads/5
		public void Delete(int id) {
		}
	}
}
