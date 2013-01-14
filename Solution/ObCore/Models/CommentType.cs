using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObCore.Models {
	public class CommentType {
		[PetaPoco.Column("Description")] public string Description { get; set; }
		[PetaPoco.Column("Adult")] public bool Adult { get; set; }
		[PetaPoco.Column("Display_General")] public bool Active { get; set; }
		[PetaPoco.Column("ID_Comment_Type")] public int IdCommentType { get; set; }

		public static CommentType Find(int idCommentType) {
			using (var db=new ObDb()) {
				return db.FirstOrDefault<CommentType>("select * from Comment_Type where ID_Comment_Type=@idCommentType", new {
					idCommentType
				}); 
			}
		}

		public static List<CommentType> FindAll(bool includeAdult = true, bool includeInactive = false) {
			var sql = new StringBuilder("select * from Comment_Type where 1=1 ");
			if (!includeAdult) sql.Append(" and Adult=0");
			if (!includeInactive) sql.Append(" and Display_General=1");
			sql.Append(" order by Adult desc, Description");

			using (var db = new ObDb()) {
				return db.Fetch<CommentType>(sql.ToString());
			}
		}
	}
}
