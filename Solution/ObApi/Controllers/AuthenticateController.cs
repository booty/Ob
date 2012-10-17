using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ObApi.Models;
using ObCore;

namespace ObApi.Controllers {
	public class AuthenticateController : ApiController {
		/*
		public HttpResponseMessage Get(string token, bool friendsOnly = false) {
			var response = Request.CreateResponse<IEnumerable<PhoneNumber>>(HttpStatusCode.OK, PhoneNumber.Find(1238, friendsOnly));
			response.Content.Headers.Add("Cache-Control", "private");
			response.Content.Headers.Add("Expires", DateTime.UtcNow.AddMinutes(60).ToString("R"));
			return response;
		}
		*/

		// GET api/authenticate
		/// <summary>Authenticates a user based on an auth token</summary>
		/// <param name="login">Login, as entered by the user</param>
		/// <param name="password">Password, as entered by the user</param>
		/// <returns>
		/// AuthenticationResult
		/// </returns>
		public HttpResponseMessage Get(string login, string password) {
			var authResult = Security.Authenticate(
				login, 
				password, 
				HttpContext.Current.Request.UserHostAddress, 
				HttpContext.Current.Request.Url.ToString());

			// Todo: What should the HttpStatusCode of the response be if auth fails? See what Google etc. does...
			var response = Request.CreateResponse<AuthenticationResult>(HttpStatusCode.OK, authResult);
			response.Headers.Add("Cache-Control", "private");
			// Todo: Make this value configurable via web.config
			response.Content.Headers.Add("Expires", DateTime.UtcNow.AddMinutes(10).ToString("R"));
			return response;
		}

		// GET api/authenticate
		/// <summary>Authenticates a user based on an auth token</summary>
		/// <param name="authenticationToken">Auth token, presumably stored in user's cookies or elsewhere</param>
		/// <returns></returns>
		public HttpResponseMessage Get(string authenticationToken) {
			var authResult = Security.Authenticate(
				authenticationToken,
				HttpContext.Current.Request.UserHostAddress, 
				HttpContext.Current.Request.Url.ToString());

			// Todo: What should the HttpStatusCode of the response be if auth fails? See what Google etc. does...
			var response = Request.CreateResponse<AuthenticationResult>(HttpStatusCode.OK, authResult);
			response.Headers.Add("Cache-Control", "private");
			// Todo: Make this value configurable via web.config
			response.Content.Headers.Add("Expires", DateTime.UtcNow.AddMinutes(10).ToString("R"));
			return response;
		}

		// POST api/authentication
		public void Post([FromBody]string value) {

		}

		// PUT api/authentication/5
		public void Put(int id, [FromBody]string value) {

		}

		// DELETE api/authentication/5
		public void Delete(int id) {

		}

	}
}
