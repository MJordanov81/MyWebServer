namespace MyWebServer.Server.Routing
{
    using System.Collections.Generic;
    using Contracts;
    using Handlers.Contracts;
    using Utils;

    public class RoutingContext : IRoutingContext
    {
        public RoutingContext(IRequestHandler requestHandler, IList<string> parameters)
        {
            Validator.CheckIfNull(requestHandler);
            Validator.CheckIfNull(parameters);

            this.RequestHandler = requestHandler;
            this.Parameters = parameters;
        }

        public IEnumerable<string> Parameters { get; private set; }

        public IRequestHandler RequestHandler { get; private set; }
    }
}
