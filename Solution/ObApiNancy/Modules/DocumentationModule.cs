using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Routing;

namespace ObApiNancy.Modules {
	public class DocumentationModule : NancyModule {
		public DocumentationModule(IRouteCacheProvider routeCacheProvider)
			: base("/docs") {
			Get["/"] = p => {
				var foo = routeCacheProvider.GetCache();

				return View["routes.cshtml", routeCacheProvider.GetCache()];

			};
		}
	}
}