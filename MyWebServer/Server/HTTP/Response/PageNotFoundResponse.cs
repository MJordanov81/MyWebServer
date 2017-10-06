namespace MyWebServer.Server.HTTP.Response
{
    using Enums;
    using Server.Contracts;

    public class PageNotFoundResponse : HttpResponse
    {
        public PageNotFoundResponse(ResponseStatusCode statusCode, IView view) : base(statusCode, view)
        {
        }
    }
}
