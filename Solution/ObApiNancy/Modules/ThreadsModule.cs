using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using ObCore.Models;


namespace ObApiNancy {
	public class ThreadsModule : Nancy.NancyModule {
		public int? GetNullableInt(DynamicDictionary dd, string key) {
			if (!dd.ContainsKey(key)) return null;
			try {
				return (int)dd[key];
			}
			catch {
				return null;
			}

		}

		public ThreadsModule()
			: base("/api/threads") {
				Get[@"/(?<idThread>\d+)"] = p => {
					int? skip = GetNullableInt(Request.Query, "skip") ?? 42;
					int? take = GetNullableInt(Request.Query, "take") ?? 666;
					
					return String.Format("You want thread #{0} skip {1} take {2}", p.idThread, skip, take);
				};
		}
	}
}