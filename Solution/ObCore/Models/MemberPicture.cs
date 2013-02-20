using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using ObCore.Helpers;
using PetaPoco;

namespace ObCore.Models {
	[TableName("Picture_Member")]
	public class MemberPicture {
		[PetaPoco.Column("ID_Picture_Member")] public int IdPictureMember { get; set; }
		[PetaPoco.Column("ID_Member")] public int IdMember { get; set; }
		[PetaPoco.Column("Friends_Only")] public bool FriendsOnly { get; set; }
		[PetaPoco.Column("Timestamp_Created")] public DateTime Created { get; set; }
		public string Caption { get; set; }
		public Guid Guid { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		[PetaPoco.Column("Width_Thumb")] public int WidthThumb { get; set; }
		[PetaPoco.Column("Width_Height")] public int HeightThumb { get; set; }
		[PetaPoco.Column("Primary_Photo")] public bool PrimaryPhoto { get; set; }
		[PetaPoco.Column("ID_Approval_Status")] public int IdApprovalStatus { get; set; }
		public string Login { get; set; }
		[PetaPoco.Column("ApprovalStatusDescription")] public string ApprovalStatusDescription { get; set; }
		[PetaPoco.Column("Moderator_Login")] public string ModeratorLogin { get; set; }

		// If this default constructor doesn't exist, PetaPoco gets confused
		public MemberPicture() {
		}

		public MemberPicture(string fopGuid) {
			Guid =  new Guid(fopGuid);
		}

		public MemberPicture(Guid fopGuid) {
			Guid = fopGuid;
		}

		/*
		public string Url {
			get {
				if (FriendsOnly) {
					return MemberPicture.FriendsOnlyPictureUrl(Guid);
				}
				return MemberPicture.PublicPictureUrl(IdPictureMember);
			}
		}
		 * */

		public bool IsApproved { 
			get { 
				return (IdApprovalStatus==0); 
			}
		}

		public bool IsPending {
			get {
				return (IdApprovalStatus == 1);
			}
		}

		public bool IsDenied {
			get {
				return (IdApprovalStatus > 1);
			}
		}

		public System.Collections.Generic.Dictionary<string, string> Urls {
			get {
				var urls = new Dictionary<string, string>(4);
				if (FriendsOnly) return FriendsOnlyPictureUrls(Guid.ToString());
				return PublicPictureUrls(IdPictureMember);
			}
		}

		/// <summary>
		/// Returns a list of pictures.
		/// </summary>
		/// <param name="idMember">Person who's pictures we're looking for</param>
		/// <param name="friendsOnly">True if FOPs; false if public</param>
		/// <param name="relationship">If supplied, it will only return FOPs if authorized. If omitted, it assumes you've already verified this!!!!</param>
		/// <returns></returns>
		public static List<MemberPicture> Find(int idMember, bool friendsOnly=false, Relationship relationship=null) {
			using (var db=new ObDb()) {
				// fail out (return empty list) if this member isn't authorized
				if (relationship != null) {
					if (!relationship.Member2FopsVisible) return new List<MemberPicture>(0);
				}

				if (friendsOnly) {
					return db.Fetch<MemberPicture>("select * from PictureMemberView where id_member=@0 and friends_only=1 order by id_picture_member desc", idMember);
				}

				return db.Fetch<MemberPicture>("select * from PictureMemberView where id_member=@0 and friends_only=0 and id_approval_status=0 order by primary_photo desc, id_picture_member desc",  idMember);
			}
		}

		public static MemberPicture FindPublicPicture(int idPictureMember, bool approvedOnly = true) {
			using (var db=new ObDb()) {
				if (approvedOnly) {
					return db.FirstOrDefault<MemberPicture>("select * from PictureMemberView where friends_only=0 and id_picture_member=@idPictureMember and id_approval_status=0", new { idPictureMember } );
				}
				else {
					return db.FirstOrDefault<MemberPicture>("select * from PictureMemberView where friends_only=0 and id_picture_member=@idPictureMember");
				}
			}
		}

		public static List<MemberPicture> FindPublicPictures(int skip = 0, int take = 25, bool approvedOnly = true) {
			string sql;

			if (approvedOnly)
				sql = @"select *
				from ( select ROW_NUMBER() over (order by id_picture_member desc) as rownumber, * from PictureMemberView where friends_only=0 and id_approval_status=0 ) 
				pics where rownumber between (@skip+1) and @skip+@take";
			else {
				sql = @"select *
				from ( select ROW_NUMBER() over (order by id_picture_member desc) as rownumber, * from PictureMemberView where friends_only=0 ) pics 
				where rownumber between (@skip+1) and @skip+@take";
			}

			using (var db=new ObDb()) {
				return db.Fetch<MemberPicture>(sql, new {
					skip,
					take
				});
			}

		}

		/*
		public static MemberPicture Find(string guid, Member memberViewing) {
			using (var db = new ObDb()) {
				// see if the FOP even exists
				var pic = db.FirstOrDefault<MemberPicture>("select * from PictureMemberView where guid=@guid", new { guid });
				if (pic == null) return null;

				// if it exists, see if this member has the right to see it
				if (memberViewing.CanViewFopsOf(pic.IdMember)) {
					return pic;
				}
				else {
					return null;
				}
			}
		}
		 * */

		internal static List<MemberPicture> FindFriendsOnlyPictures(int idMember) {
			using (var db = new ObDb()) {
				return db.Fetch<MemberPicture>("select * from PictureMemberView where friends_only=1 and id_picture_member=@idMember order by id_picture_member desc", new { idMember });
			}
		}

		internal static MemberPicture FindFriendsOnlyPicture(string guid) {
			using (var db = new ObDb()) {
				return db.FirstOrDefault<MemberPicture>("select * from PictureMemberView where friends_only=1 and guid=@guid order by id_picture_member desc", new { guid });
			}
		}

		internal static MemberPicture FindFriendsOnlyPicture(string guid, int idMember) {
			using (var db = new ObDb()) {
				return db.FirstOrDefault<MemberPicture>("select * from PictureMemberView where guid=@guid and friends_only=1 and id_member=@idMember  order by id_picture_member desc", new { guid, idMember });
			}
		}

		/*
		URL Methods
		Arguably, these shouldn't be here in the model...
		*/

		// http://assets.otakubooty.com/user/fop/AC9B9BF2-CD75-4EE9-AAB9-C0A1EE156360.jpg
		public static string FriendsOnlyPictureUrl(string fopGuid, string pictureSize) {
			if (String.IsNullOrEmpty(fopGuid)) return string.Empty;
			return String.Format("{0}/user/fop/{1}{2}.jpg",
				ConfigurationManager.AppSettings["StaticAssetRootUrl"],
				fopGuid,
				pictureSize);
		}

		public static string FriendsOnlyPictureUrl(string fopGuid) {
			return FriendsOnlyPictureUrl(fopGuid, PictureSize.Full);
		}

		public static string FriendsOnlyPictureUrl(Guid fopGuid) {
			return FriendsOnlyPictureUrl(fopGuid.ToString(), PictureSize.Full);
		}

		public static string FriendsOnlyPictureUrl(Guid fopGuid, string pictureSize) {
			return FriendsOnlyPictureUrl(fopGuid.ToString(), pictureSize);
		}

		public static string PublicPictureUrl(int? idPictureMember) {
			if (idPictureMember.HasValue) return PublicPictureUrl(idPictureMember.Value, PictureSize.Small50Px);
			return null;
		}

		public static string PublicPictureUrl(int? idPictureMember, string pictureSize) {
			if (idPictureMember.HasValue) return PublicPictureUrl(idPictureMember.Value, pictureSize);
			return null;
		}

		public static string PublicPictureUrl(int idPictureMember) {
			return PublicPictureUrl(idPictureMember, PictureSize.Small50Px);
		}

		public static string PublicPictureUrl(int idPictureMember, string pictureSize) {
			return String.Format("{0}/user/pic/{1}/{2}{3}.jpg",
				ConfigurationManager.AppSettings["StaticAssetRootUrl"],
				idPictureMember.ToString().Left(2),
				idPictureMember,
				pictureSize
			);
		}

		public static Dictionary<string, string> FriendsOnlyPictureUrls(string fopGuid) {
			var urls = new Dictionary<string, string>(3);
			urls.Add("full", FriendsOnlyPictureUrl(fopGuid, PictureSize.Full));
			urls.Add("150", FriendsOnlyPictureUrl(fopGuid, PictureSize.Medium150Px));
			urls.Add("75", FriendsOnlyPictureUrl(fopGuid, PictureSize.Thumb75Px));
			urls.Add("50", FriendsOnlyPictureUrl(fopGuid, PictureSize.Small50Px));
			return urls;		
		}

		public static Dictionary<string, string> PublicPictureUrls(int? idPictureMember) {
			if (!idPictureMember.HasValue) return null;
			return PublicPictureUrls(idPictureMember.Value);
		}

		public static Dictionary<string, string> PublicPictureUrls(int idPictureMember) {
			var urls = new Dictionary<string, string>(3);
			urls.Add("full", PublicPictureUrl(idPictureMember, PictureSize.Full));
			urls.Add("150", PublicPictureUrl(idPictureMember, PictureSize.Medium150Px));
			urls.Add("75", PublicPictureUrl(idPictureMember, PictureSize.Thumb75Px));
			urls.Add("50", PublicPictureUrl(idPictureMember, PictureSize.Small50Px));
			return urls;		
		}

	}
}
