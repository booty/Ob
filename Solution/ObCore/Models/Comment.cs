using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ObCore.Models {
	internal class Comment : ObDb.Record<Comment> {
		[PetaPoco.Column("ID_Comment")] public int IdComment { get; set; }
		[PetaPoco.Column("ID_Member_From")] public int IdMemberFrom { get; set; }
		[PetaPoco.Column("ID_Member_To")] public int IdMemberTo { get; set; }
		[PetaPoco.Column("ID_Comment_Type")] public int IdCommentType { get; set; }
		[PetaPoco.Column("Description")] public string Description { get; set; }
		[PetaPoco.Column("Adult")] public bool Adult { get; set; }
		[PetaPoco.Column("Created")] public DateTime Created { get; set; }
		[PetaPoco.Column("Viewed")] public DateTime? Viewed { get; set; }
		[JsonIgnore] public Member MemberFrom { get { throw new NotImplementedException(); } }
		[JsonIgnore] public Member MemberTo { get { throw new NotImplementedException(); } }

		public static List<Comment> FindForMember(int idMemberTo, int skip=0, int take=50) {
			throw new NotImplementedException();
		}

		public static Comment Find(int idComment) {
			throw new NotImplementedException();
		}

		internal static void Create(int idMemberFrom, int idMemberTo, int idCommentType) {
			throw new NotImplementedException();
		}

		internal static void MarkAsReadForMember(int idMember, int idLastMessageRead) {
			throw new NotImplementedException();
		}

	}
}
