namespace MyWebServer.Server.HTTP.Contracts
{
    using System.Collections.Generic;

    public interface IHttpHeaderCollection : IEnumerable<IList<HttpHeader>>
    {
        IDictionary<string, IList<HttpHeader>> Headers { get; }

        void AddHeader(HttpHeader header);

        bool ContainsKey(string key);

        IList<HttpHeader> GetHeader(string key);
    }
}
