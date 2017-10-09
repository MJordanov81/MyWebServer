namespace MyWebServer.Server.StaticData
{
    using System;
    using HTTP;
    using HTTP.Contracts;
    using System.Collections.Concurrent;

    public static class SessionStore
    {
        private static ConcurrentDictionary<string, IHttpSession> sessions = new ConcurrentDictionary<string, IHttpSession>();

        public static IHttpSession Get(string key)
        {
            return sessions.GetOrAdd(key, new HttpSession(key));
        }

        public static string GetNewSessionId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
