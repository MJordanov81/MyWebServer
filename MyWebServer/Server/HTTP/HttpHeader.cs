namespace MyWebServer.Server.HTTP
{
    using System;
    using Utils;

    public class HttpHeader
    {
        public HttpHeader(string key, string value)
        {
            Validator.CheckIfNullOrEmpty(key, nameof(key));
            Validator.CheckIfNullOrEmpty(value, nameof(value));

            this.Key = key;
            this.Value = value;
        }

        public string Key { get; private set; }

        public string Value { get; private set; }

        public override string ToString()
        {
            return $"{this.Key}: {this.Value}";
        }
    }
}
