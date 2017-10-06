namespace MyWebServer.Server.HTTP.Contracts
{
    using System.Collections.Generic;
    using Enums;

    public interface IHttpRequest
    {
        IDictionary<string, string> FormData { get; }

        HttpHeaderCollection HeaderCollection { get; }

        string Url { get; }

        IDictionary<string, string> UrlParameters { get; }

        string Path { get; }

        IDictionary<string, string> QueryParameters { get; }

        RequestMethod RequestMethod { get; }

        void AddUrlParameter(string key, string value);
    }
}
