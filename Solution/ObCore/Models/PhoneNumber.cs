using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ObCore;
using ObCore.Helpers;
using ObCore.Models;

namespace ObApi.Models {
	public class PhoneNumber {
		public int IdMember;
		public int? IdPictureMember;
		public string Login, PhoneNumberUs, LoginsPrevious, FirstName, LastName, PictureUrl;
		public DateTime ClitterPreferencesUpdated;

		public static List<PhoneNumber> Find(int idMember, bool friendsOnly = true) {
			string sql = String.Format("select * from ClitterPhoneNumbers({0},{1}) order by login", idMember, Convert.ToInt32(friendsOnly));
			var da = new DataAccess();
			List<PhoneNumber> results;
			using (var dt = da.GetDataTable(sql)) {
				results = new List<PhoneNumber>(dt.Rows.Count);
				foreach (DataRow dr in dt.Rows) {
					results.Add(ToPhoneNumber(dr));
				}
			}
			return results;
		}

		private static PhoneNumber ToPhoneNumber(DataRow dr) {
			var pn = new PhoneNumber {
				ClitterPreferencesUpdated = (DateTime) dr["clitter_preferences_updated"],
				FirstName = (String) dr["first_name"],
				LastName = (String) dr["last_name"],
				Login = (String) dr["login"],
				//LoginsPrevious = (String) dr["logins_previous"],
				IdMember = (int) dr["id_member"],
				PhoneNumberUs = (string) dr["phone_number_us"]
			};
			pn.PictureUrl = Picture.PublicPictureUrl(pn.IdMember, PictureSize.Small50Px);
			if (dr["logins_previous"] != DBNull.Value) pn.LoginsPrevious = (string) dr["logins_previous"]; 
			if (dr["id_picture_member"] != DBNull.Value) pn.IdPictureMember = (int) dr["id_picture_member"]; 


			return pn;

		}

	}
}