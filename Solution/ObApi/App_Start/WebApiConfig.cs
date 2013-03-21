using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Tracing;
using Newtonsoft.Json.Serialization;
using WebApiContrib.Formatting;
using ITraceWriter = Newtonsoft.Json.Serialization.ITraceWriter;

namespace ObApi {
	public static class WebApiConfig {

		/*
		 * Make Json the default; not XML
		 * as per: http://stackoverflow.com/questions/9847564/how-do-i-get-mvc-4-webapi-to-return-json-instead-of-xml-using-chrome
		 * */

		public static void Register(HttpConfiguration config) {
			RouteConfig.RegisterRoutes(config.Routes);

			config.Formatters.Add(new PlainTextFormatter());

			var json = config.Formatters.JsonFormatter;
			json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
			json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

			var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
			config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

			// Tracing
			//config.Services.Replace(typeof(ITraceWriter), new SimpleTracer());
			config.EnableSystemDiagnosticsTracing();

		}
	}


}
