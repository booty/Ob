using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

		public static List<Picture> Fetch(int idMember, bool friendsOnly=false, int limit=999) {
			using (var db=new ObDb()) {
				if (friendsOnly) return db.Fetch<Picture>(String.Format("select top {0} * from Picture_Member where id_menber=@0 and id_member friends_only=1 order by id_picture_member desc", limit), idMember);
				return db.Fetch<Picture>(String.Format("select top {0} * from Picture_Member where id_member=@0 and friends_only=0 and id_approval_status=0 order by primary_photo desc, id_picture_member desc", limit),  idMember);
			}
		}


	}
}
