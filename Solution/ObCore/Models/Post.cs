using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObCore.Models {

	public class Post : ObDb.Record<Post> {
		[PetaPoco.Column("ID_Post")]
		public int IdPost {get;set;}
		[PetaPoco.Column("sequence")]
		public int Sequence {get;set;}
		[PetaPoco.Column("ID_Member")] public int IdMember { get; set;}
		[PetaPoco.Column("ID_Picture_Member")] public int? IdPictureMember {get; set;}
		[PetaPoco.Column("Timestamp_Created")] public DateTime Created { get; set; }
		[PetaPoco.Column("Timestamp_Updated")]
		public DateTime? Updated { get; set; }
		[PetaPoco.Column("ID_Post_Reply_To")] public int? IdPostReplyTo { get; set; }
		[PetaPoco.Column("Post_Subject")] public string Subject { get; set; }
		[PetaPoco.Column("Post_Body")] public string Body { get; set; }
		[PetaPoco.Column("ID_Post_Moderation_Status")] public int PostModerationStatus { get; set; }
		[PetaPoco.Column("Moderation_Timestamp")] public DateTime? Moderated { get; set; }
		[PetaPoco.Column("Moderator_Login")] public string ModeratorLogin { get; set; }
		[PetaPoco.Column("ID_Member_Moderated")] public int? IdMemberModerated { get; set; }
		[PetaPoco.Column("IP_Address")] public string IpAddress { get; set; }
		[PetaPoco.Column("ID_Article")] public int? IdArticle { get; set; }
		[PetaPoco.Column("Date_To_Show")] public DateTime? DateToShowArticle { get; set; }
		[PetaPoco.Column("Article_Has_Album")] public bool? ArticleHasAlbum { get; set; }
		[PetaPoco.Column("Article_Has_Body")] public bool? ArticleHasBody { get; set; }
		[PetaPoco.Column("Item_Description")] public string ItemDescription { get; set; }
		[PetaPoco.Column("Item_Icon")] public string ItemIcon { get; set; }
		[PetaPoco.Column("Adult")] public bool Adult { get; set; }
		[PetaPoco.Column("md5")] public string Md5Digest { get; set; }
		[PetaPoco.Column("Picture_Guid")] public string PictureGuid { get; set; }
		[PetaPoco.Column("Picture_Filesize_Bytes")] public int? PictureFileSizeBytes { get; set; }
		[PetaPoco.Column("Picture_Width")] public int? PictureWidth { get; set; }
		[PetaPoco.Column("Picture_Height")] public int? PictureHeight { get; set; }
		[PetaPoco.Column("Description")] public string ModerationDescription { get; set; }
		[PetaPoco.Column("Positive")] public bool? ModerationIsPositive { get; set; }
		[PetaPoco.Column("File_Extension")] public string ModerationFlagFileExtension { get; set; }
		[PetaPoco.Column("Moderation_Info")] public string ModerationInformation { get; set; }

		public string ModerationFlagUrl {
			get {
				return Models.PostModerationStatus.ModerationFlagUrl(PostModerationStatus, ModerationFlagFileExtension);
			}
		}

		public string MemberPictureUrl {
			get {
				if (IdPictureMember.HasValue) return MemberPicture.PublicPictureUrl(IdPictureMember.Value, PictureSize.Small50Px);
				return string.Empty;
			}
		}

		public bool HasPicture {
			get {
				return PictureFileSizeBytes.HasValue;
			}
		}

		public bool HasArticle {
			get {
				return IdArticle.HasValue;
			}
		}

		public bool IsThreadParent {
			get {
				return !(IdPostReplyTo.HasValue);
			}
		}

		public bool IsDeleted {
			get {
				return PostModerationStatus == Models.PostModerationStatus.Deleted;
			}
		}

		public bool IsFlaggedForReview {
			get {
				return PostModerationStatus == Models.PostModerationStatus.FlaggedForReview;
			}
		}

		public bool IsLocked {
			get {
				return PostModerationStatus == Models.PostModerationStatus.Locked;
			}
		}


		/// <summary>
		/// Finds a single post. Warning; assumes you're an ubermod. This might be private/internal at some point
		/// </summary>
		/// <param name="idPost">The post we're looking for</param>
		/// <param name="includeAdult">Return adult posts?</param>
		/// <param name="includeDeleted">Return deleted posts?</param>
		/// <returns>A single Post, or null if the post doesn't exist (or is not visible, because it's deleted or you said you didn't want adult posts, pussy</returns>
		public static Post Find(int idPost, bool includeAdult = false, bool includeDeleted = false) {
			return Find(idPost, MemberPermissionLevel.UberModerator, includeAdult, includeDeleted);
		}

		/// <summary>
		/// Finds a single post
		/// </summary>
		/// <param name="idPost">The post we're looking for</param>
		/// <param name="includeAdult">Return adult posts?</param>
		/// <param name="includeDeleted">Return deleted posts?</param>
		/// <param name="memberPermissionLevel">Permission level of the member viewing the post. Post might not be visible, depending on permission level</param>
		/// <returns>A single Post, or null if the post doesn't exist (or is not visible, because it's deleted or you said you didn't want adult posts, pussy</returns>
		public static Post Find(int idPost, MemberPermissionLevel memberPermissionLevel, bool includeAdult = false, bool includeDeleted=false) {
			using (var db = new ObDb()) {
				var sql = "select * from dbo.PostSingle(@idPost, @includeAdult, @includeDeleted, @idMemberPermissionLevel) where id_post=@idPost";
				if (!includeAdult) sql += " and Adult<>1";
				return db.FirstOrDefault<Post>(sql, new {idPost, includeAdult, includeDeleted, memberPermissionLevel});
			}
		}
	}
}
