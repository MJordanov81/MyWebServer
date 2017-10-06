namespace WebServerApplication.Server.HTTP.Response
{
    public class RedirectResponse : HttpResponse
    {
        public string Data { get; private set; }

        public RedirectResponse(string redirectUrl, string data) : base(redirectUrl)
        {
            this.Data = data;
        }
    }
}
