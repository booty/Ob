using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ObCore.Models {
	public class Forum : ObDb.Record<Forum> {
		[JsonIgnore] [PetaPoco.Column("Forum_Title")] public string RawTitle { get; set; }
		[PetaPoco.Column("Forum_Description")] public string Description  { get; set; }
		[PetaPoco.Column("Image_Posting_Enabled")] public bool ImagePostingEnabled { get; set; }
		[PetaPoco.Column("Display_Order")] public int DisplayOrder { get; set; }
		[PetaPoco.Column("ID_Forum")] public int IdForum { get; set; }
		[PetaPoco.Column("Permission_Make_Replies")] public MemberPermissionLevel PermissionToMakeReplies { get; set; }
		[PetaPoco.Column("Permission_View_Threads")] public MemberPermissionLevel PermissionViewThreads  { get; set; }
		[PetaPoco.Column("Permission_Read_Replies")] public MemberPermissionLevel PermissionReadReplies { get; set; }
		[PetaPoco.Column("Permission_Edit_Post_Parent")] public MemberPermissionLevel PermissionEditPostParent { get; set; }
		[PetaPoco.Column("Permission_See_Tab")] public MemberPermissionLevel PermissionSeeTab { get; set; }

		public string Title {
			get {
				return RawTitle.Replace("<BR>", " ");
			}
		}
		public List<Thread> Threads { get; set; } 

		public void LoadThreads(MemberPermissionLevel memberPermissionLevel, bool includeAdult, bool includeSticky, int skip, int take) {
			Threads =Thread.Find(memberPermissionLevel, IdForum, includeAdult, includeSticky, skip, take);
		}

		static public Forum Find(MemberPermissionLevel memberPermissionLevel, int idForum) {
			using (var db=new ObDb()) {
				return db.FirstOrDefault<Forum>("select * from Forum where ID_Forum=@idForum and Permission_See_Tab <= @memberPermissionLevel order by Display_Order", new { idForum, memberPermissionLevel });
			}
		}

		static public Forum FindWithThreads(MemberPermissionLevel memberPermissionLevel, int idForum, bool includeAdult, bool includeSticky, int skip, int take) {
			var forum = Forum.Find(memberPermissionLevel, idForum);
			if (forum == null) return forum;
			forum.LoadThreads(memberPermissionLevel, includeAdult, includeSticky, skip, take);
			return forum;
		}

		static public List<Forum> Find(MemberPermissionLevel memberPermissionLevel) {
			using (var db=new ObDb()) {
				var results = db.Fetch<Forum>("select * from Forum where Permission_See_Tab <= @memberPermissionLevel order by Display_Order", new { memberPermissionLevel });
				if (results.Count == 0) return null;
				return results;
			}
		}

	}
}
