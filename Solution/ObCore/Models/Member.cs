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

		[PetaPoco.Column("your_job")]
		[DisplayName("Your Job")]
		public string YourJob { get; set; }

		[PetaPoco.Column("id_member_invite")]
		[DisplayName("id_member_invite")]
		public int? IdMemberInvite{ get; set; }

		[PetaPoco.Column("login_invite")]
		[DisplayName("Invited By")]
		public string LoginInvite { get; set; }
		#endregion


		public Member MemberInvitedBy {
			get {
				if (IdMemberInvite.HasValue) return Member.Find(IdMemberInvite.Value);
				return null;
			}
		}

		public bool IsAuthorized(ObCore.AuthorizationRequirement authorizationRequirement) {
			return ObCore.Security.IsAuthorized(this, authorizationRequirement);
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

		public List<Picture> PublicPictures {
			get {
				return Picture.Fetch(this.IdMember, false);
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

		


	}
}