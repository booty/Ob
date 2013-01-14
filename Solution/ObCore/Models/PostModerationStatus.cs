using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObCore.Models {
	public static class PostModerationStatus {
		public const int Normal=1;
		public const int FlaggedForReview=3;
		public const int Deleted=4;
		public const int Locked=9;

		public static string ModerationFlagUrl(int idPostModerationStatus, string fileExtension) {
			if (idPostModerationStatus == Normal) return null;
			return String.Format("http://assets.otakubooty.com/forum/modflags/post/{0}{1}", idPostModerationStatus, fileExtension);
		}
	}
}
