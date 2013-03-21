using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace ObApi {
	public class RouteConfig {
		public static void RegisterRoutes(System.Web.Http.HttpRouteCollection routes) {

			//routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			/*
			routes.MapHttpRoute(
				name: "Fart 1!",
				routeTemplate: "api/Members/Fart",
				defaults: new {
					controller = "MembersController",
					action = "Fart"
				}
			);
			*/

			// I guess somebody is fucked if their login is also a valid integer! 
			routes.MapHttpRoute(
				name: "Member (by id)",
				routeTemplate: "api/Members/1238",
				defaults: new {
					controller = "Members",
					action = "GetByIdMember"
				},
				constraints: new {
					id = @"\d+"
				}
			);

			routes.MapHttpRoute(
				name: "Member (by login)",
				routeTemplate: "api/Members/{login}",
				defaults: new {
					controller = "Members",
					action = "GetByLogin"
				}
			);

			routes.MapHttpRoute(
				name: "Private Message (multiple)", 
				routeTemplate: "PrivateMessages/", 
				defaults: new {
					controller = "PrivateMessages",
					action = "Get"
			});

			routes.MapHttpRoute(
				name: "Private Message (single)",
				routeTemplate: "PrivateMessages/{id}",
				defaults: new {
					controller="PrivateMessages",
					action="GetSingle"
				}
			);

			routes.MapHttpRoute(
				 name: "Default API",
				 routeTemplate: "{controller}/{action}/{id}",
				 defaults: new {
					 controller = "Home",
					 action = "Index",
					 id = UrlParameter.Optional
				 }
			);
			
		}
	}
}