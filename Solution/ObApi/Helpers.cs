using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using ObCore;

namespace ObApi {
	public static class Helpers {
		public static HttpResponseMessage CreateMissingAuthorizationTokenResponse(this HttpRequestMessage hrm) {
			var result = hrm.CreateErrorResponse(HttpStatusCode.Unauthorized, ConfigurationManager.AppSettings["BadOrMissingAuthorizationTokenMessage"]).WithObApiPublicDefaults();
			//var result = hrm.CreateErrorResponse(HttpStatusCode.Unauthorized, ConfigurationManager.AppSettings["BadOrMissingAuthorizationTokenMessage"]).WithObApiDefaults();
			//result.ReasonPhrase = ConfigurationManager.AppSettings["BadOrMissingAuthorizationTokenMessage"];
			result.Content.Headers.Remove("Content-Type");
			result.Content.Headers.Add("Content-Type", "text/plain");
			return result;
		}


		/// <summary>
		/// Adds default headers to HttpResponse
		/// </summary>
		/// <param name="httpResponseMessage">The response we're adding these headers to</param>
		/// <returns>The same HttpResponseMessage, with added headers</returns>
		public static HttpResponseMessage WithObApiPublicDefaults(this HttpResponseMessage httpResponseMessage) {
			httpResponseMessage.Headers.Add("Cache-Control", "public");
			httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");
			httpResponseMessage.Content.Headers.Add("Expires", DateTime.UtcNow.AddSeconds(ApiResponseTtlSeconds()).ToString("R"));
			return httpResponseMessage;
		}

		/// <summary>
		/// Adds default headers to HttpResponse
		/// </summary>
		/// <param name="httpResponseMessage">The response we're adding these headers to</param>
		/// <returns>The same HttpResponseMessage, with added headers</returns>
		public static HttpResponseMessage WithObApiPrivateDefaults(this HttpResponseMessage httpResponseMessage) {
			httpResponseMessage.Headers.Add("Cache-Control", "private");
			httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");
			httpResponseMessage.Content.Headers.Add("Expires", DateTime.UtcNow.AddSeconds(ApiResponseTtlSeconds()).ToString("R"));
			return httpResponseMessage;
		}

		/// <summary>
		/// Checks the message request for a valid authentication token and returns MemberId.
		/// First checks an Application object cache for the member ID, if not found it checks the database
		/// If the token is bad, it is removed from the client's cookies collection
		/// </summary>
		/// <param name="ctx">An HttpContext (presumably the current one obtained with HttpContext.Current</param>
		/// <returns>The member Id, or null</returns>
		public static int? MemberId(this HttpContext ctx) {
			string authToken = ctx.Request.Headers["ObAuthenticationToken"] ?? ctx.Request.Headers["ObAuthentication"] ?? ctx.Request["authenticationToken"];

			// if they didn't supply one, return null -- nothing else to do, obviously
			if (String.IsNullOrEmpty(authToken) || String.IsNullOrWhiteSpace(authToken)) return null;

			// Look in app cache for valid auth token, creating the cache if necessary
			Dictionary<string, int> memberIdCache;
			if (ctx.Application["AuthenticationTokens"] is Dictionary<string, int>) {
				memberIdCache = (Dictionary<string, int>)ctx.Application["AuthenticationTokens"];
				if (memberIdCache.ContainsKey(authToken)) return memberIdCache[authToken];  // success! we're done
			}
			else {
				ctx.Application["AuthenticationTokens"] = new Dictionary<string, int>();
				memberIdCache = (Dictionary<string, int>)ctx.Application["AuthenticationTokens"];
			}

			
			var da = new DataAccess();
			var cmd = da.GetCommand("select id_member from login_token where login_token=@login_token");
			cmd.Parameters.AddWithValue("@login_token", authToken);
			int? idMember = da.GetScalarInt(cmd);
			if (idMember.HasValue) {
				// Success! Cache the successful result
				memberIdCache[authToken] = idMember.Value;
			}
			else {
				// This is a bad token. Remove it from the client's cookies, if it's there
				ctx.Request.Cookies.Remove("authenticationToken");
			}
			return idMember;
		}
		

		public static int ApiResponseTtlSeconds() {
			return ConfigValueOrDefault("ApiResponseExpirationSeconds", 30);
		}

		/*
		public static string ApiResponseExpirationDateTime {
			get {
				return DateTime.UtcNow.AddSeconds(ApiResponseTtlSeconds()).ToString("R");
			}
		}	
		 * */

		public static string ConfigValueOrDefault(string keyName, string defaultValue) {
			return ConfigurationManager.AppSettings[keyName] ?? defaultValue;
		}

		public static int ConfigValueOrDefault(string keyName, int defaultValue) {
			int returnVal;
			if (Int32.TryParse(ConfigurationManager.AppSettings[keyName], out returnVal)) return returnVal;
			return defaultValue;
		}
	}
}