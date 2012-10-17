using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ObCore.Models;

namespace ObCore {
	public struct AuthenticationResult {
		public readonly Security.AuthenticationResultCode AuthenticationResultCode;
		public readonly string AuthenticationResultDescription;
		public readonly Member Member;
		public readonly string AuthenticationToken;

		public AuthenticationResult(Security.AuthenticationResultCode authenticationResultCode, Member member, string authenticationToken) {

			Member = member;
			AuthenticationToken = authenticationToken;
			AuthenticationResultDescription = Security.AuthenticationResultDescription(authenticationResultCode);

			// Override the caller if they supplied a null member or token, but called it "success"
 			// Should this throw an invalid argument exception instead?
			// todo: Why the fuck are we doing this?
			if ( (authenticationResultCode==Security.AuthenticationResultCode.Success) && ((member == null) || String.IsNullOrWhiteSpace(authenticationToken)))
				AuthenticationResultCode = Security.AuthenticationResultCode.FailureWhatTheFuckHappened;
			else
				AuthenticationResultCode = authenticationResultCode;
		}
	}
}
