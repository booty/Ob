using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ObCore.Models;
using System.Collections.Generic;

namespace ObApi.Controllers {
	public class MembersController : ApiController {
		public class MemberProfile {
			public Member Member;
			public List<MemberPicture> PublicPictures;
			public List<MemberPicture> FriendsOnlyPictures;
		}


		/// <summary>
		/// Returns information about one or more members.
		/// </summary>
		/// <param name="id">Can be either numeric memberId (1238) or the user's past/present login ("John Booty")</param>
		/// <returns>Member profile - profile information, pictures</returns>
		public HttpResponseMessage Get(string id) {
			int memberId;

			MemberProfile memberProfile = new MemberProfile();

			// Try to find member; bomb out if we can't. If "id" is a valid int, search by member ID, otherwise
			// search by login.
			// I guess somebody is fucked if their login is also a valid integer! 
			// Not going to hit the database twice for those people.
			memberProfile.Member = Int32.TryParse(id, out memberId) ? Member.Find(memberId) : Member.Find(id);
			if (memberProfile.Member == null) return Request.CreateResponse<string>(HttpStatusCode.NotFound, "No member with that past or present login.");
			return Request.CreateResponse(HttpStatusCode.OK, memberProfile).WithObApiPublicDefaults();
		}

	}
}
