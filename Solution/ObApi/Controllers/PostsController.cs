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
	public class PostsController : ApiController {
		// GET api/post
		//public IEnumerable<string> Get() {
		public string Get() {
			return "Not implemented!";
		}

		// GET api/post/5
		public HttpResponseMessage Get(int id) {
			// Assume viewer is unauth'd and non-adult
			MemberPermissionLevel effectiveMpl = MemberPermissionLevel.Unauthenticated;
			bool includeAdult=false;

			int? memberId = HttpContext.Current.MemberId();
			if (memberId.HasValue) {
				var member = Member.Find(memberId.Value);
				effectiveMpl = member.MemberPermissionLevel;
				includeAdult = member.IsAdult;
			}

			Post post = ObCore.Models.Post.Find(id, effectiveMpl, includeAdult, false);
			if (post == null) {
				return Request.CreateResponse(HttpStatusCode.NotFound, "That post doesn't exist, or perhaps you're not allowed to see it?");
			}
			else {
				return Request.CreateResponse<Post>(HttpStatusCode.OK, post).WithObApiPublicDefaults();
			}
		}

		// POST api/post
		public void Post([FromBody]string value) {
		}

		// PUT api/post/5
		public void Put(int id, [FromBody]string value) {
		}

		// DELETE api/post/5
		public void Delete(int id) {
		}
	}
}
