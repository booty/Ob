using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ObCore.Models;
using System.Collections.Generic;

namespace ObApi.Controllers {
	public class MembersController : ApiController {
		[AcceptVerbs("Get")]
		public string Fart() {
			return "Fart!";
		}

		/// <summary>
		/// Returns information about a member
		/// </summary>
		/// <param name="idMember">The member's numeric ID</param>
		/// <returns>Member profile</returns>
		[AcceptVerbs("Get")]
		public HttpResponseMessage GetByIdMember(string idMember) {
			int id;
			if (Int32.TryParse(idMember, out id)) {
				Member memberProfile = Member.Find(id);
				if (memberProfile!=null) return Request.CreateResponse(HttpStatusCode.OK, memberProfile).WithObApiPublicDefaults();
			}
			return Request.CreateResponse<string>(HttpStatusCode.NotFound, "No member with that past or present login.");
		}

		/// <summary>
		/// Returns information about a member
		/// </summary>
		/// <param name="login">Past OR present login belonging to a member</param>
		/// <returns>Member profile</returns>
		// todo: return a HTTPmoved response if it's not their current login?
		[AcceptVerbs("Get")]
		public HttpResponseMessage GetByLogin(string login) {
			int id;
			Member memberProfile = Member.Find(login);
			if (memberProfile!=null) return Request.CreateResponse(HttpStatusCode.OK, memberProfile).WithObApiPublicDefaults();
			return Request.CreateResponse<string>(HttpStatusCode.NotFound, "No member with that past or present login.");
		}

	}
}
