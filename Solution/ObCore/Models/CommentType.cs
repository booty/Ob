using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObCore.Models {
	public class CommentType {
		[PetaPoco.Column("Description")]
		public string Description {
			get;
			set;
		}
		[PetaPoco.Column("Adult")]
		public bool Adult {
			get;
			set;
		}
		[PetaPoco.Column("Display_General")]
		public bool Active {
			get;
			set;
		}
		[PetaPoco.Column("ID_Comment_Type")]
		public int IdCommentType {
			get;
			set;
		}

		public enum Order {
			ByName = 1,
			ByAdultFirst = 2,
			ByAdultLast = 3
		}

		public static CommentType Find(int idCommentType, bool includeAdult = false) {
			using (var db=new ObDb()) {
				string sql;

				if (includeAdult)
					sql = String.Format("select * from Comment_Type where ID_Comment_Type={0}", idCommentType);
				else
					sql = String.Format("select * from Comment_Type where ID_Comment_Type={0} and adult=0", idCommentType);

				return db.FirstOrDefault<CommentType>(sql);
			}
		}

		public static List<CommentType> FindAll(bool includeAdult = true, bool includeInactive = false, Order orderBy = Order.ByName) {
			var sql = new StringBuilder("select * from Comment_Type where 1=1 ");
			if (!includeAdult) sql.Append(" and Adult=0");
			if (!includeInactive) sql.Append(" and Display_General=1");
			switch (orderBy) {
				case Order.ByAdultFirst:
					sql.Append(" order by adult desc, description");
					break;
				case Order.ByAdultLast:
					sql.Append(" order by adult, description");
					break;
				case Order.ByName:
				default:
					sql.Append(" order by description");
					break;
			}

			using (var db = new ObDb()) {
				return db.Fetch<CommentType>(sql.ToString());
			}
		}
	}
}
