namespace MyWebServer.Server.HTTP
{
    using System;
    using Contracts;
    using System.Collections.Generic;
    using StaticData;
    using Utils;

    public class HttpSession : IHttpSession
    {
        public HttpSession(string id)
        {
            this.Id = id;
            this.Parameters = new Dictionary<string, object>();
        }

        public string Id { get; private set; }

        public IDictionary<string, object> Parameters { get; private set; }

        public object GetParameter(string key)
        {
            Validator.CheckIfNullOrEmpty(key, nameof(key));

            if (!this.Parameters.ContainsKey(key))
            {
                throw new InvalidOperationException(string.Format(ExceptionConstants.MissingParameter, key));
            }

            return this.Parameters[key];
        }

        public void Add(string key, object value)
        {
            Validator.CheckIfNullOrEmpty(key, nameof(key));
            Validator.CheckIfNull(value, nameof(value));

            this.Parameters[key] = value;
        }

        public void Clear() => this.Parameters.Clear();
        
        public bool IsAuthenticated()
        {
            return this.Parameters.ContainsKey("CurrentUser");
        }
    }
}
