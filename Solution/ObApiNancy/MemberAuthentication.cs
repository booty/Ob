using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.Security;
using ObCore;
using ObCore.Models;

namespace ObApiNancy {
	public class MemberAuthentication {

		public static IUserIdentity GetUserFromApiKey(string authenticationToken, string ipAddress, string url) {
			if ((authenticationToken==null) || (String.IsNullOrWhiteSpace(authenticationToken))) return null;
			
			// check auth token - if not valid, return null
			// TODO: Cache auth token lookups and/or members?
			var authResult = ObCore.Security.Authenticate(authenticationToken, ipAddress, url);
			if (authResult.AuthenticationResultCode == Security.AuthenticationResultCode.Success) {
				return new OtakuBootyUserIdentity() {
					UserName = authResult.Member.Login,
					Member = authResult.Member
				};
			}

			return null;
		}

	}

	public class OtakuBootyUserIdentity : IUserIdentity {
		private Member _member;

		public string UserName {
			get;
			set;
		}

		public Member Member {
			set {
				_member = value;
				UserName = value.Login;
				var c = new List<string>();
				if (_member.IsAdmin) c.Add(OtakuBootyClaims.ADMIN);
				if (_member.IsAdult) c.Add(OtakuBootyClaims.ADULT);
				if (_member.IsCustomerServiceRepresentative) c.Add(OtakuBootyClaims.CSR);
				if (_member.IsCustomerServiceRepresentativeAdmin) c.Add(OtakuBootyClaims.CSR_ADMIN);
				if (_member.IsMod) c.Add(OtakuBootyClaims.MODERATOR);
				if (_member.IsPaidMember) c.Add(OtakuBootyClaims.PAID);
				if (_member.IsPaidOrLifetimeMember) c.Add(OtakuBootyClaims.PAID_OR_LIFETIME);
				if (_member.IsPicApprover) c.Add(OtakuBootyClaims.PICTURE_APPROVER);
				if (_member.IsProfileApprover) c.Add(OtakuBootyClaims.PROFILE_APPROVER);
				if (_member.IsSysAdmin) c.Add(OtakuBootyClaims.SYS_ADMIN);
			}
			get {
				return _member;
			}
		}

		// todo: implement claims
		public IEnumerable<string> Claims {
			get;
			set;
		}
	}
}