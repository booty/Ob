using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using ObCore.Models;

namespace Clitter {
	public static class Extensions {
		public static Member CurrentMember(this NancyContext nancyContext) {
			if (nancyContext.CurrentUser == null) return null;
			return (Member)nancyContext.CurrentUser;
		}

		public static int? CurrentEmployeeId(this NancyContext nancyContext) {
			if (nancyContext.CurrentUser == null) return null;
			return nancyContext.CurrentMember().IdMember;
		}
	}
}