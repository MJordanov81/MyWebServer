namespace MyWebServer.Server.HTTP.Contracts
{
    using System.Collections.Generic;

    public interface IHttpHeaderCollection : IEnumerable<HttpHeader>
    {
        IDictionary<string, HttpHeader> Headers { get; }

        void AddHeader(HttpHeader header);

        bool ContainsKey(string key);

        HttpHeader GetHeader(string key);
    }
}
