using Serilog;
using Serilog.Formatting.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Datacenter.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            string logPath = string.Format(@"{0}\log.txt", ConfigurationHelper.ReadProperty("LogPath"));

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.RollingFile(new JsonFormatter(renderMessage: true), logPath)
                .Enrich.WithWebApiActionName()
                .Enrich.WithWebApiControllerName()
                .Enrich.WithWebApiRouteData()
                .Enrich.WithWebApiRouteTemplate()
                .CreateLogger();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }

    public class ConfigurationHelper
    {
        public static bool HasProperty(string key)
        {
            return !String.IsNullOrWhiteSpace(key) && ConfigurationManager.AppSettings.AllKeys.Select((string x) => x).Contains(key);
        }

        public static string ReadProperty(string key)
        {
            if (HasProperty(key))
                return ConfigurationManager.AppSettings[key];

            return string.Empty;
        }
    }
}
