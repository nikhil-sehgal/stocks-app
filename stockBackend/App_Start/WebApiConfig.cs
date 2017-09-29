using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace stock
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Removing XML formatting
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            
            //Enabling Cross-Origin-Policy for GET Request 
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "GET");
            config.EnableCors(cors);
        }
    }
}
