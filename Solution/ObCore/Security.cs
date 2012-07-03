using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ObCore.Models;

namespace ObCore {
	public static class Security {

		/*
		 ALTER  PROCEDURE [dbo].[Process_Login_Token]
	@Login char(25) output,
	@Pword char(25),
	@ID_Member_Login_Method int,
	@URL varchar(100),
	@IP_Address char(15),
	@RecordLogin bit=1,
	@id_member int=null,
	@login_token varchar(36)=null
		 */


		// todo: return LoginResult object (contains member, error, and/or login token)
		public static Member AttemptLogin(string login, string password, string ipAddress, string url, out string loginToken) {

			int idMember;

			using (var cmd = DataAccess.GetCommandStoredProcedure("Process_Login_Token")) {
				cmd.Parameters.AddWithValue("Login", login).Size = 25;
				cmd.Parameters.AddWithValue("Pword", password).Size = 25;
				cmd.Parameters.AddWithValue("ID_Member_Login_Method", ObCore.LoginMethod.Form);
				cmd.Parameters.AddWithValue("URL", url);
				cmd.Parameters.AddWithValue("IP_Address", ipAddress).Size = 25;
				cmd.Parameters.AddWithValue("RecordLogin", false);
				cmd.Parameters.AddWithValue("login_token", ""); // horrible kludge, shitty sproc expects blank
				/*
				cmd.Parameters.AddWithValue("id_member", DBNull.Value).Direction=ParameterDirection.InputOutput;
				cmd.Parameters["id_member"].Size = 4;
				cmd.Parameters.AddWithValue("login_token", DBNull.Value).Direction=ParameterDirection.InputOutput;
				cmd.Parameters["login_token"].Size = 36;
				*/
				var dr = DataAccess.GetDataRow(cmd);
				if (dr.Table.Columns.Contains("login_token")) {
					loginToken = (string)dr["login_token"];
					idMember = (int)dr["id_member"];
				}
				else {
					loginToken = null;
					return null;
				}

			}
			// Whoops, PetaPoco is weird about nulls
			// var idMember = db.ExecuteScalar<int>("select isnull(dbo.MemberValidate(@0,@1), -1)", login, password);
			using (var db = new ObDb()) {
				//var result = db.Fetch<Member>("select * from MemberBasic where id_member = dbo.MemberValidate(@0,@1)", login, password);
				var result = db.Fetch<Member>("select * from MemberBasic where id_member=@0", idMember);
				if (result.Count == 0) return null;
				return result[0];
			}


			// todo: call proper login Sproc (to update "lastlogin" and stuff and get a real token)
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
