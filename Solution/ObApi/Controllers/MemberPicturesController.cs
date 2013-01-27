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
	public class MemberPicturesController : ApiController {
		// GET api/memberpictures
		//public HttpResponseMessage Get(int? skip=0, int? take=25) {
		/*
		 * public HttpResponseMessage Get() {
			
		}
		 * */

		// GET api/memberpictures/5
		public HttpResponseMessage Get(string id="", int? skip=0, int? take=25) {
			int idPictureMember;
			MemberPicture mp;
			if (Int32.TryParse(id, out idPictureMember)) {
				mp= MemberPicture.FindPublicPicture(idPictureMember, true);
				if (mp == null) return Request.CreateResponse(HttpStatusCode.NotFound, "That profile picture doesn't exist, or can't be viewed.").WithObApiPublicDefaults();
				return Request.CreateResponse<MemberPicture>(HttpStatusCode.OK, mp).WithObApiPublicDefaults();
			}
			else {
				if (id == "") {
					return Request.CreateResponse(HttpStatusCode.OK, MemberPicture.FindPublicPictures(skip.Value,take.Value,true)).WithObApiPublicDefaults();
				}
				else {
					int? memberId = HttpContext.Current.MemberId();
					if (!memberId.HasValue) return Request.CreateMissingAuthorizationTokenResponse();
					var member = Member.Find(memberId.Value);
					if (member == null) return Request.CreateMissingAuthorizationTokenResponse();
					return Request.CreateResponse(HttpStatusCode.OK, member.GetFriendsOnlyPicture(id)).WithObApiPrivateDefaults();
				}
			}
		}

		/*
		// POST api/memberpictures
		public void Post([FromBody]string value) {
		}

		// PUT api/memberpictures/5
		public void Put(int id, [FromBody]string value) {
		}

		// DELETE api/memberpictures/5
		public void Delete(int id) {
		}
		 * */
	}
}
