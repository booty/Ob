/*
 * Example of MVC-style action value binder.
 * From: http://blogs.msdn.com/b/jmstall/archive/2012/04/18/mvc-style-parameter-binding-for-webapi.aspx
 * 
 * Specify it for your controllers via an attribute, like so: 
 * [HttpControllerConfiguration(ActionValueBinder=typeof(MvcActionValueBinder))]
    public class MvcController : ApiController    {
        [HttpGet]
        public void Combined(Customer item) { }
    }
 * 
 * Why do we need this?
 * 
 * Well, unlike "regular" ASP.NET MVC, the request body is not buffered.
 * It is only read *once* because it is treated like a forward-only stream.
 * If you want multiple parameters bound, you need to provide a custom binder like this.
 * 
 * 
 * 
 
 */
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders.Providers;
using System.Web.Mvc;
using WebApiContrib.ModelBinders;
using IModelBinder = System.Web.Http.ModelBinding.IModelBinder;
using IValueProvider = System.Web.Http.ValueProviders.IValueProvider;
using ModelBinderAttribute = System.Web.Http.ModelBinding.ModelBinderAttribute;
using ValueProviderFactory = System.Web.Http.ValueProviders.ValueProviderFactory;

namespace ObApi {


	// from https://github.com/panesofglass/WebAPIContrib/commit/7c42d3a0dcd664f745a3b348622025590792d10c
	public class MvcStyleBindingAttribute : Attribute, IControllerConfiguration {
		public void Initialize(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor) {
			controllerSettings.Services.Replace(typeof(IActionValueBinder), new MvcActionValueBinder());
		}
	}


}
