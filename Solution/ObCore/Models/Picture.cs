using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using ObCore.Helpers;
using PetaPoco;

namespace ObCore.Models {
	[TableName("Picture_Member")]
	public class Picture {
		[PetaPoco.Column("ID_Picture_Member")]
		public int IdPictureMember { get; set; }
		[PetaPoco.Column("ID_Member")]
		public int IdMember { get; set; }
		[PetaPoco.Column("Friends_Only")]
		public bool FriendsOnly { get; set; }
		public DateTime Created { get; set; }
		public string Caption { get; set; }
		public Guid Guid { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		[PetaPoco.Column("Width_Thumb")]
		public int WidthThumb { get; set; }
		[PetaPoco.Column("Width_Height")]
		public int HeightThumb { get; set; }
		[PetaPoco.Column("Primary_Photo")]
		public bool PrimaryPhoto { get; set; }

		/// <summary>
		/// Returns a list of pictures.
		/// </summary>
		/// <param name="idMember">Person who's pictures we're looking for</param>
		/// <param name="friendsOnly">True if FOPs; false if public</param>
		/// <param name="relationship">If supplied, it will only return FOPs if authorized. If omitted, it assumes you've already verified this!!!!</param>
		/// <returns></returns>
		public static List<Picture> Find(int idMember, bool friendsOnly=false, Relationship relationship=null) {
			using (var db=new ObDb()) {
				// fail out (return empty list) if this member isn't authorized
				if (relationship != null) {
					if (!relationship.Member2FopsVisible) return new List<Picture>(0);
				}

				if (friendsOnly) {
					return db.Fetch<Picture>("select * from Picture_Member where id_member=@0 and friends_only=1 order by id_picture_member desc", idMember);
				}

				return db.Fetch<Picture>("select * from Picture_Member where id_member=@0 and friends_only=0 and id_approval_status=0 order by primary_photo desc, id_picture_member desc",  idMember);
			}
		}

		
		public static string PublicPictureUrl(int idPictureMember, string size="") {
			return String.Format("{0}/user/pic/{1}/{2}{3}.jpg",
				ConfigurationManager.AppSettings["StaticAssetRootUrl"],
				idPictureMember.ToString().Left(2),
				idPictureMember,
				size
			);
		}

	}
}
