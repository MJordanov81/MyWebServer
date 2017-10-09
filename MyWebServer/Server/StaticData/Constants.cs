namespace MyWebServer.Server.StaticData
{
    public static class Constants
    {
        public const string Ip = "127.0.0.1";

        public const string ServerMessage = "Server is now running and listening to TCP clients at IP {0} : {1}";

        public const string SessionIdCookieKey = "SSID";

        public const string HttpVersion = "HTTP/1.1";

        public const string HeaderRegexPattern = @"(\S+): (.+)";

        public const string ParamNameRegexPattern = @"<\w+>";

        public const string PageNotFoundMessage = "Error 404! Page not found!";
    }
}
