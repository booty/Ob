using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ObCore;
using ObCore.Models;
using Nancy;

namespace ObApiNancy {
	public static class Extensions {
		public static Member CurrentOtakuBootyMember(this NancyContext ctx) {
			if (ctx.CurrentUser == null) return null;
			if (ctx.CurrentUser is OtakuBootyUserIdentity) return ((OtakuBootyUserIdentity)ctx.CurrentUser).Member;
			return null;
		}

		public static MemberPermissionLevel CurrentOtakuBootyPermissionLevel(this NancyContext ctx) {
			Member member = ctx.CurrentOtakuBootyMember();
			if (member == null) return MemberPermissionLevel.Unauthenticated;
			return member.MemberPermissionLevel;
		}

		public static bool CurrentOtakuBootyMemberIsAdult(this NancyContext ctx) {
			Member member = ctx.CurrentOtakuBootyMember();
			if (member == null) return false;
			return member.IsAdult;
		}

		public static Response AsPrivate(this Response r) {
			r.Headers["Cache-Control"] = "private";
			return r;
		}

		public static Response AsPublic(this Response r) {
			r.Headers["Cache-Control"] = "public";
			return r;
		}

		public static int? GetNullableInt(this Nancy.DynamicDictionary dv, string variableName) {
			if (!dv[variableName].HasValue) return null;
			try {
				return (int?)dv[variableName];
			}
			catch {
				return null;
			}

			/*
			if (!ddv.HasValue) return null;
			int i;
			if (Int32.TryParse((string)ddv.Value, out i)) return i;
			*/

		}
	}
}