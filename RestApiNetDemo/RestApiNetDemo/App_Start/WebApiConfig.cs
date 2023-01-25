using RestApiNetDemo.IoC;
using Serilog;
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
            config.Filters.Add(new AuthorizeAttribute());

            config.DependencyResolver = new NinjectResolver();
            

            // config.DependencyResolver = new IocRegistrations(IServiceCollection services, string configLocation);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
