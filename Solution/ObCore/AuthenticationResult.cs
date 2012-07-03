using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ObCore.Models;

namespace ObCore {
	public struct AuthenticationResult {
		public readonly Security.AuthenticationResultCode AuthenticationResultCode;
		public readonly Member Member;
		public readonly string Token;

		public AuthenticationResult(Security.AuthenticationResultCode authenticationResultCode, Member member, string token) {

			Member = member;
			Token = token;

			// Override the caller if they supplied a null member or token, but called it "success"
 			// Should this throw an invalid argument exception instead?
			if ( (authenticationResultCode==Security.AuthenticationResultCode.Success) && ((member == null) || String.IsNullOrWhiteSpace(token)))
				AuthenticationResultCode = Security.AuthenticationResultCode.FailureWhatTheFuckHappened;
			else
				AuthenticationResultCode = authenticationResultCode;
		}
	}
}
