namespace MyWebServer.Server.HTTP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using Contracts;
    using Enums;
    using Exceptions;
    using Utils;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            Validator.CheckIfNullOrEmpty(requestString);

            this.FormData = new Dictionary<string, string>();
            this.UrlParameters = new Dictionary<string, string>();
            this.QueryParameters = new Dictionary<string, string>();
            this.HeaderCollection = new HttpHeaderCollection();

            this.ParseRequest(requestString);
        }

        public IDictionary<string, string> FormData { get; private set; }

        public HttpHeaderCollection HeaderCollection { get; private set; }

        public string Url { get; private set; }

        public IDictionary<string, string> UrlParameters { get; private set; }

        public string Path { get; private set; }

        public IDictionary<string, string> QueryParameters { get; private set; }

        public RequestMethod RequestMethod { get; private set; }

        public void AddUrlParameter(string key, string value)
        {
            Validator.CheckIfNullOrEmpty(key);
            Validator.CheckIfNullOrEmpty(value);

            if (!this.UrlParameters.ContainsKey(key))
            {
                this.UrlParameters[key] = null;
            }

            this.UrlParameters[key] = value;
        }

        private void ParseRequest(string requestString)
        {
            Validator.CheckIfNullOrEmpty(requestString);

            string[] requestTokens = requestString.Split(new string[]{$"{Environment.NewLine}"}, StringSplitOptions.None);

            string[] requestLine = requestTokens[0].Trim().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            if (requestLine.Length != 3 || requestLine[2].ToLower() != "http/1.1" )
            {
                throw new BadRequestException("Invalid request line!");
            }

            this.RequestMethod = this.ParseRequestMethod(requestLine[0]);
            this.Url = requestLine[1];
            this.Path = this.Url.Split(new[] {'?', '#'}, StringSplitOptions.RemoveEmptyEntries)[0];

            IList<string> headers = new List<string>();

            for (int i = 1; i < requestTokens.Length; i++)
            {
                if (String.IsNullOrEmpty(requestTokens[i]))
                {
                    break;
                }

                headers.Add(requestTokens[i]);
            }

            this.ParseHeaders(headers);

            this.ParseParameters(this.Url);

            if (this.RequestMethod == RequestMethod.Post)
            {
                this.ParseQuery(requestTokens[requestTokens.Length - 1], this.FormData);
            }
        }

        private void ParseParameters(string url)
        {
            Validator.CheckIfNullOrEmpty(url);

            if (!url.Contains('?'))
            {
                return;
            }

            string query = url.Split(new[] {'?'}, StringSplitOptions.RemoveEmptyEntries)[1];

            this.ParseQuery(query, this.QueryParameters);
        }

        private void ParseQuery(string query, IDictionary<string , string> dictionary)
        {
            Validator.CheckIfNullOrEmpty(query);

            if (!query.Contains('='))
            {
                return;
            }

            query = query.Split(new[] {'#'}, StringSplitOptions.RemoveEmptyEntries)[0];

            string[] queryParameters = query.Split(new[] {'&'}, StringSplitOptions.RemoveEmptyEntries);

            foreach (string parameter in queryParameters)
            {
                string[] parameterTokens = parameter.Split(new[] {'='}, StringSplitOptions.RemoveEmptyEntries);

                if (parameterTokens.Length != 2)
                {
                    continue;
                }

                string key = WebUtility.UrlDecode(parameterTokens[0]);
                string value = WebUtility.UrlDecode(parameterTokens[1]);

                dictionary.Add(key, value);
            }
        }

        private void ParseHeaders(IList<string> headers)
        {
            Validator.CheckIfNull(headers);

            string headerPattern = @"(\S+): (.+)";

            foreach (string header in headers)
            {
               Regex regex = new Regex(headerPattern);

                Match headerMatch = regex.Match(header);

                if (headerMatch.Success)
                {
                    string key = headerMatch.Groups[1].ToString();
                    string value = headerMatch.Groups[2].ToString();

                    if (!this.HeaderCollection.ContainsKey(key))
                    {
                        this.HeaderCollection.AddHeader(new HttpHeader(key, value));
                    }
                }
            }

            if (!this.HeaderCollection.ContainsKey("Host"))
            {
                throw new BadRequestException("Missing 'Host' header!");
            }
        }

        private RequestMethod ParseRequestMethod(string request)
        {
            Validator.CheckIfNullOrEmpty(request);

            try
            {
                return (RequestMethod) Enum.Parse(typeof(RequestMethod), request, true);
            }
            catch (Exception)
            {
                throw new BadRequestException("Invalid or not supported method!");
            }
        }
    }
}
