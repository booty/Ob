using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Web;
using PetaPoco;

namespace ObCore.Models {
	
	[TableName("MemberBasic")]
	[PetaPoco.PrimaryKey("id_member")]
	[ExplicitColumns]
	public class Member : ObDb.Record<Member>, IPrincipal {

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
		
		[Required]
		[DisplayName("Paid Member?")]
		[PetaPoco.Column("Paid_Member")]
		public int PaidMember { get; set; }

		[DisplayName("Paid or Lifetime Member?")]
		[PetaPoco.Column("Paid_Or_Lifetime_Member")]
		public int? PaidOrLifetimeMember { get; set; }

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
		#endregion


		public string FriendlyLocation {
			get {
				if ((Latitude.HasValue) && (Longitude.HasValue) && !String.IsNullOrEmpty(City) && !string.IsNullOrEmpty(State)) {
					if (Country.Equals("United States")) return string.Format("{0}, {1}", City, State);
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

		public static bool Validate(string login, string password) {
			var db = new ObDb();

			// Whoops, PetaPoco is weird about nulls
			var idMember = db.ExecuteScalar<int>("select isnull(dbo.MemberValidate(@0,@1), -1)", login, password);
			return (idMember>0);


			throw new NotImplementedException();
		}

		#region IPrincipal Members

		public IIdentity Identity {
			get { throw new NotImplementedException(); }
		}

		public bool IsInRole(string role) {
			throw new NotImplementedException();
		}

		#endregion
	}
}