using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(LuizaLabs.App_Start.Startup))]

namespace LuizaLabs.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Configuracao WebApi
            var config = new HttpConfiguration();

            //Configurando rotas
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                  name: "DefaultApi",
                  routeTemplate: "api/{controller}/{id}",
                  defaults: new { id = RouteParameter.Optional }
             );

            //Ativando cors
            app.UseCors(CorsOptions.AllowAll);

            //Ativando configuração WebApi
            app.UseWebApi(config);
        }
    }
}
