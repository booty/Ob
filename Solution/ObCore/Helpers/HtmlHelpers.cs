using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ObCore.Helpers {
	public static class HtmlHelpers {
		public static string CurrentController(this HtmlHelper htmlHelper) {
			return htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
		}



		public static IHtmlString DisplayName<TModel, TValue>(
									  this HtmlHelper<TModel> html,
									  Expression<Func<TModel, TValue>> expression) {
			var metadata = ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, html.ViewData);
			return new HtmlString(metadata.DisplayName);
		}

	}
}
