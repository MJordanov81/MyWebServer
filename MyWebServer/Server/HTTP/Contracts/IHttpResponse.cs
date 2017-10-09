namespace MyWebServer.Server.HTTP.Contracts
{
    public interface IHttpResponse
    {
        void AddHeader(string key, string value);

        void AddCookie(string key, string value);

        string Response();
    }
}
