namespace MyWebServer.Server.HTTP
{
    using System;
    using Contracts;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Utils;

    public class HttpCookieCollection : IHttpCookieCollection
    {
        public IDictionary<string, HttpCookie> Cookies { get; }

        public HttpCookieCollection()
        {
            this.Cookies = new Dictionary<string, HttpCookie>();
        }

        public void AddCookie(HttpCookie cookie)
        {
            Validator.CheckIfNull(cookie, nameof(cookie));

            this.Cookies.Add(cookie.Key, cookie);
        }

        public bool ContainsKey(string key)
        {
            Validator.CheckIfNullOrEmpty(key, nameof(key));

            return this.Cookies.ContainsKey(key);
        }

        public HttpCookie GetCookie(string key)
        {
            Validator.CheckIfNullOrEmpty(key, nameof(key));

            if (!this.Cookies.ContainsKey(key))
            {
                throw new InvalidOperationException($"There is no cookie with key '{key}' in the CookieCollection!");
            }

            return this.Cookies[key];
        }

        public IEnumerator<HttpCookie> GetEnumerator()
        {
            for (int i = 0; i < this.Cookies.Count; i++)
            {
                yield return this.Cookies.Values.ToList()[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
