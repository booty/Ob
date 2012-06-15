using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using PetaPoco;

namespace ObCore.Models {

	[ExplicitColumns]
	public class Notification : ObDb.Record<Notification> {
		[PetaPoco.Column]
		public string Subject { get; set; }
		[PetaPoco.Column]
		public string Login { get; set; }
		[PetaPoco.Column]
		public string Body { get; set; }
		[PetaPoco.Column]
		public string Description { get; set; }

		[PetaPoco.Column("event_type")]
		public string EventType { get; set; }
		[PetaPoco.Column("event_time")]
		public DateTime EventTime { get; set; }
		[PetaPoco.Column("id_member_from")]
		public int? IdMemberFrom { get; set; }
		[PetaPoco.Column("id_post")]
		public int? IdPost { get; set; }
		[PetaPoco.Column("id_post_reply_to")]
		public int? IdPostReplyTo { get; set; }
		[PetaPoco.Column("qty")]
		public int? Quantity { get; set; }
		[PetaPoco.Column("fop_guid1")]
		public string FopGuid1 { get; set; }
		[PetaPoco.Column("fop_guid2")]
		public string FopGuid2 { get; set; }
		[PetaPoco.Column("fop_guid3")]
		public string FopGuid3 { get; set; }
		[PetaPoco.Column("id_picture_member")]
		public int? IdPictureMember { get; set; }
		[PetaPoco.Column("timestamp_read")]
		public DateTime? TimestampRead { get; set; }
		[PetaPoco.Column("Id_Comment")]
		public int? IdComment { get; set; }
		[PetaPoco.Column("Id_Message")]
		public int? IdMessage { get; set; }

		public string HtmlDataAttribute {
			get {
				if (IdComment.HasValue) return String.Format(@"data-id-comment=""{0}""", IdComment.Value);
				if (IdMessage.HasValue) return String.Format(@"data-id-message=""{0}""", IdMessage.Value);
				if (IdPost.HasValue) return String.Format(@"data-id-post=""{0}""", IdPost.Value);
				if (IdPostReplyTo.HasValue) return String.Format(@"data-id-post=""{0}""", IdPostReplyTo.Value);
				if (IdMemberFrom.HasValue) return String.Format(@"data-id-member=""{0}""", IdMemberFrom.Value);
				return String.Empty;
			}
		}

		public static string EventTypeComment="Comment";
		public static string EventTypeFopShared="FOP Shared";
		public static string EventTypeFopsShared="FOPs Shared";
		public static string EventTypeFriended="Friended";
		public static string EventTypePrivateMessage="Private Message";
		public static string EventTypeProfileView="Profile View";

		public static List<Notification> Fetch(int idMember) {
			using (var db=new ObCore.Models.ObDb()) {
				return db.Fetch<Notification>("select * from dbo.ClitterNotifications(@0) order by event_time desc", idMember);
			}
		}
	}

}

