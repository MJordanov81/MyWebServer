namespace MyWebServer.Server.Routing
{
    using Contracts;
    using Enums;
    using Handlers.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            Validator.CheckIfNull(methodType, nameof(methodType));
            Validator.CheckIfNullOrEmpty(route, nameof(route));
            Validator.CheckIfNull(requestHandler, nameof(requestHandler));

            if (methodType == RequestMethod.Get)
            {
                this.routes[RequestMethod.Get].Add(route, requestHandler);
            }
            else if (methodType == RequestMethod.Post)
            {
                this.routes[RequestMethod.Post].Add(route, requestHandler);
            }
        }
    }
}
