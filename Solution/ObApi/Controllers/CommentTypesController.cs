using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ObCore.Models;

namespace ObApi.Controllers {
	public class CommentTypesController : ApiController {
		// GET api/commenttype
		/// <summary>
		/// Returns CommentTypes. Comments are "canned" messages you can send to another user.
		/// </summary>
		/// <param name="id">Optional. If omitted, all active comment types will be returned.</param>
		/// <returns></returns>
		public HttpResponseMessage Get(int id=0) {
			int? memberId = HttpContext.Current.MemberId();
			bool includeAdult = false;

			if (memberId.HasValue) {
				var member = Member.Find(memberId.Value);
				includeAdult = member.IsAdult;
			}

			// Return a single comment type
			if (id>0) {
				var comment = CommentType.Find(id);
				if ((comment == null) || (comment.Adult && !includeAdult)) return Request.CreateResponse(HttpStatusCode.NotFound, "That comment type doesn't exist, or maybe you can't see it.");
				return Request.CreateResponse<CommentType>(HttpStatusCode.OK, comment).WithObApiPublicDefaults();
			}

			// Return all comment types visible to this person
			return Request.CreateResponse<List<CommentType>>(HttpStatusCode.OK, CommentType.FindAll(includeAdult, false)).WithObApiPublicDefaults();

		}

		/*
		 // GET api/commenttype/5
		 public string Get(int id)
		 {
			  return "value";
		 }

		 // POST api/commenttype
		 public void Post([FromBody]string value)
		 {
		 }

		 // PUT api/commenttype/5
		 public void Put(int id, [FromBody]string value)
		 {
		 }

		 // DELETE api/commenttype/5
		 public void Delete(int id)
		 {
		 }
		 * */
	}
}
