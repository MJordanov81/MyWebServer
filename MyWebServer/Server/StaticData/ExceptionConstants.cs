namespace MyWebServer.Server.StaticData
{
    public static class ExceptionConstants
    {
        public const string InvalidRequestMessage = "Invalid request line!";

        public const string MissingHostHeaderMessage = "Missing 'Host' header!";

        public const string InvalidMethod = "Invalid or not supported method!";

        public const string MissingParameter = "There is no parameter with key '{0}' in the session!";

        public const string InvalidString = "Invalid string!";

        public const string ObjectCannotBeNullOrEmpty = "Object {0} cannot be null or empty!";
    }
}
