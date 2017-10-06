namespace MyWebServer.Server.HTTP
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Contracts;
    using Utils;

    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        public IDictionary<string, HttpHeader> Headers { get; private set; }

        public HttpHeaderCollection()
        {
            this.Headers = new Dictionary<string, HttpHeader>();
        }

        public void AddHeader(HttpHeader header)
        {
            Validator.CheckIfNull(header);

            if (!this.Headers.ContainsKey(header.Key))
            {
                this.Headers.Add(header.Key, header);
            }
        }

        public bool ContainsKey(string key)
        {
            Validator.CheckIfNullOrEmpty(key);

            return this.Headers.ContainsKey(key);
        }

        public HttpHeader GetHeader(string key)
        {
            Validator.CheckIfNullOrEmpty(key);

            return this.Headers[key];
        }




        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            foreach (var header in this.Headers)
            {
                result.AppendLine(header.ToString());
            }

            return result.ToString().TrimEnd();
        }

        public IEnumerator<HttpHeader> GetEnumerator()
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
