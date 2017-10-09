namespace MyWebServer.Server.HTTP
{
    using Contracts;
    using Utils;

    public class HttpContext : IHttpContext
    {
        private readonly IHttpRequest request;

        public HttpContext(string requestString)
        {
            Validator.CheckIfNullOrEmpty(requestString, nameof(requestString));

            this.request = new HttpRequest(requestString);
        }

        public IHttpRequest Request => this.request;
    }
}
