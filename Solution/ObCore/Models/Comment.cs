using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ObCore.Models {
	public class Comment : ObDb.Record<Comment> {
		[PetaPoco.Column("ID_Comment")] public int IdComment { get; set; }
		[PetaPoco.Column("ID_Member_From")] public int IdMemberFrom { get; set; }
		[PetaPoco.Column("ID_Member_To")] public int IdMemberTo { get; set; }
		[PetaPoco.Column("ID_Comment_Type")] public int IdCommentType { get; set; }
		[PetaPoco.Column("Description")] public string Description { get; set; }
		[PetaPoco.Column("Adult")] public bool Adult { get; set; }
		[PetaPoco.Column("Timestamp_Created")] public DateTime Created { get; set; }
		[PetaPoco.Column("Timestamp_Viewed")] public DateTime? Viewed { get; set; }
		[PetaPoco.Column("ID_Picture_Member")] public int? IdPictureMember { get; set; }
		public string Login { get; set; }
		[JsonIgnore] public Member MemberFrom { get { throw new NotImplementedException(); } }
		[JsonIgnore] public Member MemberTo { get { throw new NotImplementedException(); } }

		public Dictionary<string, string> PrimaryPhotoUrls {
			get {
				if (IdPictureMember.HasValue)
					return MemberPicture.PublicPictureUrls(IdPictureMember.Value);
				return null;
			}
		}

		public static List<Comment> FindFromMember(int idMember, int skip=0, int take=50, bool newestFirst=true) {
			string sql = String.Format("select * from commentspagedfrom({0},{1},{2}) order by [row_number]", idMember, skip, take);

			using (var db = new ObDb()) {
				return db.Fetch<Comment>(sql);
			}

		}

		public static List<Comment> FindToMember(int idMember, int skip = 0, int take = 50, bool newestFirst = true) {
			string sql = String.Format("select * from commentspagedto({0},{1},{2}) order by [row_number]", idMember, skip, take);

			using (var db = new ObDb()) {
				return db.Fetch<Comment>(sql);
			}
		}

		internal static void Create(int idMemberFrom, int idMemberTo, int idCommentType) {
			throw new NotImplementedException();
		}

		internal static void MarkAsReadForMember(int idMember, int idLastMessageRead) {
			throw new NotImplementedException();
		}

	}
}
