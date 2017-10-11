namespace App.Config
{
    using Contracts;
    using Controllers;
    using MyWebServer.Server.Enums;
    using MyWebServer.Server.Routing.Contracts;
    using MyWebServer.Server.Utils;
    using Views.ViewModels.Product;
    using Views.ViewModels.User;

    public class AppConfig : IApplicationConfiguration
    {
        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.AddHomePage("/register");

            // GET
            appRouteConfig.AddRoute(RequestMethod.Get, "/", httpContext => new HomeController().Index(), true);

            appRouteConfig.AddRoute(RequestMethod.Get, "/product/add", httpContext => new ProductController().Add(), true);

            appRouteConfig.AddRoute(RequestMethod.Get, "/product/search", httpContext => new ProductController().Search(httpContext), true);

            appRouteConfig.AddRoute(RequestMethod.Get, "/product/{(?<id>[0-9]+)}", httpContext => new ProductController().Details(httpContext), true);

            appRouteConfig.AddRoute(RequestMethod.Get, "/aboutus", httpContext => new HomeController().AboutUs(), true);

            appRouteConfig.AddRoute(RequestMethod.Get, "/login", httpContext => new AccountController().Login(), false);

            appRouteConfig.AddRoute(RequestMethod.Get, "/register", httpContext => new AccountController().Register(), false);

            appRouteConfig.AddRoute(RequestMethod.Get, "/profile", httpContext => new AccountController().Profile(httpContext), true);

            appRouteConfig.AddRoute(RequestMethod.Get, "/cart/order/{(?<id>[0-9]+)}", httpContext => new CartController().PutToCart(httpContext), true);

            appRouteConfig.AddRoute(RequestMethod.Get, "/cart/details", httpContext => new CartController().Details(httpContext), true);

            appRouteConfig.AddRoute(RequestMethod.Get, "/order/list", httpContext => new OrderController().List(httpContext), true);

            appRouteConfig.AddRoute(RequestMethod.Get, "/order/{(?<id>[0-9]+)}", httpContext => new OrderController().Details(httpContext), true);



            //POST
            appRouteConfig.AddRoute(RequestMethod.Post, "/product/add", httpContext => new ProductController().AddPost(new ProductAddViewModel
            {
                Name = httpContext.Request.FormData["name"],
                Price = NumberParser.ParseDecimal(httpContext.Request.FormData["price"]),
                ImageUrl = httpContext.Request.FormData["imageUrl"]
            }), true);

            appRouteConfig.AddRoute(RequestMethod.Post, "/product/search", httpContext => new ProductController().SearchPost(httpContext), true);

            appRouteConfig.AddRoute(RequestMethod.Post, "/login", httpContext => new AccountController().LoginPost(httpContext, new LoginUserViewModel
            {
                Username = httpContext.Request.FormData["username"],
                Password = httpContext.Request.FormData["password"]
                
            }), false);

            appRouteConfig.AddRoute(RequestMethod.Post, "/register", httpContext => new AccountController().RegisterPost(httpContext, new RegisterUserViewModel()
            {
                Username = httpContext.Request.FormData["username"],
                Password = httpContext.Request.FormData["password"],
                ConfirmPassword = httpContext.Request.FormData["confirmPassword"]
            }), false);

            appRouteConfig.AddRoute(RequestMethod.Post, "/order/create", httpContext => new OrderController().Create(httpContext), true);

        }
    }
}
