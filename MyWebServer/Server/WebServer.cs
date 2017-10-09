namespace MyWebServer.Server
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;
    using Routing;
    using Routing.Contracts;
    using StaticData;

    public class WebServer
    {
        private readonly int port;

        private readonly IServerRouteConfig serverRouteConfig;

        private readonly TcpListener tcpListener;

        private bool isRunning;

        public WebServer(int port, IAppRouteConfig appRouteConfig)
        {
            this.port = port;
            this.tcpListener = new TcpListener(IPAddress.Parse(Constants.Ip), this.port);
            this.serverRouteConfig = new ServerRouteConfig(appRouteConfig);
        }

        public void Run()
        {
            this.tcpListener.Start();
            this.isRunning = true;

            Console.WriteLine(String.Format(Constants.ServerMessage, Constants.Ip, this.port));

            try
            {
                Task task = Task.Run(this.ListenLoop);
                task.Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private async Task ListenLoop()
        {
            while (this.isRunning)
            {
                Socket client = await this.tcpListener.AcceptSocketAsync();
                ConnectionHandler handler = new ConnectionHandler(client, this.serverRouteConfig);

                try
                {
                    Task connection = handler.ProcessRequestAsync();
                    connection.Wait();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
    }
}
