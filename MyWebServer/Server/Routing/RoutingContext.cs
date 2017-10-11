namespace MyWebServer.Server.Routing
{
    using System.Collections.Generic;
    using Contracts;
    using Handlers.Contracts;
    using Utils;

    public class RoutingContext : IRoutingContext
    {
        public RoutingContext(IRequestHandler requestHandler, bool userAuthenticationRequired, IList<string> parameters)
        {
            Validator.CheckIfNull(requestHandler, nameof(requestHandler));
            Validator.CheckIfNull(parameters, nameof(parameters));

            this.RequestHandler = requestHandler;
            this.UserAuthenticationRequired = userAuthenticationRequired;
            this.Parameters = parameters;
        }

        public IEnumerable<string> Parameters { get; private set; }

        public IRequestHandler RequestHandler { get; private set; }

        public bool UserAuthenticationRequired { get; private set; }
    }
}
