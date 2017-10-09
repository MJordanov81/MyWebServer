namespace MyWebServer.Server.HTTP.Response
{
    using Contracts;
    using Enums;
    using StaticData;
    using System.Text;
    using Utils;
    using Views.Contracts;

    public abstract class HttpResponse : IHttpResponse
    {
        private readonly IView view;

        protected HttpResponse(ResponseStatusCode statusCode)
        {
            Validator.CheckIfNull(statusCode, nameof(statusCode));

            this.HeaderCollection = new HttpHeaderCollection();
            this.CookieCollection = new HttpCookieCollection();
            this.StatusCode = statusCode;
        }

        protected HttpResponse(string redirectUrl)
        {
            Validator.CheckIfNullOrEmpty(redirectUrl, nameof(redirectUrl));

            this.HeaderCollection = new HttpHeaderCollection();
            this.CookieCollection = new HttpCookieCollection();
            this.StatusCode = ResponseStatusCode.Found;
            this.AddHeader(HeaderType.Types[HeaderTypeCode.Location], redirectUrl);
        }

        protected HttpResponse(ResponseStatusCode statusCode, IView view)
        {
            Validator.CheckIfNull(statusCode, nameof(statusCode));
            Validator.CheckIfNull(view, nameof(view));

            this.HeaderCollection = new HttpHeaderCollection();
            this.CookieCollection = new HttpCookieCollection();
            this.StatusCode = statusCode;
            this.view = view;
        }

        protected IHttpHeaderCollection HeaderCollection { get; }

        protected IHttpCookieCollection CookieCollection { get; }

        protected ResponseStatusCode StatusCode { get; }

        protected string StatusMessage => this.StatusCode.ToString();

        public void AddHeader(string key, string value)
        {
            Validator.CheckIfNullOrEmpty(key, nameof(key));
            Validator.CheckIfNullOrEmpty(value, nameof(value));

            this.HeaderCollection.AddHeader(new HttpHeader(key, value));
        }

        public void AddCookie(string key, string value)
        {
            Validator.CheckIfNullOrEmpty(key, nameof(key));
            Validator.CheckIfNullOrEmpty(value, nameof(value));

            this.CookieCollection.AddCookie(new HttpCookie(key, value));
        }

        public string Response()
        {
            StringBuilder response = new StringBuilder();

            response.AppendLine($"{Constants.HttpVersion} {(int)this.StatusCode} {this.StatusMessage}");
            response.Append(this.HeaderCollection);
            response.AppendLine(this.SetCookies(this.CookieCollection));
            response.AppendLine();

            int responseStatusCode = (int) this.StatusCode;

            if (responseStatusCode < 300 || responseStatusCode > 400)
            {
                response.AppendLine(this.view.View());
            }

            return response.ToString().TrimEnd();
        }

        private string SetCookies(IHttpCookieCollection cookieCollection)
        {
            StringBuilder result = new StringBuilder();

            foreach (HttpCookie cookie in cookieCollection)
            {
                result.AppendLine($"{HeaderType.Types[HeaderTypeCode.SetCookie]}: {cookie.Key}={cookie.Value}");
            }

            return result.ToString();
        }
    }
}
