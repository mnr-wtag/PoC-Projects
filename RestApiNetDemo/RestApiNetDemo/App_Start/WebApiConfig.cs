using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web.Http;

namespace RestApiNetDemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Information().WriteTo
                            .File("logs.txt")
                            .CreateLogger();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
