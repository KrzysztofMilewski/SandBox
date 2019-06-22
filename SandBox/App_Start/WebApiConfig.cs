using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace SandBox
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var settings = config.Formatters.JsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Formatting = Formatting.Indented;


            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "UsersSugestions",
                routeTemplate: "api/users/{nameQuery}",
                defaults: new { controller = "Users", action = "GetUsers" }
            );


            config.Routes.MapHttpRoute(
                name: "Subscriptions",
                routeTemplate: "api/subscriptions",
                defaults: new { controller = "Subscriptions", action = "GetSubscriptions" }
            );

            config.Routes.MapHttpRoute(
                name: "Subscribers",
                routeTemplate: "api/subscribers",
                defaults: new { controller = "Subscriptions", action = "GetMySubscribers" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
