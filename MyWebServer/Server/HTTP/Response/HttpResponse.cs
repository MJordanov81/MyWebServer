namespace MyWebServer.Server.HTTP.Response
{
    using System.Text;
    using Contracts;
    using Enums;
    using Server.Contracts;
    using Utils;

    public abstract class HttpResponse : IHttpResponse
    {
        private readonly IView view;

        protected HttpResponse(ResponseStatusCode statusCode)
        {
            Validator.CheckIfNull(statusCode);

            this.HeaderCollection = new HttpHeaderCollection();
            this.StatusCode = statusCode;
        }

        protected HttpResponse(string redirectUrl)
        {
            Validator.CheckIfNullOrEmpty(redirectUrl);

            this.HeaderCollection = new HttpHeaderCollection();
            this.StatusCode = ResponseStatusCode.Found;
            this.AddHeader("Location", redirectUrl);
        }

        protected HttpResponse(ResponseStatusCode statusCode, IView view)
        {
            Validator.CheckIfNull(statusCode);
            Validator.CheckIfNull(view);

            this.HeaderCollection = new HttpHeaderCollection();
            this.StatusCode = statusCode;
            this.view = view;
        }

        protected HttpHeaderCollection HeaderCollection { get; set; }

        protected ResponseStatusCode StatusCode { get; set; }

        protected string StatusMessage => this.StatusCode.ToString();

        public void AddHeader(string key, string value)
        {
            Validator.CheckIfNullOrEmpty(key);
            Validator.CheckIfNullOrEmpty(value);

            this.HeaderCollection.AddHeader(new HttpHeader(key, value));
        }

        public string Response()
        {
            StringBuilder response = new StringBuilder();

            response.AppendLine($"HTTP/1.1 {(int)this.StatusCode} {this.StatusMessage}");
            foreach (var header in this.HeaderCollection.Headers.Values)
            {
                response.AppendLine($"{header.Key}: {header.Value}");
            }

            response.AppendLine();

            int responseStatusCode = (int) this.StatusCode;

            if (responseStatusCode < 300 || responseStatusCode > 400)
            {
                response.AppendLine(this.view.View());
            }

            return response.ToString().TrimEnd();
        }
    }
}
