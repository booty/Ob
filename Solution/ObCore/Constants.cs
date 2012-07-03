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
	/// Represents the authorization required to access a given resource
	/// </summary>
	public enum AuthorizationRequirement {
		NoRequirement,
		IsNotAuthenticated,
 		IsAuthenticated,
		IsFreeMember,
		HasPaidMemberPriviledges,
		IsModOrHigher,
		IsUberModOrHigher,
		IsCustomerServiceRepresentative,
		IsCustomerServiceRepresentativeAdmin,
		CanApprovePictures,
		CanApproveProfiles,
		CanApproveAds
	}

}
