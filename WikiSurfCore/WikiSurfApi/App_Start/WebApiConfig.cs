using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WikiSurfApi.App_Start;

namespace WikiSurfApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.DependencyResolver = new NInjectResolver();
            // Web API routes
            config.MapHttpAttributeRoutes();
            var cors = new EnableCorsAttribute("*","*","*");
            config.EnableCors(cors);
            config.MessageHandlers.Add(new SessionAuthenticateMiddleware());
            config.MessageHandlers.Add(new ResponseFormatter());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
