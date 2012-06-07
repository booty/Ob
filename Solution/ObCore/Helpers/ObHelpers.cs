using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;


namespace ObCore.Helpers {
	public static class ObHelpers {
		public static string MemberProfilePath(this int idMember) {
			return string.Format("/Member/{0}", idMember);
		}

		public static string MemberProfileA(this int idMember, string login) {
			return System.String.Format("<a href=\"{0}\">{1}</a>", idMember.MemberProfilePath(), login);
		}

		public static string MemberProfilePictureUrl(this int idPictureMember, string size = "") {
			return System.String.Format("{0}/user/pic/{1}/{2}{3}.jpg",
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
			if (idPictureMember.HasValue)
				return new HtmlString(System.String.Format(
					"<div class=\"memberPicture{2}\"><a href=\"{0}\">{1}</a></div>", 
					idMember.MemberProfilePath(), 
					idPictureMember.Value.MemberProfilePictureImg(login,size),
					size
				)
			);
			return new HtmlString(String.Empty);
		}

		public static HtmlString MemberProfilePicture(this int idPictureMember, string size, int idMember, string login) {
			return new HtmlString(System.String.Format(
				"<div class=\"memberPicture{2}\"><a href=\"{0}\">{1}</a></div>", 
				idMember.MemberProfilePath(), 
				idPictureMember.MemberProfilePictureImg(login,size),
				size
				)
			);
		}
	}

}
