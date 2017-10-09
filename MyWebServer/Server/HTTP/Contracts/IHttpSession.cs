namespace MyWebServer.Server.HTTP.Contracts
{
    public interface IHttpSession
    {
        string Id { get; }

        object GetParameter(string key);

        void Add(string key, object value);

        void Clear();

        bool IsAuthenticated();
    }
}
