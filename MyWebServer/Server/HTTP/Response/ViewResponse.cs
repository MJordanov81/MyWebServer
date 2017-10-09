namespace MyWebServer.Server.HTTP.Response
{
    using Enums;
    using Views.Contracts;

    public class ViewResponse : HttpResponse
    {
        public ViewResponse(ResponseStatusCode statusCode, IView view) : base(statusCode, view)
        {

        }
    }
}
