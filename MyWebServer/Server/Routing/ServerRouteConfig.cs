namespace MyWebServer.Server.Routing
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Contracts;
    using Enums;
    using Handlers.Contracts;
    using Utils;

    public class ServerRouteConfig : IServerRouteConfig
    {
        public ServerRouteConfig(IAppRouteConfig appRouteConfig)
        {
            Validator.CheckIfNull(appRouteConfig);

            this.Routes = new ConcurrentDictionary<RequestMethod, IDictionary<string, IRoutingContext>>();

            foreach (RequestMethod method in Enum.GetValues(typeof(RequestMethod)).Cast<RequestMethod>())
            {
                this.Routes[method] = new Dictionary<string, IRoutingContext>();
            }

            this.IzitializeServerConfig(appRouteConfig);
        }

        public IDictionary<RequestMethod, IDictionary<string, IRoutingContext>> Routes { get; private set; }

        private void IzitializeServerConfig(IAppRouteConfig appRouteConfig)
        {
            Validator.CheckIfNull(appRouteConfig);

            foreach (KeyValuePair<RequestMethod, IDictionary<string, IRequestHandler>> kvp in appRouteConfig.Routes)
            {
                foreach (KeyValuePair<string, IRequestHandler> requestHandler in kvp.Value)
                {
                    IList<string> parameters = new List<string>();

                    string parsedRegex = this.ParseRoute(requestHandler.Key, parameters);

                    IRoutingContext routingContext = new RoutingContext(requestHandler.Value, parameters);

                    this.Routes[kvp.Key].Add(parsedRegex, routingContext);
                }
            }
        }

        private string ParseRoute(string route, IList<string> parameters)
        {
            Validator.CheckIfNullOrEmpty(route);
            Validator.CheckIfNull(parameters);

            if (route == "/")
            {
                return "^/$";
            }

            StringBuilder parsedRegex = new StringBuilder();

            parsedRegex.Append("^/");

            string[] tokens = route.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);

            this.ParseTokens(parameters, tokens, parsedRegex);

            return parsedRegex.ToString();


        }

        private void ParseTokens(IList<string> parameters, string[] tokens, StringBuilder parsedRegex)
        {
            Validator.CheckIfNull(parameters);
            Validator.CheckIfNull(tokens);
            Validator.CheckIfNull(parsedRegex);

            for (int i = 0; i < tokens.Length; i++)
            {
                string end = i == tokens.Length - 1 ? "$" : "/";

                if (!tokens[i].StartsWith("{") && !tokens[i].EndsWith("}"))
                {
                    parsedRegex.Append($"{tokens[i]}{end}");
                }

                string paramPattern = @"<\w+>";

                Regex regax = new Regex(paramPattern);

                Match match = regax.Match(tokens[i]);

                if (!match.Success)
                {
                    continue;
                }

                string paramName = match.Groups[0].Value.Substring(1, match.Groups[0].Length - 2);
                parameters.Add(paramName);
                parsedRegex.Append($"{tokens[i].Substring(1, tokens[i].Length - 2)}{end}");
            }
        }
    }
}
