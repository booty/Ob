using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObCore.Models {
	public class CommentType {
		public string Description { get; set; }

		public bool Adult { get; set; }

		public static List<CommentType> FindAll(bool includeAdult = true) {
			using (var db = new ObDb()) {
				if (includeAdult) 
					return db.Fetch<CommentType>("select Description,Adult from Comment_Type where Display_General=1 order by Adult desc, Description");
				else 
					return db.Fetch<CommentType>("select Description,Adult from Comment_Type where Display_General=1 and Adult=0 order by Description");
			}
		}
	}
}
