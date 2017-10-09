namespace MyWebServer.Server.HTTP
{
    using System;
    using Contracts;

    public class HttpCookie : IHttpCookie
    {
        public HttpCookie(string key, string value, int expiresInDays = 3)
        {
            this.Key = key;
            this.Value = value;
            this.Expires = DateTime.Now.AddDays(expiresInDays);
        }

        public HttpCookie(string key, string value, bool isNew, int expiresInDays = 3)
            : this(key, value, expiresInDays)
        {
            this.IsNew = isNew;
        }

        public string Key { get; private set; }

        public string Value { get; private set; }

        public DateTime Expires { get; private set; }

        public bool IsNew { get; private set; } = true;

        public override string ToString()
        {
            return $"{this.Key}: {this.Value}; Expires: {this.Expires.Date}";
        }
    }
}
