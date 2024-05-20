using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace testeDealer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Rota padrão para a página inicial
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            // Rotas para os controllers de Cliente, Produto e Venda
            routes.MapRoute(
                name: "Cliente",
                url: "Cliente/{action}/{id}",
                defaults: new { controller = "Cliente", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Produto",
                url: "Produto/{action}/{id}",
                defaults: new { controller = "Produto", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Venda",
                url: "Venda/{action}/{id}",
                defaults: new { controller = "Venda", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
