namespace ByTheCakeApplication.Config.Contracts
{
    using WebServerApplication.Server.Routing.Contracts;

    public interface IApplicationConfiguration
    {
        void Start(IAppRouteConfig appRouteConfig);
    }
}
