namespace App.Config.Contracts
{
    using MyWebServer.Server.Routing.Contracts;

    public interface IApplicationConfiguration
    {
        void Start(IAppRouteConfig appRouteConfig);
    }
}
