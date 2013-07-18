using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObCore {
	/// <summary>
	/// Sizes of the member-supplied pictures we store on disk.
	/// These correspond to the suffixes of the physical filenames
	/// ie, the 50px version of "foo12345.jpg" is "foo12345_50.jpg"
	/// </summary>
	public static class PictureSize {
		public static string Thumb75Px = "_thumb"; // 75px
		public static string Full = ""; 
		public static string Small50Px = "_50";
		public static string Medium150Px = "_150";
	}

	/// <summary>
	/// Used by the Member class to implement Nancy's (simple) IUserIdentity interface
	/// </summary>
	public static class Claims {
		public const string Admin = "Admin";
		public const string Moderator = "Moderator";
		public const string PaidOrLifetime = "PaidOrLifetime";
		public const string Adult = "Adult";
		public const string BootyCon2013 = "Bc2013";
	}

	/// <summary>
	/// Ways the user can log in
	/// </summary>
	public enum LoginMethod {
		Other=0,
		Form=1,
		Cookies=2,
		Signup=3,
		Activation=4
	}

	/// <summary>
	/// Phone number visibility levels
	/// </summary>
	public enum PhoneNumberVisibility {
		Private=0,
		FriendsOnly=1,
		AllMembers=2
	}

	/// <summary>
	/// Represents the authorization required to access a given resource
	/// </summary>
	public enum AuthorizationRequirement {
		NoRequirement,
		IsNotAuthenticated,
 		IsAuthenticated,
		IsFreeMember,
		HasPaidMemberPriviledges,
		IsModOrHigher,
		IsAdminOrHigher,
		IsCustomerServiceRepresentative,
		IsCustomerServiceRepresentativeAdmin,
		CanApprovePictures,
		CanApproveProfiles,
		CanApproveAds,
		CanSendPrivateMessages,
		CanSendComments,
		IsSysAdmin
	}

	public enum MemberPermissionLevel {
		Unauthenticated=0,
		FreeMember=1,
		PaidMember=2,
		Moderator=3,
		UberModerator=4,
		SysAdmin=5
	}

	public enum ClitterChannel {
		Public=0,
		BootyCon=1
	}
}
