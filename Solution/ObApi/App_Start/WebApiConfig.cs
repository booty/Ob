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
			// Routes
			config.Routes.MapHttpRoute(
				 name: "DefaultApi",
				 routeTemplate: "api/{controller}/{id}",
				 defaults: new {
					 id = RouteParameter.Optional
				 }
			);

			config.Formatters.Add(new PlainTextFormatter());

			var json = config.Formatters.JsonFormatter;
			json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
			json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

			var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
			config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

			// Tracing
			//config.Services.Replace(typeof(ITraceWriter), new SimpleTracer());

		}
	}

	/*
	public class SimpleTracer : ITraceWriter {
		public void Trace(HttpRequestMessage request, string category, TraceLevel level,
			 Action<TraceRecord> traceAction) {
			TraceRecord rec = new TraceRecord(request, category, level);
			traceAction(rec);
			WriteTrace(rec);
		}

		protected void WriteTrace(TraceRecord rec) {
			var message = string.Format("{0};{1};{2}",
				 rec.Operator, rec.Operation, rec.Message);
			System.Diagnostics.Trace.WriteLine(message, rec.Category);
		}

		#region ITraceWriter Members

		public System.Diagnostics.TraceLevel LevelFilter {
			get {
				throw new NotImplementedException();
			}
		}

		public void Trace(System.Diagnostics.TraceLevel level, string message, Exception ex) {
			throw new NotImplementedException();
		}

		#endregion
	}
	*/
	/*
	public static void Register(HttpConfiguration config) {
		config.Routes.MapHttpRoute(
			 name: "DefaultApi",
			 routeTemplate: "api/{controller}/{id}",
			 defaults: new {
				 id = RouteParameter.Optional
			 }
		);
	}
	 * */

}
