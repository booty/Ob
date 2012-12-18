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
		[PetaPoco.Column("ID_Picture_Member_To")] public int? IdPictureMemberRecipient { get; set; }
		[PetaPoco.Column("ID_Picture_Member_From")] public int? IdPictureMemberSender { get; set; }
		[PetaPoco.Column("ID_Message_Reply_To")] public int IdMessageReplyTo { get; set; }
		[PetaPoco.Column("Read_Count")] public int ReadCount { get; set; }
		[PetaPoco.Column("Timestamp_Created")] public DateTime Created { get; set; }
		[PetaPoco.Column("Timestamp_Read")] public DateTime Read { get; set; }
		[PetaPoco.Column("Validation_Code")] public string ValidationCode { get; set; }
		[PetaPoco.Column("Timestamp_Delete_Sender")] public DateTime DeletedBySender { get; set; }
		[PetaPoco.Column("Timestamp_Delete_Recipient")] public DateTime DeletedByRecipient { get; set; }
		[PetaPoco.Column("MessageAge")]public string MessageAge { get; set; }
		[PetaPoco.Column("ReadAge")]public string ReadAge { get; set; }
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="idMemberCurrent"></param>
		/// <param name="idMessage"></param>
		/// <param name="markAsRead"></param>
		/// <returns></returns>
		public static PrivateMessage FindForMember(int idMemberCurrent, int idMessage, bool markAsRead = false) {
			using (var db = new ObDb()) {
				var privateMessage = db.SingleOrDefault<PrivateMessage>("select * from messageview where id_message = @idMessage and @idMember in (id_member_from, id_member_to)", new {
					idMessage=idMessage,
					idMember=idMemberCurrent
				});

				if (privateMessage==null) return null;
				
				if ((markAsRead) && (privateMessage.IdMemberRecipient==idMemberCurrent)) {
					db.Execute("execute Message_Mark_As_Read " + idMessage);
				}

				return privateMessage;
			}
		}

		/// <summary>
		/// Important: don't call this unless you're sure the message belongs to the current member.
		/// Should be used in ADMIN/SUPERMOD/WHATEVER code ONLY
		/// Use FindForMember if you're using public-facing code
		/// </summary>
		/// <param name="idMessage"></param>
		/// <param name="markAsRead"></param>
		/// <returns></returns>
		public static PrivateMessage Find(int idMessage, bool markAsRead=false) {
			throw new NotImplementedException();
		}

		/// <summary>
		/// Returns a list of private messages for a given recipient.
		/// </summary>
		/// <param name="idMemberRecipient">Messages for this member</param>
		/// <param name="skip"></param>
		/// <param name="take"></param>
		/// <param name="unreadOnly"></param>
		/// <param name="includeDeleted"></param>
		/// <returns></returns>
		public static List<PrivateMessage> FindByRecipient(int idMemberRecipient, int skip = 0, int take = 50, bool unreadOnly = false, bool includeDeleted=false) {
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
		public static List<PrivateMessage> FindBySender(int idMemberSender, int skip = 0, int take = 50, bool unreadOnly = false, bool includeDeleted=false) {
			return Find(idMemberSender, unreadOnly, true, skip, take);
		}
		
		public static List<PrivateMessage> Find(int? idMember,
 			bool unreadOnly = false,
			bool sent=false, // default is messages received by this sender
			int skip = 0, 
			int take = 50
			) {

			using (var db = new ObDb()) {
				return db.Fetch<PrivateMessage>("select * from dbo.Messages(@id_member, @unread_only, @sent, @skip, @take) order by id_message desc", new {
					id_member=idMember,
					unread_only=unreadOnly,
					sent=sent,
					skip=skip,
					take=take
				});
			}
		}
	}

}
