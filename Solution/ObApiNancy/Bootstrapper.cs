using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Authentication.Stateless;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Diagnostics;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ObCore.Models;

namespace ObApiNancy {
	public class Bootstrapper : DefaultNancyBootstrapper {
		protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines) {
			base.ApplicationStartup(container, pipelines);
			var statelessAuthConfiguration =
                new StatelessAuthenticationConfiguration(nancyContext => {
						 var authenticationToken = nancyContext.Request.Cookies["authenticationToken"]
							 ?? nancyContext.Request.Form.authenticationToken.Value
							 ?? nancyContext.Request.Query.authenticationToken.Value
							 ?? nancyContext.Request.Headers["ObAuthenticationToken"].FirstOrDefault()
							 ?? nancyContext.Request.Headers["ObAuthentication"].FirstOrDefault();

						 //get the user identity however you choose to (for now, using a static class/method)
						 return MemberAuthentication.GetUserFromApiKey(authenticationToken, nancyContext.Request.UserHostAddress, nancyContext.Request.Url.ToString());
					 });
			StatelessAuthentication.Enable(pipelines, statelessAuthConfiguration);
			AllowAccessToConsumingSite(pipelines);
			StaticConfiguration.EnableRequestTracing = true;
		}

		/// <summary>
		/// Tell browsers they can use Javascript from this domain
		/// </summary>
		/// <param name="pipelines"></param>
		static void AllowAccessToConsumingSite(IPipelines pipelines) {
			pipelines.AfterRequest.AddItemToEndOfPipeline(x => {
				x.Response.Headers.Add("Access-Control-Allow-Origin", "*");
				x.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,DELETE,PUT,OPTIONS");
				if (!x.Response.Headers.ContainsKey("Expires")) {
					x.Response.Headers.Add("Expires",DateTime.UtcNow.AddSeconds(3600).ToString("R"));
				}

				// If Cache-control hasn't been set, set it based on whether or not somebody's logged in
				if (!x.Response.Headers.ContainsKey("Cache-Control")) {
					if (x.CurrentUser == null) {
						x.Response.Headers["Cache-Control"] = "public";
					}
					else {
						x.Response.Headers["Cache-Control"] = "private";
					}
				}

			});
		}

		protected override void ConfigureConventions(NancyConventions conventions) {
			base.ConfigureConventions(conventions);

			conventions.StaticContentsConventions.Add(
				 StaticContentConventionBuilder.AddDirectory("Content", @"Content")
			);
		}

		protected override DiagnosticsConfiguration DiagnosticsConfiguration {
			get {
				return new DiagnosticsConfiguration {
					Password = @"1234"
				};
			}
		}

		protected override void ConfigureApplicationContainer(TinyIoCContainer container) {
			base.ConfigureApplicationContainer(container);
			container.Register(typeof(JsonSerializer), typeof(CustomJsonSerializer));
		}
	}

	public class CustomJsonSerializer : JsonSerializer {
		public CustomJsonSerializer() {
			this.ContractResolver = new CamelCasePropertyNamesContractResolver();
			this.Formatting = Formatting.Indented;
		}
	}
}