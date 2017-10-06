namespace MyWebServer.Server.Exceptions
{
    using System;

    public class BadRequestException : ArgumentException
    {
        public BadRequestException(string message) : base(message)
        {
            
        }
    }
}
