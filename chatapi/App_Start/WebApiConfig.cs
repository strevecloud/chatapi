using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;
using chatapi.App_Start;
using System.Web.Configuration;

namespace chatapi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            // Web API routes
            config.MapHttpAttributeRoutes();
            var ClientID = WebConfigurationManager.AppSettings["auth0:ClientId"];
            var clientsecret = WebConfigurationManager.AppSettings["auth0:ClientSecret"];
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.MessageHandlers.Add(new JsonWebTokenValidationHandler()
            {
                Audience = ClientID,
                SymmetricKey = clientsecret,
                IsSecretBase64Encoded = false
            });
            config.EnableCors(cors);
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //FluentValidationModelValidatorProvider.Configure(config);
        }
    }
}
