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
		[PetaPoco.Column("Id_Notification_Event_Type")] 
		public int? IdNotificationEventType { get; set; }

		public string PictureUrl {
			get {
				if (!IdPictureMember.HasValue) return String.Empty;
				return Picture.PublicPictureUrl(IdPictureMember.Value);
			}
		}

		// todo: This should really be client-side, right?
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

		public static List<Notification> Find(int idMember, int skip, int take, NotificationType notificationType=NotificationType.All) {
			using (var db=new ObCore.ObDb()) {
				switch (notificationType) {
					case NotificationType.PrivateMessages:
						return db.Fetch<Notification>("select * from dbo.MessageNotifications(@0,@1,@2) where id_notification_event_type=1 order by event_time desc", idMember, skip, take);					
					case NotificationType.Comments:
						return db.Fetch<Notification>("select * from dbo.CommentNotifications(@0,@1,@2) order by event_time desc", idMember, skip, take);					
					case NotificationType.PrivateMessagesAndComments:
						return db.Fetch<Notification>("select * from dbo.ClitterNotifications(@0,@1,@2) where id_notification_event_type in (1,2) order by event_time desc", idMember, skip, take);
					case NotificationType.Fops: 
						return db.Fetch<Notification>("select * from dbo.FopNotifications(@0,@1,@2) where id_notification_event_type in (5,6) order by event_time desc", idMember, skip, take);
					case NotificationType.Friendings:
						return db.Fetch<Notification>("select * from dbo.FriendNotifications(@0,@1,@2) where id_notification_event_type=4 order by event_time desc", idMember, skip, take);					
					case NotificationType.All:
						return db.Fetch<Notification>("select * from dbo.ClitterNotifications(@0,@1,@2) order by event_time desc", idMember, skip, take);
					default:
						throw new NotImplementedException("Booty didn't implement that notificationt type yet. :-(");
				}
				
			}
		}

		public static NotificationType ToNotificationType(string s) {
			switch (s.ToLower()) {
				case "all":
					return NotificationType.All;
				case "messagesandcomments": 
					return NotificationType.PrivateMessagesAndComments;
				case "fops":
					return NotificationType.Fops;
				case "friendings":
					return NotificationType.Friendings;
				case "profileviews":
					return NotificationType.ProfileViews;
				case "messages":
					return NotificationType.PrivateMessages;
				case "comments":
					return NotificationType.Comments;
				default:
					throw new ArgumentException("Don't know that notification type!");
			}
		}

		public enum NotificationType {
			PrivateMessagesAndComments = 1000,
			Fops = 1001,
			Friendings = 4,
			ProfileViews = 3,
			PrivateMessages = 1,
			Comments = 2,
			All = 9999
		}


	}

}

