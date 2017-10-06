namespace ByTheCakeApplication.Controllers
{
    using Context;
    using HtmlUtilities.DataModels;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Views.Home;
    using WebServerApplication.Server.Enums;
    using WebServerApplication.Server.HTTP.Contracts;
    using WebServerApplication.Server.HTTP.Response;
    using WebServerApplication.Server.Utils;

    public class HomeController
    {
        public IHttpResponse Index()
        {
            return new ViewResponse(ResponseStatusCode.Ok, new IndexView());
        }

        public IHttpResponse AddGet()
        {
            return new ViewResponse(ResponseStatusCode.Ok, new AddView());
        }

        public IHttpResponse AddPost(IHttpContext httpContext)
        {
            try
            {
                Cake cake =
                    new Cake(httpContext.Request.FormData["name"],
                        NumberParser.ParseDecimal(httpContext.Request.FormData["price"]));

                CsvContext context = new CsvContext();
                context.Add(cake);

                return new RedirectResponse("/", null);
            }
            catch (Exception)
            {
                return new RedirectResponse("/add", null);
            }

        }

        public IHttpResponse SearchGet()
        {
            return new ViewResponse(ResponseStatusCode.Ok, new SearchView());
        }

        public IHttpResponse SearchPost(IHttpContext httpContext)
        {
            string []query = httpContext.Request.FormData["query"].Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);

            List<string> headers = new List<string>{"Cake", "Price"};

            CsvContext context = new CsvContext();

            List<Cake> cakes = context.GetAll().Where(c => this.PositiveSearch(query, c.Name)).ToList();

            string [][] data = new string[cakes.Count][];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = new[] {$"{cakes[i].Name}", $"{cakes[i].Price:f2}"};
            }

            HtmlTableDataModel dataModel = new HtmlTableDataModel(headers, data);

            return new ViewResponse(ResponseStatusCode.Ok, new SearchView(dataModel));
        }

        public IHttpResponse AboutUs()
        {
            return new ViewResponse(ResponseStatusCode.Ok, new AboutUsView());
        }

        private bool PositiveSearch(string [] query, string cakeName)
        {
            if (query.Length == 1 && query[0] == "*")
            {
                return true;
            }

            bool result = true;

            foreach (string queryPart in query)
            {
                if (!cakeName.ToLower().Contains(queryPart.ToLower()))
                {
                    result = false;
                }
            }

            return result;
        }
    }
}
