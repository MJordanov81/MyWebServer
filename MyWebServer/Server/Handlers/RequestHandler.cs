namespace MyWebServer.Server.Handlers
{
    using System;
    using Contracts;
    using HTTP.Contracts;
    using Utils;

    public class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpContext, IHttpResponse> func;

        public RequestHandler(Func<IHttpContext, IHttpResponse> func)
        {
            Validator.CheckIfNull(func);

            this.func = func;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            IHttpResponse httpResponse = this.func(httpContext);
            httpResponse.AddHeader("Content-type", "text/html");

            return httpResponse;
        }
    }
}
