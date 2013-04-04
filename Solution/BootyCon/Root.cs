using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace BootyCon {
	public class Root: NancyModule {
		public Root() {

			Get["/"] = parameters => {
				return View["index.cshtml"];
			};
		}
	}
}