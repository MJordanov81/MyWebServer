namespace MyWebServer.Server.Routing.Contracts
{
    using Handlers.Contracts;

    public interface IAppRoutingContext
    {
        IRequestHandler RequestHandler { get; }

        bool UserAuthenticationRequired { get; }
    }
}
