﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ObCore {
	public static class Exceptions {
		[Serializable]
		public class ObAuthorizationException : Exception {
			//
			// For guidelines regarding the creation of new exception types, see
			//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
			// and
			//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
			//

			public ObAuthorizationException() {
			}

			public ObAuthorizationException(string message)
				: base(message) {
			}

			public ObAuthorizationException(string message, Exception inner)
				: base(message, inner) {
			}

			protected ObAuthorizationException(SerializationInfo info, StreamingContext context)
				: base(info, context) {
			}
		}
	}
}
