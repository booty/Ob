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
using Clitter.Models;
using ObCore;

namespace Clitter {
	public class Bootstrapper : DefaultNancyBootstrapper {
		protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines) {
			base.ApplicationStartup(container, pipelines);
			var statelessAuthConfiguration =
                new StatelessAuthenticationConfiguration(nancyContext => {
						 var authenticationToken = nancyContext.Request.Cookies["ObAuthenticationToken"]
							 ?? nancyContext.Request.Query.ObAuthenticationToken.Value;

						 //get the user identity however you choose to (for now, using a static class/method)
						 if (authenticationToken == null) return null;
						 AuthenticationResult foo = ObCore.Security.Authenticate(authenticationToken, 
							 nancyContext.Request.UserHostAddress, 
							 nancyContext.Request.Url.ToString());
						 // todo: probably won't work; probably need a constructor for Clitter.Models.Member that takes a ObCore.Member as an argument
						 return (Clitter.Models.Member)foo.Member;
						 
					 });
			StatelessAuthentication.Enable(pipelines, statelessAuthConfiguration);
			StaticConfiguration.EnableRequestTracing = true;
		}
	}
}