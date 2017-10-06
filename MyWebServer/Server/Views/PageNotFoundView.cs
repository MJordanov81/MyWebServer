namespace MyWebServer.Server.Views
{
    using Contracts;

    public class PageNotFoundView : IView
    {
        public string View()
        {
            return "Page not found! Error 404.";
        }
    }
}
