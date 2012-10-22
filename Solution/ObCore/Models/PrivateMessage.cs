using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ObCore.Models;

namespace ObCore.Models {
	public class PrivateMessage : ObDb.Record<PrivateMessage> {
		[PetaPoco.Column("ID_Message")] public int IdMessage { get; set; }
		[PetaPoco.Column("ID_Member_From")] public int IdMemberSender { get; set; }
		[PetaPoco.Column("ID_Member_To")] public int IdMemberRecipient { get; set; }
		[PetaPoco.Column("ID_Message_Reply_To")] public int IdMessageReplyTo { get; set; }
		[PetaPoco.Column("Read_Count")] public int ReadCount { get; set; }
		[PetaPoco.Column("Timestamp_Created")] public DateTime Created { get; set; }
		[PetaPoco.Column("Timestamp_Read")] public DateTime Read { get; set; }
		[PetaPoco.Column("Validation_Code")] public string ValidationCode { get; set; }
		[PetaPoco.Column("Timestamp_Delete_Sender")] public DateTime DeletedBySender { get; set; }
		[PetaPoco.Column("Timestamp_Delete_Recipient")] public DateTime DeletedByRecipient { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }

		[JsonIgnore]
		public Member Sender {
			get {
				return Member.Find(IdMemberSender);
			}
		}

		[JsonIgnore] 
		public Member Recipient {
			get {
				return Member.Find(IdMemberRecipient);
			}
		}

		public static PrivateMessage Find(int idMessage, bool markAsRead=false) {
			throw new NotImplementedException();
		}

		public static List<PrivateMessage> FindByRecipient(int idMemberRecipient, int skip = 0, int limit = 50, bool unreadOnly = false, bool includeDeleted=false) {
			return Find(null, idMemberRecipient, skip, limit, unreadOnly, true, includeDeleted);
		}

		public static List<PrivateMessage> FindBySender(int idMemberSender, int skip = 0, int limit = 50, bool unreadOnly = false, bool includeDeleted=false) {
			return Find(idMemberSender, null, skip, limit, unreadOnly, includeDeleted, true);
		}

		public static List<PrivateMessage> Find(int? idMemberSender, int? idMemberRecipient, int skip = 0, int limit = 50, bool unreadOnly = false, bool includeDeletedBySender = false, bool includeDeletedByRecipient = false) {
			if (skip != 0) throw new NotImplementedException("Whoops; not supporting skip yet.");
			if (!idMemberSender.HasValue && !idMemberRecipient.HasValue) throw new ArgumentException("Must specify the sender, the recipient, or both. Specifying neither just won't fly.");

			using (var db = new ObDb()) {
				var sql = new StringBuilder("select top " + limit.ToString() + " id_message, id_member_from, id_member_to, id_message_reply_to, subject, body, read_count, timestamp_created, timestamp_read, timestamp_delete_sender, timestamp_delete_recipient from Message where 1=1 ");
				if (idMemberSender.HasValue) sql.AppendFormat(" and id_member_from={0} ", idMemberSender);
				if (idMemberRecipient.HasValue) sql.AppendFormat(" and id_member_to={0} ", idMemberRecipient);
				if (unreadOnly) sql.Append(" and timestamp_read is null ");
				if (!includeDeletedBySender) sql.Append(" and Timestamp_Delete_Sender is null "); 
				if (!includeDeletedByRecipient) sql.Append(" and Timestamp_Delete_Recipient is null ");
				sql.Append(" order by id_message desc");
				return db.Fetch<PrivateMessage>(sql.ToString());
			}
		}
	}

}
