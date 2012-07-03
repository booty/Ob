using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using PetaPoco;

namespace ObCore.Models {

	[TableName("MemberBasic")]
	[PetaPoco.PrimaryKey("id_member")]
	[ExplicitColumns]
	public class Member : ObDb.Record<Member> {

		#region Stuff PetaPoco added
		[Required]
		[PetaPoco.Column("ID_Member")]
		public int IdMember { get; set; }
		[Required]
		[PetaPoco.Column]
		public string Login { get; set; }

		[PetaPoco.Column("About_Self")]
		[DisplayName("About")]
		public string AboutSelf { get; set; }

		[PetaPoco.Column]
		public int? Age { get; set; }

		[Required]
		[PetaPoco.Column]
		public string Gender { get; set; }

		[PetaPoco.Column]
		public string City { get; set; }

		[PetaPoco.Column]
		public string State { get; set; }

		[PetaPoco.Column]
		public string Country { get; set; }

		[PetaPoco.Column("latitude")]
		public double? Latitude { get; set; }

		[PetaPoco.Column("longitude")]
		public double? Longitude { get; set; }

		[PetaPoco.Column("id_picture_member")]
		public int? IdPictureMember { get; set; }

		[PetaPoco.Column("Current_Relationship_Description_Others")]
		[DisplayName("Current Relationship")]
		public string CurrentRelationshipDescriptionOthers { get; set; }

		[PetaPoco.Column("Relationship_Desired_Description_Others")]
		[DisplayName("Relationship Desired")]
		public string RelationshipDesiredDescriptionOthers { get; set; }

		[PetaPoco.Column("logins_previous")]
		[DisplayName("Previously Known As")]
		public string LoginsPrevious { get; set; }

		[PetaPoco.Column("lifetime_member")]
		[DisplayName("Lifetime Member?")]
		public bool? LifetimeMember { get; set; }

		[DisplayName("Paid Member?")]
		public int IsPaidMember { get; set; }

		[PetaPoco.Column]
		[DisplayName("Paid or Lifetime Member?")]
		public bool IsPaidOrLifetimeMember { get; set; }
		
		[PetaPoco.Column]
		[DisplayName("Is Moderator?")]
		public bool IsMod { get; set; }
		
		[PetaPoco.Column]
		[DisplayName("Is UberModerator?")]
		public bool IsUberMod { get; set; }
		
		[PetaPoco.Column]
		[DisplayName("Is SysAdmin?")]
		public bool IsSysAdmin { get; set; }
		
		[PetaPoco.Column]
		[DisplayName("Is a Customer Service Rep?")]
		public bool IsCustomerServiceRepresentative { get; set; }

		[PetaPoco.Column]
		[DisplayName("Is a Customer Server Rep Admin?")]
		public bool IsCustomerServiceRepresentativeAdmin { get; set; }

		[PetaPoco.Column]
		[DisplayName("Can approve member-uploaded pictures?")]
		public bool IsPicApprover { get; set; }

		[PetaPoco.Column]
		[DisplayName("Can approve member profiles?")]
		public bool IsProfileApprover { get; set; }

		[Required]
		[DisplayName("Last Visit")]
		[PetaPoco.Column("last_login")]
		public DateTime LastLogin { get; set; }

		[PetaPoco.Column("Last_Login_Relative")]
		[DisplayName("Last Visit")]
		public string LastLoginRelative { get; set; }

		[PetaPoco.Column("last_active")]
		[DisplayName("Last Active")]
		public DateTime? LastActive { get; set; }

		[PetaPoco.Column("Last_Active_Relative")]
		[DisplayName("Last Active")]
		public string LastActiveRelative { get; set; }

		[Required]
		[PetaPoco.Column("Joined_Site")]
		[DisplayName("Joined")]
		public DateTime JoinedSite { get; set; }

		[PetaPoco.Column("Joined_Site_Relative")]
		[DisplayName("Joined")]
		public string JoinedSiteRelative { get; set; }

		[Required]
		[PetaPoco.Column("Likes_Females")]
		[DisplayName("Interested In Females?")]
		public bool LikesFemales { get; set; }

		[Required]
		[PetaPoco.Column("Likes_Males")]
		[DisplayName("Interested In Men?")]
		public bool LikesMales { get; set; }

		[PetaPoco.Column("Gender_Preference")]
		[DisplayName("Gender Preference")]
		public string GenderPreference { get; set; }

		[PetaPoco.Column("Your_Dream_Job")]
		[DisplayName("Your Dream Job")]
		public string YourDreamJob { get; set; }

		[PetaPoco.Column("Anime_Hobbies")]
		[DisplayName("Anime_Hobbies")]
		public string AnimeHobbies { get; set; }

		[PetaPoco.Column("Conventions")]
		[DisplayName("Conventions")]
		public string Conventions { get; set; }

		[PetaPoco.Column("Music")]
		[DisplayName("Music")]
		public string Music { get; set; }

		[PetaPoco.Column("Something_Funny_You_Own")]
		[DisplayName("Something Funny You Own")]
		public string SomethingFunnyYouOwn { get; set; }

		[PetaPoco.Column("Other_Interests")]
		[DisplayName("Other Interests")]
		public string OtherInterests { get; set; }

		[PetaPoco.Column("Favorite_Anime_Manga")]
		[DisplayName("Favorite Anime or Manga")]
		public string FavoriteAnimeManga { get; set; }

		[PetaPoco.Column("Favorite_Games")]
		[DisplayName("Favorite Games")]
		public string FavoriteGames { get; set; }

		[PetaPoco.Column("Your_Job")]
		[DisplayName("Your_Job")]
		public string YourJob { get; set; }


		#endregion

		public bool IsAuthorized(ObCore.AuthorizationRequirement authorizationRequirement) {
			return IsAuthorized(this, authorizationRequirement);
		}

		public string FriendlyLocation {
			get {
				if ((Latitude.HasValue) && (Longitude.HasValue) && !String.IsNullOrEmpty(City) && !string.IsNullOrEmpty(State)) {
					if (Country.Equals("United States") || Country.Equals("USA")) return string.Format("{0}, {1}", City, State);
					return string.Format("{0}, {1}, {2}", City, State, Country);
				}

				// If we don't have a lat/long, their location probably isn't valid
				return String.Empty;
			}
		}

		public static Member Find(int idMember) {
			using (var db=new ObDb()) {
				return db.First<Member>("select * from MemberBasic where id_member=@0", idMember);
			}
		}

		public static Member Find(string login) {
			using (var db = new ObDb()) {
				return db.First<Member>("select * from MemberBasic mb inner join MemberLoginAll mla on mb.id_member=mla.id_member where mla.login=@0", login);
			}
		}

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

		# region Static Authorization / Authentication (Move to sep. auth class?)
		// todo: return LoginResult object (contains member, error, and/or login token)
		public static Member AttemptLogin(string login, string password, string ipAddress, string url, out string loginToken) {
			
			int idMember;

			using (var cmd = DataAccess.GetCommandStoredProcedure("Process_Login_Token")) {
				cmd.Parameters.AddWithValue("Login", login).Size=25;
				cmd.Parameters.AddWithValue("Pword", password).Size=25;
				cmd.Parameters.AddWithValue("ID_Member_Login_Method", ObCore.LoginMethod.Form);
				cmd.Parameters.AddWithValue("URL", url);
				cmd.Parameters.AddWithValue("IP_Address", ipAddress).Size=25;
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

		#endregion

	}
}