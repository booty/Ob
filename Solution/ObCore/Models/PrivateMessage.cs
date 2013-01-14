using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ObCore.Models;

namespace ObCore.Models {
	public class PrivateMessage : ObDb.Record<PrivateMessage> {
		[PetaPoco.Column("ID_Message")]
		public int IdMessage {
			get;
			set;
		}
		[PetaPoco.Column("ID_Member_From")]
		public int IdMemberSender {
			get;
			set;
		}
		[PetaPoco.Column("ID_Member_To")]
		public int IdMemberRecipient {
			get;
			set;
		}
		[PetaPoco.Column("ID_Picture_Member_To")]
		public int? IdPictureMemberRecipient {
			get;
			set;
		}
		[PetaPoco.Column("ID_Picture_Member_From")]
		public int? IdPictureMemberSender {
			get;
			set;
		}
		[PetaPoco.Column("ID_Message_Reply_To")]
		public int IdMessageReplyTo {
			get;
			set;
		}
		[PetaPoco.Column("Read_Count")]
		public int ReadCount {
			get;
			set;
		}
		[PetaPoco.Column("Timestamp_Created")]
		public DateTime Created {
			get;
			set;
		}
		[PetaPoco.Column("Timestamp_Read")]
		public DateTime? Read {
			get;
			set;
		}
		[PetaPoco.Column("Validation_Code")]
		public string ValidationCode {
			get;
			set;
		}
		[PetaPoco.Column("Timestamp_Delete_Sender")]
		public DateTime? DeletedBySender {
			get;
			set;
		}
		[PetaPoco.Column("Timestamp_Delete_Recipient")]
		public DateTime? DeletedByRecipient {
			get;
			set;
		}
		[PetaPoco.Column("MessageAge")]
		public string MessageAge {
			get;
			set;
		}
		[PetaPoco.Column("ReadAge")]
		public string ReadAge {
			get;
			set;
		}
		public string Subject {
			get;
			set;
		}
		public string Body {
			get;
			set;
		}

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

		public string SenderPictureUrl {
			get {
				return MemberPicture.PublicPictureUrl(this.IdPictureMemberSender, PictureSize.Small50Px);
			}
		}

		public string RecipientPictureUrl {
			get {
				return MemberPicture.PublicPictureUrl(this.IdPictureMemberRecipient, PictureSize.Small50Px);
			}
		}


		public void MarkAsRead() {
			using (var db = new ObDb()) {
				db.Execute("execute Message_Mark_As_Read @@id_message=" + this.IdMessage);
			}
		}

		public static void MarkAsReadForMember(int idMessage, int idMember, ObDb obDb = null) {
			string sql = "execute Message_Mark_As_Read @@id_message=@idMessage, @@id_member=@idMember";

			if (obDb != null) {
				obDb.Execute(sql, new {
					idMessage = idMessage,
					idMember = idMember
				});
				return;
			}
			using (var db = new ObDb()) {
				db.Execute(sql, new {
					idMessage = idMessage,
					idMember = idMember
				});
			}
		}

		public static void MarkAsRead(int idMessage, ObDb obDb = null) {
			string sql = "execute Message_Mark_As_Read " + idMessage;
			if (obDb != null) {
				obDb.Execute(sql);
				return;
			}
			using (var db = new ObDb()) {
				db.Execute(sql);
			}
		}

		public static void Delete(int idMessage, int idMember) {
			using (var db = new ObDb()) {
				db.Execute("exec message_delete @@id_member=@idMember, @@id_message=@idMessage", new {
					idMessage = idMessage,
					idMember = idMember
				});
			}
		}



		/// <summary>
		/// Retrieves a private message, but only if idMemberCurrent has permission to view it
		/// (ie, if they're the sender or recipient)
		/// </summary>
		/// <param name="idMemberCurrent">The member we're retrieving the message for</param>
		/// <param name="idMessage">The message we're retrieving</param>
		/// <param name="markAsRead">Should we mark this message as read? Ignored if 
		/// idMemberCurrent is the sender; "sent" pertains to whether or not the recipient has read the message.
		/// </param>
		/// <returns>A PrivateMessage, or null.</returns>
		public static PrivateMessage FindForMember(int idMemberCurrent, int idMessage, bool markAsRead = false) {
			using (var db = new ObDb()) {
				var privateMessage = db.SingleOrDefault<PrivateMessage>("select * from messageview where id_message = @idMessage and @idMember in (id_member_from, id_member_to)", new {
					idMessage = idMessage,
					idMember = idMemberCurrent
				});

				if (privateMessage == null) return null;

				if ((markAsRead) && (privateMessage.IdMemberRecipient == idMemberCurrent)) {
					MarkAsRead(idMessage);
				}

				return privateMessage;
			}
		}

		/// <summary>
		/// Shouldn't call this directly. Instead, obtain a reference to the member and call member.SendPrivateMessage
		/// </summary>
		/// <param name="idMemberTo">Member we're sending to</param>
		/// <param name="idMemberFrom">Member we're sending from</param>
		/// <param name="subject">Subject of the message</param>
		/// <param name="body">Body of the message</param>
		/// <param name="idMessageReplyTo">Optional - message we're replying to</param>
		/// <returns></returns>
		static internal PrivateMessage Create(int idMemberTo, int idMemberFrom, string subject, string body, int? idMessageReplyTo) {

			using (var db = new ObDb()) {
				var result = db.ExecuteScalar<dynamic>(@"declare @@id_message int, @@rc int, @@error varchar(512); 
					exec @@rc=Message_Insert 
					@@id_message=@@id_message output,
					@@id_member_from=@idMemberFrom,
					@@id_member_to=@idMemberTo,
					@@subject=@subject,
					@@body=@body,
					@@id_message_reply_to=@idMessageReplyTo,
					@@error=@@error output; 
					select @@rc as rc, @@id_message as id_message, @@error as error",
				new {
					idMemberFrom = idMemberFrom,
					idMemberTo = idMemberTo,
					subject = subject,
					body = body,
					idMessageReplyTo = idMessageReplyTo
				}
				);

				if (result.rc == 0) {
					return PrivateMessage.Find(result.idMessage);
				}

				throw new Exception(result.error);
			}
		}


		/// <summary>
		/// Important: don't call this unless you're sure the message belongs to the current member.
		/// Should be used in ADMIN/SUPERMOD/WHATEVER code ONLY
		/// Use FindForMember if you're using public-facing code
		/// </summary>
		/// <param name="idMessage">The message we're retrieving</param>
		/// <param name="markAsRead">Should we mark this message as read? Ignored if 
		/// idMemberCurrent is the sender; "sent" pertains to whether or not the recipient has read the message.
		/// </param>
		/// <returns>A PrivateMessage, or null.</returns>
		internal static PrivateMessage Find(int idMessage) {
			using (var db = new ObDb()) {
				return db.First<PrivateMessage>("select * from MessageView where id_message=@idMessage", idMessage);
			}
		}

		internal static PrivateMessage FindAndMarkAsRead(int idMessage) {
			using (var db = new ObDb()) {
				MarkAsRead(idMessage, db);
				return db.First<PrivateMessage>("select * from MessageView where id_message=@idMessage", idMessage);
			}
		}

		/// <summary>
		/// Returns a list of private messages for a given recipient.
		/// </summary>
		/// <param name="idMemberRecipient">Messages for this member</param>
		/// <param name="skip">How many to skip (used for paging).</param>
		/// <param name="take">How many to retrieve (used for paging, etc)</param>
		/// <param name="unreadOnly">Only return unread messages?</param>
		/// <param name="includeDeleted">Include deleted?</param>
		/// <returns></returns>
		public static List<PrivateMessage> FindByRecipient(int idMemberRecipient, int skip = 0, int take = 50, bool unreadOnly = false) {
			return Find(idMemberRecipient, unreadOnly, false, skip, take);
		}

		/// <summary>
		/// Returns a list of private messages sent by a given member.
		/// </summary>
		/// <param name="idMemberSender">Member who sent the messages.</param>
		/// <param name="skip">Number to skip</param>
		/// <param name="take">Number to take</param>
		/// <param name="unreadOnly">Do you want all messages, or just the unread ones?</param>
		/// <param name="includeDeleted">Include deleted messages?</param>
		/// <returns></returns>
		public static List<PrivateMessage> FindBySender(int idMemberSender, int skip = 0, int take = 50, bool unreadOnly = false, bool includeDeleted = false) {
			return Find(idMemberSender, unreadOnly, true, skip, take);
		}

		public static List<PrivateMessage> Find(int? idMember,
			bool unreadOnly = false,
			bool sent = false, // default is messages received by this sender
			int skip = 0,
			int take = 50
			) {

			using (var db = new ObDb()) {
				return db.Fetch<PrivateMessage>("select * from dbo.Messages(@id_member, @unread_only, @sent, @skip, @take) order by id_message desc", new {
					id_member = idMember,
					unread_only = unreadOnly,
					sent = sent,
					skip = skip,
					take = take
				});
			}
		}
	}

}
