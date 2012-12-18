using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using ObApi.Models;



namespace ObApi.Controllers {
	public class PhoneNumbersController : ApiController {
		// GET api/phonenumber
		/// <summary>
		/// Get a list of phone numbers visible to a single user
		/// </summary>
		/// <param name="friendsOnly">If true, only show phone numbers for members this member has friended. 
		/// If false, show all phone numbers that others have shared with this member, even if this member
		/// hasn't friended them.</param>
		/// <returns></returns>
		public HttpResponseMessage Get(bool friendsOnly = false) {
			int? memberId = HttpContext.Current.MemberId();
			if (!memberId.HasValue) return Request.CreateMissingAuthorizationTokenResponse();
			return Request.CreateResponse<IEnumerable<PhoneNumber>>(HttpStatusCode.OK, PhoneNumber.Find(memberId.Value, friendsOnly)).WithObApiPublicDefaults();
		}


	}
}
