using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using PetaPoco;

/*
Messages and comments
*/

namespace ObCore.Models {
	//[PrimaryKey("ID_Member_To", autoIncrement = false)]
	[ExplicitColumns]
	public partial class MessageNotification : ObDb.Record<MessageNotification> {
		// total_unseen,messages_unseen,comments_unseen,last_message_seen,Login,id_picture_member,event_time_relative,event_epoch,Body,Subject,event_time,Timestamp_Read,ID_Member_From,ID_Message,ID_Comment

		[PetaPoco.Column]
		public string Login { get; set; }
		[PetaPoco.Column("id_picture_member")]
		public int IdPictureMember { get; set; }
		[PetaPoco.Column("event_time_relative")]
		public string EventTimeRelative { get; set; }
		[PetaPoco.Column]
		public string Body { get; set; }
		[PetaPoco.Column]
		public string Subject { get; set; }
		[PetaPoco.Column("event_time")]
		public DateTime EventTime { get; set; }
		[PetaPoco.Column("timestamp_read")]
		public DateTime? TimestampRead { get; set; }
		[PetaPoco.Column("id_member_from")]
		public int IdMemberFrom { get; set; }
		[PetaPoco.Column("id_message")]
		public int IdMessage { get; set; }
		[PetaPoco.Column("id_comment")]
		public int IdComment { get; set; }

		public static List<MessageNotification> Fetch(int idMember) {
			using (var db = new ObCore.ObDb()) {
				return db.Fetch<MessageNotification>("select * from dbo.ToolbarMessages(@0) order by event_time desc", idMember);
			}
		}
	}
}
