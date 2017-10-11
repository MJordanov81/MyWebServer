namespace MyWebServer.Server.HTTP
{
    using Contracts;
    using Enums;
    using Exceptions;
    using StaticData;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using Utils;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            Validator.CheckIfNullOrEmpty(requestString, nameof(requestString));

            this.FormData = new Dictionary<string, string>();
            this.UrlParameters = new Dictionary<string, string>();
            this.QueryParameters = new Dictionary<string, string>();
            this.HeaderCollection = new HttpHeaderCollection();
            this.CookieCollection = new HttpCookieCollection();

            this.ParseRequest(requestString);
        }

        public IDictionary<string, string> FormData { get; private set; }

        public IHttpHeaderCollection HeaderCollection { get; private set; }

        public IHttpCookieCollection CookieCollection { get; private set; }

        public IHttpSession Session { get; private set; }

        public string Url { get; private set; }

        public IDictionary<string, string> UrlParameters { get; private set; }

        public string Path { get; private set; }

        public IDictionary<string, string> QueryParameters { get; private set; }

        public RequestMethod RequestMethod { get; private set; }

        public void AddUrlParameter(string key, string value)
        {
            Validator.CheckIfNullOrEmpty(key, nameof(key));
            Validator.CheckIfNullOrEmpty(value, nameof(value));

            if (!this.UrlParameters.ContainsKey(key))
            {
                this.UrlParameters[key] = null;
            }

            this.UrlParameters[key] = value;
        }

        private void ParseRequest(string requestString)
        {
            Validator.CheckIfNullOrEmpty(requestString, nameof(requestString));

            string[] requestTokens = requestString.Split(new string[] { $"{Environment.NewLine}" }, StringSplitOptions.None);

            string[] requestLine = requestTokens[0].Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (requestLine.Length != 3 || requestLine[2].ToLower() != Constants.HttpVersion.ToLower())
            {
                throw new BadRequestException(ExceptionConstants.InvalidRequestMessage);
            }

            this.RequestMethod = this.ParseRequestMethod(requestLine[0]);
            this.Url = requestLine[1];
            this.Path = this.Url.Split(new[] { '?', '#' }, StringSplitOptions.RemoveEmptyEntries)[0];

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

            this.ParseCookies();

            this.ParseParameters(this.Url);

            if (this.RequestMethod == RequestMethod.Post)
            {
                this.ParseQuery(requestTokens[requestTokens.Length - 1], this.FormData);
            }

            this.SetSession();
        }

        private void SetSession()
        {
            if (this.CookieCollection.ContainsKey(StaticData.Constants.SessionIdCookieKey))
            {
                HttpCookie sessionCookie = this.CookieCollection.GetCookie(StaticData.Constants.SessionIdCookieKey);

                string sessionId = sessionCookie.Value;

                this.Session = SessionStore.Get(sessionId);
            }
        }

        private void ParseParameters(string url)
        {
            Validator.CheckIfNullOrEmpty(url, nameof(url));

            if (!url.Contains('?'))
            {
                return;
            }

            string query = url.Split(new[] { '?' }, StringSplitOptions.RemoveEmptyEntries)[1];

            this.ParseQuery(query, this.QueryParameters);
        }

        private void ParseQuery(string query, IDictionary<string, string> dictionary)
        {
            Validator.CheckIfNullOrEmpty(query, nameof(query));

            if (!query.Contains('='))
            {
                return;
            }

            query = query.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries)[0];

            string[] queryParameters = query.Split(new[] { '&' });

            foreach (string parameter in queryParameters)
            {
                string[] parameterTokens = parameter.Split(new[] { '=' });

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
            Validator.CheckIfNull(headers, nameof(headers));

            string headerPattern = Constants.HeaderRegexPattern;

            foreach (string header in headers)
            {
                Regex regex = new Regex(headerPattern);

                Match headerMatch = regex.Match(header);

                if (headerMatch.Success)
                {
                    string key = headerMatch.Groups[1].ToString();
                    string value = headerMatch.Groups[2].ToString();

                    this.HeaderCollection.AddHeader(new HttpHeader(key, value));
                }
            }

            if (!this.HeaderCollection.ContainsKey(HeaderType.Types[HeaderTypeCode.Host]))
            {
                throw new BadRequestException(ExceptionConstants.MissingHostHeaderMessage);
            }
        }

        private void ParseCookies()
        {
            if (!this.HeaderCollection.ContainsKey(HeaderType.Types[HeaderTypeCode.Cookie]))
            {
                return;
            }

            IList<HttpHeader> allCookieHeaders = this.HeaderCollection.GetHeader(HeaderType.Types[HeaderTypeCode.Cookie]);

            foreach (HttpHeader header in allCookieHeaders)
            {
                string[] cookies = header.Value.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();

                foreach (string cookieString in cookies)
                {
                    string[] cookieTokens = cookieString.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                    if (cookieTokens.Length == 2)
                    {
                        string key = cookieTokens[0];
                        string value = cookieTokens[1];

                        HttpCookie cookie = new HttpCookie(key, value, false);

                        if (!this.CookieCollection.ContainsKey(key))
                        {
                            this.CookieCollection.AddCookie(cookie);
                        }                        
                    }
                }
            }
        }

        private RequestMethod ParseRequestMethod(string request)
        {
            Validator.CheckIfNullOrEmpty(request, nameof(request));

            try
            {
                return (RequestMethod)Enum.Parse(typeof(RequestMethod), request, true);
            }
            catch (Exception)
            {
                throw new BadRequestException(ExceptionConstants.InvalidMethod);
            }
        }
    }
}
