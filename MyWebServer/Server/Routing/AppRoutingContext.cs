namespace MyWebServer.Server.Routing
{
    using Contracts;
    using Handlers.Contracts;

    public class AppRoutingContext : IAppRoutingContext
    {
        public AppRoutingContext(IRequestHandler requestHandler, bool userAuthenticationRequired)
        {
            this.RequestHandler = requestHandler;
            this.UserAuthenticationRequired = userAuthenticationRequired;

        }

        public IRequestHandler RequestHandler { get; }

        public bool UserAuthenticationRequired { get; }
    }
}
