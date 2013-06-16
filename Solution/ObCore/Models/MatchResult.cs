using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObCore.Models {
	public class MatchResult  : ObDb.Record<MatchResult> {
		[PetaPoco.Column("error_code")] public int ErrorCode { get; set; }
		[PetaPoco.Column("error_message")] string ErrorMessage  { get; set; }
		[PetaPoco.Column("is_mutual")] bool IsMutual { get; set; }
		[PetaPoco.Column("max_match_per_24_hours")] int MaximumMatchesPer24Hours { get; set; }


	}
}
