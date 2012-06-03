using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ObCore.Models {
	public class User : IPrincipal {
		protected User() { }

		public User(int userId, string userName, string fullName, string password) {
			UserId = userId;
			UserName = userName;
			FullName = fullName;
			Password = password;
		}
		public virtual int UserId { get; set; }
		public virtual string UserName { get; set; }
		public virtual string FullName { get; set; }
		public virtual string Password { get; set; }

		public virtual IIdentity Identity {
			get;
			set;
		}

		public virtual bool IsInRole(string role) {
			/*
			 * if (Role.Description.ToLower() == role.ToLower())
				return true;

			foreach (Right right in Role.Rights) {
				if (right.Description.ToLower() == role.ToLower())
					return true;
			}
			 * */
			return false;
		}

	}





}