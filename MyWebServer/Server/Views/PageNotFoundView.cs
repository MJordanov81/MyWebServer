namespace MyWebServer.Server.Views
{
    using Contracts;
    using StaticData;

    public class PageNotFoundView : IView
    {
        public string View()
        {
            return Constants.PageNotFoundMessage;
        }
    }
}
