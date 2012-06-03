using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Web;
using PetaPoco;

namespace ObCore.Models {
	
	[TableName("MemberBasic")]
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
		public string RelationshipDesiredDescriptionOthers { get; set; }
		[PetaPoco.Column("logins_previous")]
		public string LoginsPrevious { get; set; }
		[PetaPoco.Column("lifetime_member")]
		public bool? LifetimeMember { get; set; }
		[Required]
		[PetaPoco.Column("Paid_Member")]
		public int PaidMember { get; set; }
		[PetaPoco.Column("Paid_Or_Lifetime_Member")]
		public int? PaidOrLifetimeMember { get; set; }
		[Required]
		[PetaPoco.Column("last_login")]
		public DateTime LastLogin { get; set; }
		[PetaPoco.Column("Last_Login_Relative")]
		public string LastLoginRelative { get; set; }
		[PetaPoco.Column("last_active")]
		public DateTime? LastActive { get; set; }
		[PetaPoco.Column("Last_Active_Relative")]
		public string LastActiveRelative { get; set; }
		[Required]
		[PetaPoco.Column("Joined_Site")]
		public DateTime JoinedSite { get; set; }
		[PetaPoco.Column("Joined_Site_Relative")]
		public string JoinedSiteRelative { get; set; }
		[Required]
		[PetaPoco.Column("Likes_Females")]
		public bool LikesFemales { get; set; }
		[Required]
		[PetaPoco.Column("Likes_Males")]
		public bool LikesMales { get; set; }
		[PetaPoco.Column("Gender_Preference")]
		public string GenderPreference { get; set; }
		#endregion

		public static Member Find(int idMember) { 
			throw new NotImplementedException();
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