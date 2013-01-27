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
		[PetaPoco.Column("Post_Subject")] public string Subject {get; set;}
		public List<Post> Replies;

		new public bool IsThreadParent {
			get {
				return true;
			}
		}

		public void LoadReplies(Member memberViewingThread, int skip, int take) {
			Replies = ObCore.Models.Post.FindPostsInThread(memberViewingThread, IdPost, skip, take);
		}

		public void LoadReplies(MemberPermissionLevel memberPermissionLevel, bool includeAdult, int skip, int take) {
			Replies = ObCore.Models.Post.FindPostsInThread(memberPermissionLevel, IdPost, includeAdult, skip, take);
		}

		public string ReplyMemberPictureUrl {
			get {
				if (LastReplyIdPictureMember.HasValue) return MemberPicture.PublicPictureUrl(LastReplyIdPictureMember.Value, PictureSize.Small50Px);
				return string.Empty;
			}
		}

		public static Thread FindThread(Member memberViewingThread, int idPost) {
			return FindThreadWithReplies(memberViewingThread.MemberPermissionLevel, idPost, memberViewingThread.IsAdult, 0, 0);
		}

		public static Thread FindThreadWithReplies(Member memberViewingThread, int idPost, int repliesToSkip, int repliesToTake) {
			return FindThreadWithReplies(memberViewingThread.MemberPermissionLevel, idPost, memberViewingThread.IsAdult, repliesToSkip, repliesToTake);
		}

		/// <summary>
		/// Returns a thread plus the thread's replies (if any)
		/// </summary>
		/// <param name="memberPermissionLevel">Permission level of the member viewing the thread</param>
		/// <param name="idPost">ID_Post of the parent post</param>
		/// <param name="includeAdult">Include adult replies?</param>
		/// <param name="repliesToSkip">Number of replies to skip</param>
		/// <param name="repliesToTake">Number of replies to load</param>
		/// <returns>A thread with replies (or null, if the thread isn't found</returns>
		public static Thread FindThreadWithReplies(MemberPermissionLevel memberPermissionLevel, int idPost, bool includeAdult, int repliesToSkip, int repliesToTake) {
			using (var db = new ObDb()) {
				var result = db.FirstOrDefault<Thread>("select * from dbo.ThreadSingle(@memberPermissionLevel, @idPost, @includeAdult)", new {
					memberPermissionLevel, includeAdult, idPost 
				});
				if (result == null) return null;
				if (repliesToTake>0) result.LoadReplies(memberPermissionLevel, includeAdult, repliesToSkip, repliesToTake);
				return result;
			}
		}

		/// <summary>
		/// Returns a thread; does not load replies
		/// </summary>
		/// <param name="memberPermissionLevel">Permission level of the member viewing the thread</param>
		/// <param name="idPost">ID_Post of the parent post</param>
		/// <param name="includeAdult">Include adult replies?</param>
		/// <returns>A thread with no replies (or null, if the thread isn't found</returns>
		public static Thread FindThread(MemberPermissionLevel memberPermissionLevel, int idPost, bool includeAdult) {
			return FindThreadWithReplies(memberPermissionLevel, idPost, includeAdult, 0, 0);
		}


		public static List<Thread> FindThreadWithReplies(MemberPermissionLevel memberPermissionLevel, bool includeAdult, bool includeSticky, int repliesToSkip, int repliesToTake) {
			using (var db = new ObDb()) {
				var result = db.Fetch<Thread>("select * from dbo.Threads2(@memberPermissionLevel, null, @includeAdult, @includeSticky, @repliesToSkip, @repliesToTake) order by sequence", new {
					memberPermissionLevel, includeAdult, includeSticky, repliesToSkip, repliesToTake
				});
				if (result.Count == 0) return null;
				return result;
			}
		}

		public static List<Thread> FindInForum(MemberPermissionLevel memberPermissionLevel, int? idForum, bool includeAdult, bool includeSticky, int skip, int take) {
			using (var db = new ObDb()) {
				var result = db.Fetch<Thread>(String.Format("select * from dbo.Threads2(@memberPermissionLevel, @idForum, @includeAdult, @includeSticky, {0}, {1}) order by sequence",skip,take), new {
					memberPermissionLevel, idForum, includeAdult, includeSticky
				});
				if (result.Count == 0) return null;
				return result;
			}
		}

		public static List<Thread> FindInForum(Member memberViewingForum, int? idForum, bool includeSticky, int skip, int take) {
			using (var db = new ObDb()) {
				var result = db.Fetch<Thread>("select * from dbo.Threads2(@memberPermissionLevel, @idForum, @includeAdult, @includeSticky, @skip, @take) order by sequence", new {
					memberPermissionLevel=memberViewingForum.MemberPermissionLevel,
					idForum, 
					includeAdult=memberViewingForum.IsAdult, 
					includeSticky, 
					skip, 
					take
				});
				if (result.Count == 0) return null;
				return result;
			}
		}

	}



	
}
