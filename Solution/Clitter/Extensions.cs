using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clitter.Models;
using Nancy;

namespace Clitter {
	public static class Extensions {
		public static Clitter.Models.Member CurrentMember(this NancyContext nancyContext) {
			if (nancyContext.CurrentUser == null) return null;
			return (Member)nancyContext.CurrentUser;
		}

		public static int? CurrentEmployeeId(this NancyContext nancyContext) {
			if (nancyContext.CurrentUser == null) return null;
			return nancyContext.CurrentMember().IdMember;
		}
	}
}