namespace MyWebServer.Server.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Enums;
    using Handlers.Contracts;
    using Utils;

    public class AppRouteConfig : IAppRouteConfig
    {
        private readonly Dictionary<RequestMethod, IDictionary<string, IRequestHandler>> routes;

        public AppRouteConfig()
        {
            this.routes = new Dictionary<RequestMethod, IDictionary<string, IRequestHandler>>();

            foreach (RequestMethod method in Enum.GetValues(typeof(RequestMethod)).Cast<RequestMethod>())
            {
                this.routes[method] = new Dictionary<string, IRequestHandler>();
            }
        }

        public IReadOnlyDictionary<RequestMethod, IDictionary<string, IRequestHandler>> Routes => this.routes;

        public void AddRoute(RequestMethod methodType, string route, IRequestHandler requestHandler)
        {
            Validator.CheckIfNull(methodType);
            Validator.CheckIfNullOrEmpty(route);
            Validator.CheckIfNull(requestHandler);

            if (methodType.ToString().ToLower().Contains("get"))
            {
                this.routes[RequestMethod.Get].Add(route, requestHandler);
            }
            else if (methodType.ToString().ToLower().Contains("post"))
            {
                this.routes[RequestMethod.Post].Add(route, requestHandler);
            }
        }
    }
}
