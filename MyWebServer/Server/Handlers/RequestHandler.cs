namespace MyWebServer.Server.Handlers
{
    using Contracts;
    using Enums;
    using HTTP.Contracts;
    using StaticData;
    using System;
    using Utils;

    public class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpContext, IHttpResponse> func;

        public RequestHandler(Func<IHttpContext, IHttpResponse> func)
        {
            Validator.CheckIfNull(func, nameof(func));

            this.func = func;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            IHttpResponse httpResponse = this.func(httpContext);
            httpResponse.AddHeader(HeaderType.Types[HeaderTypeCode.ContentType], MimeType.Types[MimeTypeCode.TextHtml]);

            // Adds session if client does not have one (checks request and adds to reponse)
            if (!httpContext.Request.CookieCollection.ContainsKey(Constants.SessionIdCookieKey))
            {
                string sessionId = SessionStore.GetNewSessionId();

                httpResponse.AddCookie(Constants.SessionIdCookieKey, sessionId);
            }

            return httpResponse;
        }
    }
}
