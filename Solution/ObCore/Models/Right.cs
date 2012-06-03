using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ObCore.Models {
	public class Right {
		protected Right() { }

		public Right(int rightId, string description) {
			RightId = rightId;
			Description = description;
		}

		public virtual int RightId { get; set; }
		public virtual string Description { get; set; }

	}
}