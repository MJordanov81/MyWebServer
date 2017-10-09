namespace MyWebServer.Server.HTTP
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Contracts;
    using Utils;

    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        public IDictionary<string, IList<HttpHeader>> Headers { get; private set; }

        public HttpHeaderCollection()
        {
            this.Headers = new Dictionary<string, IList<HttpHeader>>();
        }

        public void AddHeader(HttpHeader header)
        {
            Validator.CheckIfNull(header, nameof(header));

            if (!this.Headers.ContainsKey(header.Key))
            {
                this.Headers.Add(header.Key, new List<HttpHeader>());
            }

            this.Headers[header.Key].Add(header);
        }

        public bool ContainsKey(string key)
        {
            Validator.CheckIfNullOrEmpty(key, nameof(key));

            return this.Headers.ContainsKey(key);
        }

        public IList<HttpHeader> GetHeader(string key)
        {
            Validator.CheckIfNullOrEmpty(key, nameof(key));

            if (!this.Headers.ContainsKey(key))
            {
                throw new InvalidOperationException($"There are no headers with key '{key}' in the HeaderCollection!");
            }

            return this.Headers[key];
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            foreach (KeyValuePair<string, IList<HttpHeader>> headers in this.Headers)
            {
                foreach (HttpHeader header in headers.Value)
                {
                    result.AppendLine(header.ToString());
                }
            }

            return result.ToString();
        }

        public IEnumerator<IList<HttpHeader>> GetEnumerator()
        {
            for (int i = 0; i < this.Headers.Values.Count; i++)
            {
                yield return this.Headers.Values.ToList()[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
