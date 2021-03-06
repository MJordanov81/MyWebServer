﻿namespace MyWebServer.Server.Routing
{
    using Contracts;
    using Enums;
    using StaticData;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Utils;

    public class ServerRouteConfig : IServerRouteConfig
    {
        public ServerRouteConfig(IAppRouteConfig appRouteConfig)
        {
            Validator.CheckIfNull(appRouteConfig, nameof(appRouteConfig));

            this.Routes = new ConcurrentDictionary<RequestMethod, IDictionary<string, IRoutingContext>>();

            foreach (RequestMethod method in Enum.GetValues(typeof(RequestMethod)).Cast<RequestMethod>())
            {
                this.Routes[method] = new Dictionary<string, IRoutingContext>();
            }

            this.IzitializeServerConfig(appRouteConfig);
        }

        public IDictionary<RequestMethod, IDictionary<string, IRoutingContext>> Routes { get; private set; }

        public string HomePage { get; private set; }

        private void IzitializeServerConfig(IAppRouteConfig appRouteConfig)
        {
            Validator.CheckIfNull(appRouteConfig, nameof(appRouteConfig));

            this.HomePage = appRouteConfig.HomePage;

            foreach (KeyValuePair<RequestMethod, IDictionary<string, IAppRoutingContext>> kvp in appRouteConfig.Routes)
            {
                foreach (KeyValuePair<string, IAppRoutingContext> appRoutingContext in kvp.Value)
                {
                    IList<string> parameters = new List<string>();

                    string parsedRegex = this.ParseRoute(appRoutingContext.Key, parameters);

                    IRoutingContext routingContext = new RoutingContext(appRoutingContext.Value.RequestHandler, appRoutingContext.Value.UserAuthenticationRequired, parameters);

                    this.Routes[kvp.Key].Add(parsedRegex, routingContext);
                }
            }
        }

        private string ParseRoute(string route, IList<string> parameters)
        {
            Validator.CheckIfNullOrEmpty(route, nameof(route));
            Validator.CheckIfNull(parameters, nameof(parameters));

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
            Validator.CheckIfNull(parameters, nameof(parameters));
            Validator.CheckIfNull(tokens, nameof(tokens));
            Validator.CheckIfNull(parsedRegex, nameof(parsedRegex));

            for (int i = 0; i < tokens.Length; i++)
            {
                string end = i == tokens.Length - 1 ? "$" : "/";

                if (!tokens[i].StartsWith("{") && !tokens[i].EndsWith("}"))
                {
                    parsedRegex.Append($"{tokens[i]}{end}");
                }

                string paramNamePattern = Constants.ParamNameRegexPattern;

                Regex regax = new Regex(paramNamePattern);

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
