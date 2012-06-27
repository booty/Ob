using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using PetaPoco;

namespace ObCore.Models {

	[TableName("MemberBasic")]
	[PetaPoco.PrimaryKey("id_member")]
	[ExplicitColumns]
	public class Member : ObDb.Record<Member>  {

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

		[DisplayName("Paid or Lifetime Member?")]
		public bool IsPaidOrLifetimeMember { get; set; }
		public bool IsMod { get; set; }
		public bool IsUberMod { get; set; }
		public bool IsSysAdmin { get; set; }
		public bool IsCsr { get; set; }
		public bool IsCsrAdmin { get; set; }

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
	

		# region Authorization / authentication
		public static bool Validate(string login, string password) {
			var db = new ObDb();

			// Whoops, PetaPoco is weird about nulls
			var idMember = db.ExecuteScalar<int>("select isnull(dbo.MemberValidate(@0,@1), -1)", login, password);
			return (idMember > 0);


			throw new NotImplementedException();
		}

		public static bool IsAuthorized(Member member, ObCore.AuthorizationRequirement authorizationRequirement) {
			// Nothing to do!
			if (authorizationRequirement==AuthorizationRequirement.NoRequirement) return true;

			// If we don't actually have a member...
			if (member == null) {
				if (authorizationRequirement == AuthorizationRequirement.IsNotLoggedIn) return true;
				return false;
			}

			switch (authorizationRequirement) {	
				case AuthorizationRequirement.IsUberModOrHigher:
					return member.IsUberMod;
				case AuthorizationRequirement.HasPaidMemberPriviledges:
					return member.IsPaidOrLifetimeMember;
				case AuthorizationRequirement.IsFreeMember:
					return !member.IsPaidOrLifetimeMember;
				case AuthorizationRequirement.IsCustomerServiceRepresentative:
					return member.IsCsr;
				case AuthorizationRequirement.IsCustomerServiceRepresentativeAdmin:
					return member.IsCsrAdmin;
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