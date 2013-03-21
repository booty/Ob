using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using ObCore.Models;

namespace ObApiNancy {
	public class MemberPicturesModule : NancyModule {
		public MemberPicturesModule()
			: base("/api/MemberPictures") {
			// FOP (single)
			Get["/(?<guid>[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12})"] = parameters => {
				return Response.AsJson("That's a FOP!").AsPrivate();
			};

			// Regular picture (single)
			Get[@"/(?<idPictureMember>\d+)"] = parameters => {
				MemberPicture mp = MemberPicture.FindPublicPicture(parameters.idPictureMember, true);
				if (mp == null) return Response.AsJson(String.Empty, HttpStatusCode.NotFound);
				return Response.AsJson(mp).AsPublic();
			};

			// Multiple public pictures. By default, returns the 25 newest public pictures.
			Get["/"] = p => {
				int skip=0, take=25; // TODO: move magic numbers to web.config
				
				if (p.skip.HasValue) Int32.TryParse(p.skip, out skip);
				if (p.take.HasValue) Int32.TryParse(p.take, out take);

				List<MemberPicture> pics = MemberPicture.FindPublicPictures(skip, take);

				return Response.AsJson(pics).AsPublic();
			};

		}

	}
}