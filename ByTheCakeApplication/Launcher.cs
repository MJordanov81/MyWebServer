namespace ByTheCakeApplication
{
    using Config;
    using Config.Contracts;
    using Context;
    using WebServerApplication.Server;
    using WebServerApplication.Server.Routing;
    using WebServerApplication.Server.Routing.Contracts;


    public class Launcher
    {
        private WebServer webServer;

        public static void Main()
        {
            new Launcher().Run();
        }

        public void Run()
        {
            CsvContext context = new CsvContext();
            context.InitializeDb(false);

            IApplicationConfiguration app = new AppConfig();
            IAppRouteConfig appRouteConfig = new AppRouteConfig();
            app.Start(appRouteConfig);

            this.webServer = new WebServer(8230, appRouteConfig);
            this.webServer.Run();
        }
    }

}