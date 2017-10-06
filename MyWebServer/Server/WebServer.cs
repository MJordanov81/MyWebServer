namespace MyWebServer.Server
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;
    using Routing;
    using Routing.Contracts;

    public class WebServer
    {
        private readonly int port;

        private readonly IServerRouteConfig serverRouteConfig;

        private readonly TcpListener tcpListener;

        private bool isRunning;

        public WebServer(int port, IAppRouteConfig appRouteConfig)
        {
            this.port = port;
            this.tcpListener = new TcpListener(IPAddress.Parse("127.0.01"), this.port);
            this.serverRouteConfig = new ServerRouteConfig(appRouteConfig);
        }

        public void Run()
        {
            this.tcpListener.Start();
            this.isRunning = true;

            Console.WriteLine($"Server is now running and listening to TCP clients at IP 127.0.01 : {this.port}...");

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
