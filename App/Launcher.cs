namespace App
{
    using Config;
    using Config.Contracts;
    using Context;
    using Microsoft.EntityFrameworkCore;
    using MyWebServer.Server;
    using MyWebServer.Server.Routing;
    using MyWebServer.Server.Routing.Contracts;

    public class Launcher
    {
        private WebServer webServer;

        public static void Main()
        {
            new Launcher().Run();
        }

        public void Run()
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.Database.Migrate();
            }

            IApplicationConfiguration app = new AppConfig();
            IAppRouteConfig appRouteConfig = new AppRouteConfig();
            app.Start(appRouteConfig);

            this.webServer = new WebServer(8230, appRouteConfig);
            this.webServer.Run();
        }
    }
}