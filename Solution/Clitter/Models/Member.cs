using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.Security;
using ObCore.Models;

namespace Clitter.Models {
	public class Member : ObCore.Models.Member, IUserIdentity {
		#region IUserIdentity Members

		public IEnumerable<string> Claims {
			get {
				var claims = new List<string>(5);
				if (this.IsPaidOrLifetimeMember) claims.Add(Clitter.Claims.PaidOrLifetime);
				if (this.IsMod) claims.Add(Clitter.Claims.Moderator);
				if (this.IsAdult) claims.Add(Clitter.Claims.Adult);
				if (this.IsAdmin) claims.Add(Clitter.Claims.Admin);
				if (this.BootyCon2013) claims.Add(Clitter.Claims.BootyCon2013);

				throw new NotImplementedException();
			}
		}

		public string UserName {
			get {
				return this.Login;
			}
		}

		#endregion
	}
}