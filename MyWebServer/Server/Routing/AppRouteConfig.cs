namespace MyWebServer.Server.Routing
{
    using Contracts;
    using Enums;
    using Handlers.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Handlers;
    using HTTP.Contracts;
    using Utils;

    public class AppRouteConfig : IAppRouteConfig
    {
        private readonly Dictionary<RequestMethod, IDictionary<string, IAppRoutingContext>> routes;

        private string homepage = "/";

        public AppRouteConfig()
        {
            this.routes = new Dictionary<RequestMethod, IDictionary<string, IAppRoutingContext>>();

            foreach (RequestMethod method in Enum.GetValues(typeof(RequestMethod)).Cast<RequestMethod>())
            {
                this.routes[method] = new Dictionary<string, IAppRoutingContext>();
            }
        }

        public IReadOnlyDictionary<RequestMethod, IDictionary<string, IAppRoutingContext>> Routes => this.routes;

        public string HomePage
        {
            get { return this.homepage; }
            private set { this.homepage = value; }
        }

        public void AddRoute(RequestMethod methodType, string route, Func<IHttpContext, IHttpResponse> func, bool userAuthenticationRequired)
        {
            Validator.CheckIfNull(methodType, nameof(methodType));
            Validator.CheckIfNullOrEmpty(route, nameof(route));
            Validator.CheckIfNull(func, nameof(func));

            IRequestHandler requestHandler = new RequestHandler(func);

            if (methodType == RequestMethod.Get)
            {
                this.routes[RequestMethod.Get].Add(route, new AppRoutingContext(requestHandler, userAuthenticationRequired));
            }
            else if (methodType == RequestMethod.Post)
            {
                this.routes[RequestMethod.Post].Add(route, new AppRoutingContext(requestHandler, userAuthenticationRequired));
            }
        }

        public void AddHomePage(string homePage)
        {
            Validator.CheckIfNullOrEmpty(homePage, nameof(homePage));

            this.homepage = homePage;
        }
    }
}
