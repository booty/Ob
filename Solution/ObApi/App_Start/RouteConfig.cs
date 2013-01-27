using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ObApi {
	public class RouteConfig {
		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			

			/*
			routes.MapRoute(
				name: "Forum Threads",
				url: "Forums/{id}/Threads",
				defaults: new {
					controller= "Forums",
					action= "GetThreads"
			});
			 * */

			routes.MapRoute(
				name: "Forums",
				url: "Forums/{id}",
				defaults: new {
					controller = "Forums",
					action="Get",
					id=UrlParameter.Optional,
					skip=0,
					take=25,
					includeSticky=false,
					includeAdult=false
				}
			);

			routes.MapRoute(
				name: "Private Message (multiple)", 
				url: "PrivateMessages/", 
				defaults: new {
					controller = "PrivateMessages",
					action = "Get"
			});

			routes.MapRoute(
				name: "Private Message (single)",
				url: "PrivateMessages/{id}",
				defaults: new {
					controller="PrivateMessages",
					action="GetSingle"
				}
			);

			routes.MapRoute(
				 name: "Default",
				 url: "{controller}/{action}/{id}",
				 defaults: new {
					 controller = "Home",
					 action = "Index",
					 id = UrlParameter.Optional
				 }
			);
		}
	}
}