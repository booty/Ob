using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ObCore.Models;

namespace ObCore {


	public static class Security {

		/// <summary>
		/// Represents why the authentication result attempt occurred. If failure, why?
		/// Kludge: these values must match up to the result codes returned by the sproc Process_Login_Token, except for FailureWhatTheFuckHappenedHere
		/// </summary>
		public enum AuthenticationResultCode {
			Success = 0,
			FailureTooManyLogins = 1,
			FailureMemberBanned = 2,
			FailureIpBanned = 3,
			FailureMemberNotActivated = 4,
			FailureMemberNotFound = 5,
			FailureWhatTheFuckHappened = 999
		}

		public static AuthenticationResultCode GetAuthenticationResultCode(int code) {
			if (Enum.IsDefined(typeof(AuthenticationResultCode), code))
				return (AuthenticationResultCode)code;
			else
				return AuthenticationResultCode.FailureWhatTheFuckHappened;
		}

		/// <summary>
		/// Attempt to authenticate a member via a login token
		/// </summary>
		/// <param name="token">Token, presumably found in their cookies or whatever</param>
		/// <param name="ipAddress">Where dey be?</param>
		/// <param name="url">URL they're accessing</param>
		/// <returns></returns>
		public static AuthenticationResult Authenticate(string token, string ipAddress, string url) {
			var da = new DataAccess();
			using (var cmd = da.GetCommandStoredProcedure("Process_Login_Token")) {
				cmd.Parameters.AddWithValue("Login", DBNull.Value).Size = 25;
				cmd.Parameters.AddWithValue("Pword", DBNull.Value).Size = 25;
				cmd.Parameters.AddWithValue("ID_Member_Login_Method", ObCore.LoginMethod.Form);
				cmd.Parameters.AddWithValue("URL", url);
				cmd.Parameters.AddWithValue("IP_Address", ipAddress).Size = 25;
				cmd.Parameters.AddWithValue("RecordLogin", false);
				cmd.Parameters.AddWithValue("login_token", token); // horrible kludge, shitty sproc expects blank
				var dr = da.GetDataRow(cmd);
				return DataRowToAuthResult(dr);
			}
		}

		/// <summary>
		/// Attempt to authenticate a member via a username+pass
		/// </summary>
		/// <param name="login">Member's login. Note: either a current or previous login (from the name change feature) can be used</param>
		/// <param name="password">The member's password, surprisingly</param>
		/// <param name="ipAddress">Where dey be?</param>
		/// <param name="url">URL they're accessing</param>
		/// <returns></returns>
		public static AuthenticationResult Authenticate(string login, string password, string ipAddress, string url) {
			var da = new DataAccess();
			using (var cmd = da.GetCommandStoredProcedure("Process_Login_Token")) {
				cmd.Parameters.AddWithValue("Login", login);
				cmd.Parameters.AddWithValue("Pword", password);
				cmd.Parameters.AddWithValue("ID_Member_Login_Method", ObCore.LoginMethod.Form);
				cmd.Parameters.AddWithValue("URL", url);
				cmd.Parameters.AddWithValue("IP_Address", ipAddress);
				cmd.Parameters.AddWithValue("RecordLogin", false);
				cmd.Parameters.AddWithValue("login_token", ""); // horrible kludge, shitty sproc expects blank
				var dr = da.GetDataRow(cmd);
				return DataRowToAuthResult(dr);
			}

		}

		private static AuthenticationResult DataRowToAuthResult(System.Data.DataRow dr) {
			if (dr.Table == null) {
				// we don't really know what happened. stupid sproc!
				return new AuthenticationResult(AuthenticationResultCode.FailureWhatTheFuckHappened, null, null);
			}

			if (!dr.Table.Columns.Contains("login_token")) {
				if (dr.Table.Columns.Contains("result")) {
					// at least the stupid sproc gave us a reason
					return new AuthenticationResult((AuthenticationResultCode)dr["result"], null, null);
				}
				else {
					// we don't really know what happened
					return new AuthenticationResult(AuthenticationResultCode.FailureWhatTheFuckHappened, null, null);
				}
			}

			// Hooray! Return the member 
			return new AuthenticationResult(AuthenticationResultCode.Success, Member.Find((int)dr["id_member"]), (string)dr["login_token"]);
		}

		/// <summary>
		/// Checks whether a given member is authorized to perform the specified action.
		/// </summary>
		/// <param name="member">A Member. Can be null.</param>
		/// <param name="authorizationRequirement">Requirement to check.</param>
		/// <returns></returns>
		public static bool IsAuthorized(Member member, ObCore.AuthorizationRequirement authorizationRequirement) {
			// Nothing to do!
			if (authorizationRequirement == AuthorizationRequirement.NoRequirement) return true;

			// If we don't actually have a member...
			if (member == null) {
				if (authorizationRequirement == AuthorizationRequirement.IsNotAuthenticated) return true;
				return false;
			}

			// If they're authenticated...
			switch (authorizationRequirement) {
				case AuthorizationRequirement.IsAuthenticated:
					return true;
				case AuthorizationRequirement.IsUberModOrHigher:
					return member.IsUberMod;
				case AuthorizationRequirement.HasPaidMemberPriviledges:
					return member.IsPaidOrLifetimeMember;
				case AuthorizationRequirement.IsFreeMember:
					return !member.IsPaidOrLifetimeMember;
				case AuthorizationRequirement.IsCustomerServiceRepresentative:
					return member.IsCustomerServiceRepresentative;
				case AuthorizationRequirement.IsCustomerServiceRepresentativeAdmin:
					return member.IsCustomerServiceRepresentativeAdmin;
				case AuthorizationRequirement.IsModOrHigher:
					return member.IsMod;
				default:
					// todo: Implement missing authorization requirements 
					throw new NotImplementedException();
			}

		}
	}
}
