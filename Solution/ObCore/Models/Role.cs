using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ObCore.Models {
	public class Role {
		protected Role() { }

		public Role(int roleid, string roleDescription) {
			RoleId = roleid;
			Description = roleDescription;
		}
		public virtual int RoleId { get; set; }
		public virtual string Description { get; set; }
		public virtual IList<Right> Rights { get; set; }
	}


}