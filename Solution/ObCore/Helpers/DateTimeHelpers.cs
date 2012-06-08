using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObCore.Helpers {
	public static class DateTimeHelpers {
		
		public static string ToShortFriendlyDate(this System.DateTime? dt) {
			if (dt.HasValue) return ToShortFriendlyDate(dt.Value);
			return System.String.Empty;
		}
		 
		public static string ToShortFriendlyDate(this System.DateTime dt) {
			DateTime now = DateTime.Now;
			TimeSpan ts = now.Subtract(dt);

			if (ts.Days == 0) return "Today";
			if (ts.Days == 1) return "Yesterday";
			if (ts.Days == -1) return "Tomorrow";

			if (dt.Year == System.DateTime.Now.Year) return System.String.Format("{0:ddd}. {0:MMM} {1}", dt, dt.Day.ToOrdinal());   // dt.ToString("ddd MMM d");
			return dt.ToString("ddd. MMM d yyyy");

		}

		public static string ToRelativeDateDetailed(this System.DateTime dt) {
			DateTime now = DateTime.Now;
			TimeSpan ts = now.Subtract(dt);
			

			// less than 24 hours ago, just show relative date
			if (ts.TotalHours <= 48) {
				// Special cases for yesterday/tomorrow
				if ((ts.TotalHours > 24) && (ts.TotalDays <= 2)) return "Yesterday, " + dt.ToRelativeDate(48);
				return dt.ToRelativeDate();
			}

			// 1 day, and less than 24 hours, show "Yesterday"
			if (DateTime.Now.Year==dt.Year) 
				return string.Format("{0} on {1:MMM}. {2}", dt.ToRelativeDate(), dt, dt.Day.ToOrdinal());
			else 
				return string.Format("{0} on {1:MMM} {1:d} '{1:yy}", dt.ToRelativeDate(), dt);	
			
		}

		public static string ToRelativeDate(this System.DateTime? dateTime) {
			if (dateTime.HasValue) return dateTime.Value.ToRelativeDate();
			return string.Empty;
		}

		public static string ToRelativeDate(this System.DateTime dateTime, int maxHours=24) {
			System.DateTime min, max;
			string description;

			if (dateTime < System.DateTime.Now) {
				// it's in the past
				min = dateTime;
				max = System.DateTime.Now;
				description = " ago";
			}
			else {
				// it's in the future
				min = System.DateTime.Now;
				max = dateTime;
				description = " from now";
			}

			var timeSpan = max - min;

			// span is less than or equal to 60 seconds, measure in seconds.
			if (timeSpan.TotalSeconds <= 10)
				return "Just now";

			// span is less than or equal to 60 seconds, measure in seconds.
			if (timeSpan.TotalSeconds <= 60)
				return timeSpan.Seconds + " seconds" + description;

			// span is less than or equal to 60 minutes, measure in minutes.
			if (timeSpan.TotalMinutes <= 60) {
				return timeSpan.Minutes > 1
							? timeSpan.Minutes + " minutes" + description
							: "a minute" + description;
			}

			// span is less than or equal to 24 hours, measure in hours.
			if (timeSpan.TotalHours < maxHours) {
				return timeSpan.TotalHours > 1
							? Math.Round(timeSpan.TotalHours) + " hours" + description
							: "an hour" + description;
			}

			// span is less than or equal to 30 days (1 month), measure in days.
			if (timeSpan.TotalDays <= 30) {
				return timeSpan.Days > 1
							? timeSpan.Days + " days" + description
							: "a day" + description;
			}

			// span is less than two months
			if (timeSpan <= TimeSpan.FromDays(60)) {
				return timeSpan.Days > 1
					? timeSpan.Days / 7 + " weeks" + description
					: " a week" + description;
			}

			// span is less than or equal to 10 years, measure in months.
			if (timeSpan <= TimeSpan.FromDays(3650)) {
				return timeSpan.Days > 30
							? timeSpan.Days / 30 + " mon." + description
							: "a month" + description;
			}

			// span is greater than 365 days (1 year), measure in years.
			return timeSpan.Days > 365
						? timeSpan.Days / 365 + " years" + description
						: "a year" + description;
		}
	}
}

