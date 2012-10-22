﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using ObApi.Models;
using ObCore;
using System.Configuration;


namespace ObApi.Controllers {
	public class AuthenticationTokenController : ApiController {

		private const int DefaultAuthCookieTtlDays = 30;  // use this only if value missing from web.config

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
			
			HttpResponseMessage response;
			if (authResult.AuthenticationResultCode == Security.AuthenticationResultCode.Success) {
				var ttlDays = Helpers.ConfigValueOrDefault("AuthenticationCookieTtlDays", DefaultAuthCookieTtlDays);
				
				// Build client response
				response = Request.CreateResponse<AuthenticationResult>(HttpStatusCode.OK, authResult).WithObApiDefaults();
				var cookies = new List<CookieHeaderValue>();
				cookies.Add(new CookieHeaderValue("idMember", authResult.Member.IdMember.ToString()) { Expires = DateTime.UtcNow.AddDays(ttlDays) });
				cookies.Add(new CookieHeaderValue("authenticationToken", authResult.AuthenticationToken) { Expires = DateTime.UtcNow.AddDays(ttlDays) });
				cookies.Add(new CookieHeaderValue("login", authResult.Member.Login) { Expires = DateTime.UtcNow.AddDays(ttlDays) });
				response.Headers.AddCookies(cookies);

				// Save the auth token & member ID in the application cache. 
				if (!(HttpContext.Current.Application["AuthenticationTokens"] is Dictionary<string, int>)) {
					HttpContext.Current.Application["AuthenticationTokens"] = new Dictionary<string, int>();
				}
				((Dictionary<string, int>) HttpContext.Current.Application["AuthenticationTokens"])[authResult.AuthenticationToken] = authResult.Member.IdMember;

			}
			else {
				// unauthorized = Http Status 401
				response = Request.CreateResponse<string>(HttpStatusCode.Unauthorized, authResult.AuthenticationResultDescription).WithObApiDefaults();
				var cookies = new List<CookieHeaderValue>();
				cookies.Add(new CookieHeaderValue("idMember", String.Empty));
				cookies.Add(new CookieHeaderValue("authenticationToken", String.Empty));
				cookies.Add(new CookieHeaderValue("login", String.Empty));
				response.Headers.AddCookies(cookies);
			}

			
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

			var response = Request.CreateResponse<AuthenticationResult>(HttpStatusCode.OK, authResult).WithObApiDefaults();
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