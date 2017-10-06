namespace App.Config
{
    using Contracts;
    using Controllers;
    using MyWebServer.Server.Enums;
    using MyWebServer.Server.Handlers;
    using MyWebServer.Server.Routing.Contracts;

    public class AppConfig : IApplicationConfiguration
    {
        public void Start(IAppRouteConfig appRouteConfig)
        {
            // GET
            appRouteConfig.AddRoute(RequestMethod.Get, "/", new RequestHandler(httpContext => new HomeController().Index()));

            appRouteConfig.AddRoute(RequestMethod.Get, "/add", new RequestHandler(httpContext => new HomeController().AddGet()));

            appRouteConfig.AddRoute(RequestMethod.Get, "/search", new RequestHandler(httpContext => new HomeController().SearchGet()));

            appRouteConfig.AddRoute(RequestMethod.Get, "/aboutus", new RequestHandler(httpContext => new HomeController().AboutUs()));


            //POST
            appRouteConfig.AddRoute(RequestMethod.Post, "/add", new RequestHandler(httpContext => new HomeController().AddPost(httpContext)));

            appRouteConfig.AddRoute(RequestMethod.Post, "/search", new RequestHandler(httpContext => new HomeController().SearchPost(httpContext)));

        }
    }
}
