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
	
	public enum AuthorizationRequirement {
		NoRequirement,
		IsNotLoggedIn,
 		IsLoggedIn,
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
