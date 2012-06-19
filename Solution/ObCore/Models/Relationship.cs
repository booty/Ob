using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ObCore.Models {
	[TableName("MemberBasic")]
	[ExplicitColumns]
	public class Relationship {
		//[PetaPoco.Column("Member1_ID_Member")]
		public int Member1IdMember { get; set; }
		//[PetaPoco.Column("Member2_ID_Member")]
		public int Member2IdMember { get; set; }
		//[PetaPoco.Column("Member1_Login")]
		public string Member1Login { get; set; }
		//[PetaPoco.Column("Member2_Login")]
		public string Member2Login { get; set; }
		//[PetaPoco.Column("Member1_Age")]
		public int Member1Age { get; set; }
		//[PetaPoco.Column("Member2_Age")]
		public int Member2Age { get; set; }
		//[PetaPoco.Column("Member1_Is_Friended")]
		public bool Member1IsFriended { get; set; }
		//[PetaPoco.Column("Member2_Is_Friended")]
		public bool Member2IsFriended { get; set; }
		//[PetaPoco.Column("Member1_Fops_Visible")]
		public bool Member1FopsVisible { get; set; }
		//[PetaPoco.Column("Member1_Fops_Blocked")]
		public bool? Member1FopsBlocked { get; set; }
		//[PetaPoco.Column("Member1_Fop_Count")]
		public int Member1FopCount { get; set; }
		//[PetaPoco.Column("Member1_Adult_Comments_Status_Description")]
		public string Member1AdultCommentsStatusDescription { get; set; }
		//[PetaPoco.Column("Member1_Can_Recieve_Adult_Comments")]
		public bool Member1CanRecieveAdultComments { get; set; }
		//[PetaPoco.Column("Member2_Fops_Visible")]
		public bool Member2FopsVisible { get; set; }
		//[PetaPoco.Column("Member2_Fops_Blocked")]
		public bool? Member2FopsBlocked { get; set; }
		//[PetaPoco.Column("Member2_Fop_Count")]
		public int Member2FopCount { get; set; }
		//[PetaPoco.Column("Member2_Adult_Comments_Status_Description")]
		public string Member2AdultCommentsStatusDescription { get; set; }
		//[PetaPoco.Column("Member2_Can_Recieve_Adult_Comments")]
		public bool Member2CanRecieveAdultComments { get; set; }

		public static Relationship Find(int idMember1, int idMember2) {
			using (var db=new ObDb()) {
				return db.First<Relationship>("select * from dbo.Relationship(@0,@1)", idMember1, idMember2);
			}
		}
	}


}
