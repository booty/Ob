using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using System.Text.RegularExpressions;


namespace ObCore.Helpers {
	public static class ObHelpers {

		public static string EventTypeToPastTenseSecondPerson(this string s) { 
			if (s.Equals("Comment")) return "sent you a Comment";
			if (s.Equals("FOP Shared")) return "shared a FOP with you";
			if (s.Equals("FOPs Shared")) return "shared FOPs with you";
			if (s.Equals("Friended")) return "added you to their Friends List";
			if (s.Equals("Private Message")) return "sent you a Private Message";
			if (s.Equals("Profile View")) return "viewed your profile";
			return "did something";
		}

		/*
		 * Helpers for generating URLs and HTML for OB assets.
		 * 
		 * Note: When a function accepts a parameter called "size", the value is expected to be one of...
		 *		ObCore.Constants.PictureSizeThumb 
		 *		ObCore.Constants.PictureSizeNormal 
		 *		ObCore.Constants.PictureSize50
		 *		ObCore.Constants.PictureSize150
		 *		
		*/

		#region Helpers for regular member pictures

		public static string MemberProfilePath(this int idMember) {
			return string.Format("/Member/{0}", idMember);
		}

		public static HtmlString MemberProfileA(this int idMember, string login) {
			return new HtmlString(String.Format("<a class=\"memberProfile\" href=\"{0}\">{1}</a>", idMember.MemberProfilePath(), login));
		}

		public static HtmlString MemberProfileA(this int? idMember, string login) {
			if (!idMember.HasValue) return String.Empty.ToHtmlString();
			return idMember.Value.MemberProfileA(login);
		}

		public static string MemberProfilePictureUrl(this int idPictureMember, string size = "") {
			return String.Format("{0}/user/pic/{1}/{2}{3}.jpg",
				ConfigurationManager.AppSettings["StaticAssetRootUrl"],
				idPictureMember.ToString().Left(2),
				idPictureMember,
				size
			);
		}

		public static string MemberProfilePictureUrl(this int? idPictureMember, string size = "") {
			if (!idPictureMember.HasValue) return string.Empty;
			return idPictureMember.Value.MemberProfilePictureUrl(size);
		}

		public static HtmlString MemberProfilePictureImg(this int idPictureMember, string login, string size) {

			return new HtmlString(System.String.Format("<img alt=\"{0}\" src=\"{1}\">", HttpUtility.HtmlEncode(login), idPictureMember.MemberProfilePictureUrl(size)));
		}

		public static HtmlString MemberProfilePictureImg(this int? idPictureMember, string login, string size) {
			return idPictureMember.HasValue ? idPictureMember.Value.MemberProfilePictureImg() : new HtmlString(string.Empty);
		}

		public static HtmlString MemberProfilePictureImg(this int idPictureMember, string size = "") {
			return new HtmlString(System.String.Format("<img src=\"{0}\">", idPictureMember.MemberProfilePictureUrl(size)));
		}

		public static HtmlString MemberProfilePictureImg(this int? idPictureMember, string size = "") {
			return idPictureMember.HasValue ? idPictureMember.Value.MemberProfilePictureImg(size) : new HtmlString(System.String.Empty);
		}

		public static HtmlString MemberProfilePicture(this int? idPictureMember, string size, int idMember, string login) {
			if (!idPictureMember.HasValue) return String.Empty.ToHtmlString();
			return System.String.Format(
				"<div class=\"memberPicture{2}\"><a href=\"{0}\">{1}</a></div>", 
				idMember.MemberProfilePath(), 
				idPictureMember.Value.MemberProfilePictureImg(login,size),
				size
			).ToHtmlString();
			
		}

		public static HtmlString MemberProfilePicture(this int? idPictureMember, string size, int? idMember, string login) {
			if ((!idMember.HasValue) || (!idPictureMember.HasValue)) return String.Empty.ToHtmlString();
			return idPictureMember.Value.MemberProfilePicture(size, idMember.Value, login);

		}

		public static HtmlString MemberProfilePicture(this int idPictureMember, string size, int idMember, string login) {
			return System.String.Format(
				"<div class=\"memberPicture{2}\"><a href=\"{0}\">{1}</a></div>", 
				idMember.MemberProfilePath(), 
				idPictureMember.MemberProfilePictureImg(login,size),
				size
				).ToHtmlString();
		}

		#endregion

		# region Helpers for Friends-Only Pictures (FOPs)

		public static string FopUrl(this string guid, string size) {
			if (String.IsNullOrEmpty(guid)) return string.Empty;
			return String.Format("{0}/user/fop/{1}{2}.jpg", ConfigurationManager.AppSettings["StaticAssetRootUrl"], guid, size);
		}

		public static HtmlString FopThumbImgA(this string guid, string size, string login = "") {
			if (String.IsNullOrEmpty(guid)) return string.Empty.ToHtmlString();
			return String.Format("<a href=\"{0}\"><img src=\"{1}\" alt=\"{2}\"></a>", guid.FopUrl(ObCore.PictureSize.Full), guid.FopUrl(size), login).ToHtmlString();
		}



		# endregion
	}

}
