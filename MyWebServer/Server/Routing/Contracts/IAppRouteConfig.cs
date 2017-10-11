namespace MyWebServer.Server.Routing.Contracts
{
    using Enums;
    using HTTP.Contracts;
    using System;
    using System.Collections.Generic;

    public interface IAppRouteConfig
    {
        IReadOnlyDictionary<RequestMethod, IDictionary<string, IAppRoutingContext>> Routes { get; }

        string HomePage { get; }

        void AddRoute(RequestMethod methodType, string route, Func<IHttpContext, IHttpResponse> func, bool userAuthenticationRequired);

        // server will automatically redirect to this page in case url needs authentication and there is no logged in user
        void AddHomePage(string homePage);
    }
}
