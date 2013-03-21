using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using ObCore;
using ObCore.Models;
using Nancy.Cookies;

namespace ObApiNancy {
	public class AuthenticationTokensModule : NancyModule {
		public AuthenticationTokensModule()
			: base("/api/AuthenticationTokens") {
			Get["/{authenticationToken}"] = parameters => {
				AuthenticationResult authResult = ObCore.Security.Authenticate(parameters.authenticationToken, Request.UserHostAddress, Request.Url.ToString());
				if (authResult.Member == null) {
					return Response.AsJson(authResult, HttpStatusCode.OK);
				}
				return Response.AsJson(authResult);
			};

			Post["/"] = p => {
				string login = Request.Form.login.Value;
				string password = Request.Form.password.Value;
				
				if (String.IsNullOrWhiteSpace(login) || String.IsNullOrWhiteSpace(password)) {
					return Response.AsJson("Request body should include values for both 'login' and 'password.'", HttpStatusCode.BadRequest);
				}

				AuthenticationResult authResult = Security.Authenticate(login, password, Request.UserHostAddress, Request.Url.ToString());

				// Success!
				if (authResult.AuthenticationResultCode == Security.AuthenticationResultCode.Success) {
					this.Context.CurrentUser = new OtakuBootyUserIdentity() {
						Member = authResult.Member
					};
					var response = Response.AsJson(authResult);


					response.AddCookie(new NancyCookie("authenticationToken", authResult.AuthenticationToken) {
						Path = "/",
						Expires = DateTime.Now.AddDays(30)
					});
					response.AddCookie(new NancyCookie("idMember", authResult.Member.IdMember.ToString()) {
						Path = "/",
						Expires = DateTime.Now.AddDays(30)
					});
					response.AddCookie(new NancyCookie("login", authResult.Member.Login.ToString()) {
						Path = "/",
						Expires = DateTime.Now.AddDays(30)
					});
					
					return response;
				}

				// Failure!
				return Response.AsJson(authResult, HttpStatusCode.Unauthorized);
			};

		}
	}
}