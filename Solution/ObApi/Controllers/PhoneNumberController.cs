using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using ObApi.Models;



namespace ObApi.Controllers {
	public class PhoneNumberController : ApiController {
		// GET api/phonenumber
		/// <summary>
		/// Get a list of phone numbers visible to a single user
		/// </summary>
		/// <param name="token">Authentication token</param>
		/// <param name="friendsOnly">If true, only show phone numbers for members this member has friended. 
		/// If false, show all phone numbers that others have shared with this member, even if this member
		/// hasn't friended them.</param>
		/// <returns></returns>
		public HttpResponseMessage Get(string token, bool friendsOnly = false) {
			var response = Request.CreateResponse<IEnumerable<PhoneNumber>>(HttpStatusCode.OK, PhoneNumber.Find(1238, friendsOnly));
			//response.Headers.Add("Your-Mom", "Is A Huge Fucking Slut");
			response.Headers.Add("Cache-Control", "private");
			response.Content.Headers.Add("Expires", DateTime.UtcNow.AddMinutes(60).ToString("R"));
			return response;
		}


	}
}
