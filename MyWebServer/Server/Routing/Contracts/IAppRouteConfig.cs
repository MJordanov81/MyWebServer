namespace MyWebServer.Server.Routing.Contracts
{
    using System.Collections.Generic;
    using Enums;
    using Handlers.Contracts;

    public interface IAppRouteConfig
    {
        IReadOnlyDictionary<RequestMethod, IDictionary<string, IRequestHandler>> Routes { get; }

        void AddRoute(RequestMethod methodType, string route, IRequestHandler requestHandler);
    }
}
