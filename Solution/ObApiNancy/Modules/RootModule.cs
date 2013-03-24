using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace ObApiNancy {
	public class RootModule : NancyModule {
		public RootModule() {
			Get["/"] = _ => {
				return View["Index.html"];
			};

			Get["/Help"] = _ => {
				return View["Docs.html"];
			};
		}
	}
}