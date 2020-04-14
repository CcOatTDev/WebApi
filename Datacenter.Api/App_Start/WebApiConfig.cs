using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Datacenter.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            SetIgnoreReferenceLoopHandling(config);
            SetGlobalContentType(config);
            //config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            MapHttpApiRoute(config);

            // Redirect to Swagger UI
            RedirectToSwaggerUI(config);
        }

        private static void RedirectToSwaggerUI(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler(SwaggerDocsConfig.DefaultRootUrlResolver, "swagger/ui/index")
            );
        }

        private static void MapHttpApiRoute(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{*id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void SetGlobalContentType(HttpConfiguration config)
        {
            var mediaType = new MediaTypeHeaderValue("application/json");
            var formatter = new JsonMediaTypeFormatter();

            formatter.SupportedMediaTypes.Clear();
            formatter.SupportedMediaTypes.Add(mediaType);

            config.Formatters.Clear();
            config.Formatters.Add(formatter);
        }

        private static void SetIgnoreReferenceLoopHandling(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}
