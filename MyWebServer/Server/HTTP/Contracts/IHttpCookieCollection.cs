namespace MyWebServer.Server.HTTP.Contracts
{
    using System.Collections.Generic;

    public interface IHttpCookieCollection : IEnumerable<HttpCookie>
    {
        IDictionary<string, HttpCookie> Cookies { get; }

        void AddCookie(HttpCookie cookie);

        bool ContainsKey(string key);

        HttpCookie GetCookie(string key);
    }
}
