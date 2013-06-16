using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObCore.Helpers;
using ObCore.Models;

namespace ObCore.Models  {
	public class Match: ObDb.Record<Match> {
		[PetaPoco.Column("id_match")] public int IdMatch { get; set; }
		[PetaPoco.Column("timestamp_created")] public DateTime Created { get; set; }
		[PetaPoco.Column("id_member_to")] public int IdMemberTo { get; set; }
		[PetaPoco.Column("id_member_from")] public int IdMemberFrom { get; set; }
		[PetaPoco.Column("id_match_type")] public int IdMatchType { get; set; }
		[PetaPoco.Column("is_mutual")] public bool IsMutual { get; set; }
		[PetaPoco.Column("timestamp_created_match")] public DateTime? MatchCreated { get; set; }
		[PetaPoco.Column("id_picture_member_from")] public int? IdPictureMemberFrom { get; set; }
		[PetaPoco.Column("id_picture_member_to")] public int? IdPictureMemberTo { get; set; }
		[PetaPoco.Column("login_from")] public string LoginFrom { get; set; }
		[PetaPoco.Column("login_to")] public string LoginTo { get; set; }
		[PetaPoco.Column("gender_from")] public char GenderFrom { get; set; }
		[PetaPoco.Column("gender_to")] public char GenderTo { get; set; }
		[PetaPoco.Column("age_from")] public int AgeFrom { get; set; }
		[PetaPoco.Column("age_to")] public int AgeTo { get; set; }

		public string CreatedRelative {
			get {
				return Created.ToRelativeDate();
			}
		}

		public string MatchCreatedRelative {
			get {
				return MatchCreated.ToRelativeDate();
			}
		}

		public Dictionary<string,string> MemberPictureFromUrls {
			get {
				if (IdPictureMemberFrom.HasValue) return MemberPicture.PublicPictureUrls(IdPictureMemberFrom.Value);
				return null;
			}
		}

		public Dictionary<string,string> MemberPictureToUrls {
			get {
				if (IdPictureMemberTo.HasValue) return MemberPicture.PublicPictureUrls(IdPictureMemberTo.Value);
				return null;
			}
		}

		/*
		static Match Find(string authenticationToken, int idMatch) {
			using (var db = new ObDb()) {
				var sql = "select * from dbo.PostSingle(@idPost, @includeAdult, @includeDeleted, @idMemberPermissionLevel) where id_post=@idPost";
			}
		}
		*/
		static public List<Match> FindAllFromUser(int idMemberFrom) {
			using (var db = new ObDb()) {
				return db.Fetch<Match>(String.Format("select * from MatchView where id_member_from={0} order by timestamp_created desc", idMemberFrom));
			}
		}
		
		static public MatchResult Create(int idMemberFrom, int idMemberTo, int idMatchType = 1) {
			using (var db = new ObDb()) {
				return db.FirstOrDefault<MatchResult>("exec Match_Insert @@ID_Member_From=@idMemberFrom, @@Id_Member_To=@idMemberTo, @@Id_Match_Type=@idMatchType", new {
					idMemberFrom = idMemberFrom,
					idMemberTo = idMemberTo,
					idMatchType = idMatchType
				});
			}
		}

		static public MatchResult Create(string authenticationToken, int idMemberFrom, int idMemberTo, int idMatchType = 1) {
			using (var db = new ObDb()) {
				return db.FirstOrDefault<MatchResult>("exec Match_Insert @@ID_Member_From=@idMemberFrom, @@Id_Member_To=@idMemberTo, @@Id_Match_Type=@idMatchType, @@AuthenticationToken=@authenticationToken", new {
					idMemberFrom = idMemberFrom,
					idMemberTo = idMemberTo,
					idMatchType = idMatchType,
					authenticationToken = authenticationToken
				});
			}
		}
	}
}
