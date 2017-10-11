namespace MyWebServer.Server.Handlers
{
    using Contracts;
    using Enums;
    using HTTP.Contracts;
    using HTTP.Response;
    using Routing.Contracts;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Utils;
    using Views;

    public class HttpHandler : IRequestHandler
    {
        private readonly IServerRouteConfig serverRouteConfig;

        public HttpHandler(IServerRouteConfig serverRouteConfig)
        {
            Validator.CheckIfNull(serverRouteConfig, nameof(serverRouteConfig));

            this.serverRouteConfig = serverRouteConfig;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            RequestMethod method = httpContext.Request.RequestMethod;
            string path = httpContext.Request.Path;

            bool isAuthenticated = false;

            if (httpContext.Request.Session != null)
            {
                isAuthenticated = httpContext.Request.Session.IsAuthenticated();
            }

            foreach (KeyValuePair<string, IRoutingContext> kvp in this.serverRouteConfig.Routes[method])
            {
                string pattern = kvp.Key;

                Regex regex = new Regex(pattern);

                Match match = regex.Match(path);

                if (!match.Success)
                {
                    continue;
                }

                // Check if path requires authentication and if it does and client is not logged-in then it is redirected to login page
                if (!isAuthenticated && kvp.Value.UserAuthenticationRequired)
                {
                    return new RedirectResponse(this.serverRouteConfig.HomePage);
                }

                foreach (string parameter in kvp.Value.Parameters)
                {
                    httpContext.Request.AddUrlParameter(parameter, match.Groups[parameter].Value);
                }

                return kvp.Value.RequestHandler.Handle(httpContext);
            }

            return new PageNotFoundResponse(ResponseStatusCode.NotFound, new PageNotFoundView());
        }
    }
}
