using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ObCore.Models {
	public class Thread : ObCore.Models.Post {
		[PetaPoco.Column("Reply_Count")] public int ReplyCount { get; set; }
		[PetaPoco.Column("Reply_Count_Non_Adult")] public int ReplyCountNonAdult {get; set;}
		[PetaPoco.Column("Reply_ID_Member")] public int? LastReplyIdMember {get; set;}
		[PetaPoco.Column("Reply_Login")] public string LastReplyLogin {get; set;}
		[PetaPoco.Column("Reply_Id_Picture_Member")] public int? LastReplyIdPictureMember { get; set; }
		[PetaPoco.Column("ID_Forum")] public int IdForum {get; set;}
		[PetaPoco.Column("Timestamp_Reply")] public DateTime? Replied {get; set;}
		[PetaPoco.Column("Sticky")] public bool Sticky {get; set;}
		[PetaPoco.Column("Awesome_Reply_Count")] public int UpModdedReplyCount {get; set;}
		
		new public bool IsThreadParent {
			get {
				return true;
			}
		}

		public string ReplyMemberPictureUrl {
			get {
				if (LastReplyIdPictureMember.HasValue) return MemberPicture.PublicPictureUrl(LastReplyIdPictureMember.Value, PictureSize.Small50Px);
				return string.Empty;
			}
		}

		public static List<Thread> Find(MemberPermissionLevel memberPermissionLevel, bool includeAdult, bool includeSticky, int skip, int take) {
			using (var db = new ObDb()) {
				var result = db.Fetch<Thread>("select * from dbo.Threads2(@memberPermissionLevel, null, @includeAdult, @includeSticky, @skip, @take) order by sequence desc", new {
					memberPermissionLevel, includeAdult, includeSticky, skip, take
				});
				if (result.Count == 0) return null;
				return result;
			}
		}

		public static List<Thread> Find(MemberPermissionLevel memberPermissionLevel, int? idForum, bool includeAdult, bool includeSticky, int skip, int take) {
			using (var db = new ObDb()) {
				var result = db.Fetch<Thread>("select * from dbo.Threads2(@memberPermissionLevel, @idForum, @includeAdult, @includeSticky, @skip, @take) order by sequence desc", new {
					memberPermissionLevel, idForum, includeAdult, includeSticky, skip, take
				});
				if (result.Count == 0) return null;
				return result;
			}
		}

		public static List<Thread> Find(int? idForum, bool includeAdult, bool includeSticky, int skip, int take) {
			using (var db = new ObDb()) {
				var result = db.Fetch<Thread>("select * from dbo.Threads2(@idForum, @includeAdult, @includeSticky, @skip, @take) order by sequence desc", new {
					idForum, includeAdult, includeSticky, skip, take
				});
				if (result.Count == 0) return null;
				return result;
			}
		}

	}



	
}
