using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObCore {
	public static class PictureSize {
		public static string Thumb75Px = "_thumb"; // 75px
		public static string Full = ""; 
		public static string Small50Px = "_50";
		public static string Medium150Px = "_150";
	}

	public enum LoginMethod {
		Other=0,
		Form=1,
		Cookies=2,
		Signup=3,
		Activation=4
	}

	public enum LoginResult {
		Success=0,
		FailureTooManyLogins=1,
		FailureMemberBanned=2,
		FailureIpBanned=3,
		FailureMemberNotActivated=4,
		FailureMemberNotFound=5
	}

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
