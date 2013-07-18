using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ObCore.Helpers;
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
		[JsonIgnore][PetaPoco.Column("fop_guid1")]
		public string FopGuid1 { get; set; }
		[JsonIgnore][PetaPoco.Column("fop_guid2")]
		public string FopGuid2 { get; set; }
		[JsonIgnore][PetaPoco.Column("fop_guid3")]
		public string FopGuid3 { get; set; }
		[PetaPoco.Column("id_picture_member")]
		public int? IdPictureMember { get; set; }
		[PetaPoco.Column("timestamp_read")]
		public DateTime? TimestampRead { get; set; }
		[PetaPoco.Column("Id_Comment")]
		public int? IdComment { get; set; }
		[PetaPoco.Column("Id_Message")]
		public int? IdMessage { get; set; }
		
		[PetaPoco.Column("Id_Clitter_Post")] 
		public int? IdClitterPost { get; set; }
		[PetaPoco.Column("Id_Clitter_Channel")] 
		public int? IdClitterChannel { get; set; }
		[PetaPoco.Column("Id_Clitter_Post_Reply")] 
		public int? IdClitterPostReplyTo { get; set; }

		[PetaPoco.Column("Friends_Only")] 
		public bool? FriendsOnly { get; set; }
		[PetaPoco.Column("Moderator_Voice")] 
		public bool? ModeratorVoice { get; set; }

		[PetaPoco.Column("Id_Notification_Event_Type")] 
		public int? IdNotificationEventType { get; set; }

		public Dictionary<string, string> MemberPictureUrls {
			get {
				return MemberPicture.PublicPictureUrls(IdPictureMember);
			}
		}
		
		public string RelativeEventTime {
			get {
				return EventTime.ToRelativeDate();
			}
		}

		public string RelativeEventTimeDetailed {
			get {
				return EventTime.ToRelativeDateDetailed();
			}
		}

		/// <summary>
		/// Hide the shameful, gross FopGuid1...FopGuid3 behind an array for neater serialization, etc.
		/// Don't actually make the array until somebody tries to access it
		/// Kludge alert: assumes that _memberPictures is never modified after we initially load the object
		/// These could be any kind of picture: public member pics, FOPs, forum attachments, etc.
		/// </summary>
		public List<Dictionary<string,string>> PictureUrls {
			get {
				if ((FopGuid1 == null) && (FopGuid2 == null) && (FopGuid3 == null)) return null;
				var urls = new List<Dictionary<string, string>>();
				if (FopGuid1!=null) urls.Add( MemberPicture.FriendsOnlyPictureUrls(FopGuid1));
				if (FopGuid2!=null) urls.Add( MemberPicture.FriendsOnlyPictureUrls(FopGuid2));
				if (FopGuid3!=null) urls.Add( MemberPicture.FriendsOnlyPictureUrls(FopGuid3));
				return urls;
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
		

		public static List<Notification> Find(int idMember, int skip, int take, NotificationType notificationType=NotificationType.All, int? idClitterChannel=null) {
			Trace.WriteLine("Hello, world, from Notification#Find");
			using (var db=new ObCore.ObDb()) {
				switch (notificationType) {

					case NotificationType.BootyConClitterPosts:
						return db.Fetch<Notification>("select * from dbo.ClitterPostNotificationsEx(@0,@1,@2,1) order by id_clitter_post desc", idMember, skip, take);
					case NotificationType.AllExceptBootyCon:
						return db.Fetch<Notification>("select * from dbo.NotificationsFeed(@0,@1,@2,0) order by event_time desc", idMember, skip, take);
					case NotificationType.PrivateMessages:
						return db.Fetch<Notification>("select * from dbo.MessageNotificationsEx(@0,@1,@2) where id_notification_event_type=1 order by event_time desc", idMember, skip, take);					
					case NotificationType.Comments:
						return db.Fetch<Notification>("select * from dbo.CommentNotificationsEx(@0,@1,@2) order by event_time desc", idMember, skip, take);					
					case NotificationType.PrivateMessagesAndComments:
						// todo: Should have a SQL table-valued function just for this notification type instead of calling the "general" one
						return db.Fetch<Notification>("select * from dbo.ClitterNotificationsEx(@0,@1,@2) where id_notification_event_type in (1,2) order by event_time desc", idMember, skip, take);
					case NotificationType.Fops: 
						return db.Fetch<Notification>("select * from dbo.FopNotificationsEx(@0,@1,@2) where id_notification_event_type in (5,6) order by event_time desc", idMember, skip, take);
					case NotificationType.Friendings:
						return db.Fetch<Notification>("select * from dbo.FriendNotificationsEx(@0,@1,@2) where id_notification_event_type=4 order by event_time desc", idMember, skip, take);					
					case NotificationType.All:
						return db.Fetch<Notification>("select * from dbo.NotificationsFeed(@0,@1,@2,default) order by event_time desc", idMember, skip, take);
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
					throw new ArgumentException("Don't know that notification type! Supported types are: 'all', 'messagesandcomments', 'fops', 'friendings', 'profileviews', 'messages', and 'comments'. Default, if not specified, is 'all.'");
			}
		}

		public enum NotificationType {
			PrivateMessagesAndComments = 1000,
			Fops = 1001,
			SingleFopShared = 5,
			MultipleFopsShared = 6,
			Friendings = 4,
			ProfileViews = 3,
			PrivateMessages = 1,
			Comments = 2,
			ClitterPost = 7,
			All = 9999,
			AllExceptBootyCon = 9998,
			BootyConClitterPosts = 9997 
		}


	}

}

