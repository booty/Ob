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
	/// <summary>
	/// Faggot sauce!
	/// </summary>
	public class MemberPicturesController : ApiController {
		// GET api/memberpictures
		//public HttpResponseMessage Get(int? skip=0, int? take=25) {
		/*
		 * public HttpResponseMessage Get() {
			
		}
		 * */

		// GET api/memberpictures/5
		/// <summary>
		/// One or more member pictures.
		/// </summary>
		/// <param name="id">Optional. Use this if you're just requesting a single picture. Either the picture's numeric ID (for public pictures) or the GUID (for Friends-Only pictures).</param>
		/// <param name="skip">How many pictures to skip? Use this for paging, etc. If you want to start at the 51st picture, then skip=50. Ignored if you specify "id"</param>
		/// <param name="take">How many pictures to return? Use this for paging, etc. Ignored if you specify "id". May be capped.</param>
		/// <returns></returns>
		public HttpResponseMessage Get(string id="", int? skip=0, int? take=25) {

			int idPictureMember;
			MemberPicture mp;
			
			// Public pic (id is a valid int)
			if (Int32.TryParse(id, out idPictureMember)) {
				mp= MemberPicture.FindPublicPicture(idPictureMember, true);
				if (mp == null) return Request.CreateResponse(HttpStatusCode.NotFound, "That profile picture doesn't exist, or can't be viewed.").WithObApiPublicDefaults();
				return Request.CreateResponse<MemberPicture>(HttpStatusCode.OK, mp).WithObApiPublicDefaults();
			}
			
			
			// Range of pics (default: take from all public pics, newest first)
			if (id == "") {
				if (take > 100) take = 100;  // todo: move this magic number to web.config
				return Request.CreateResponse(HttpStatusCode.OK, MemberPicture.FindPublicPictures(skip.Value,take.Value,true)).WithObApiPublicDefaults();
			}
			
			// Fop (id is something else; presumably a string). Auth is required.
			int? memberId = HttpContext.Current.MemberId();
			if (!memberId.HasValue) return Request.CreateMissingAuthorizationTokenResponse();
			var member = Member.Find(memberId.Value);
			if (member == null) return Request.CreateMissingAuthorizationTokenResponse();
			return Request.CreateResponse(HttpStatusCode.OK, member.GetFriendsOnlyPicture(id)).WithObApiPrivateDefaults();
			
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
