using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ObCore.Models;

namespace ObApi.Controllers {
	public class MemberController : ApiController {
		// /api/member/JohnBooty
		public HttpResponseMessage Get(string id) {
			int memberId;
			Member m;
			// todo: I guess somebody is fucked if their login is also a valid integer! oh well!
			m = Int32.TryParse(id, out memberId) ? Member.Find(memberId) : Member.Find(id);
			if (m == null) return Request.CreateResponse<string>(HttpStatusCode.NotFound, "No member with that past or present login.");
			return Request.CreateResponse(HttpStatusCode.OK, m).WithObApiDefaults();
		}

	}
}
