namespace App.Controllers
{
    using Constants;
    using MyWebServer.Server.Enums;
    using MyWebServer.Server.HTTP.Contracts;
    using MyWebServer.Server.HTTP.Response;
    using System.Collections.Generic;
    using System.IO;
    using Views;

    public abstract class Controller
    {
        protected const string HtmlFilesPath = @"Resources\Html\{0}.html";

        protected Dictionary<string, string> ViewData { get; set; }

        protected Controller()
        {
            this.ViewData = new Dictionary<string, string>();
            this.ShowError();
        }

        protected IHttpResponse HtmlViewResponse(string fileName)
        {
            string layoutHtmlFile = File.ReadAllText(@"Resources\Html\Layout.html");

            string htmlFile = File.ReadAllText(string.Format(HtmlFilesPath, fileName));

            foreach (KeyValuePair<string, string> value in this.ViewData)
            {
                htmlFile = htmlFile.Replace($"{{{{{{{value.Key}}}}}}}", value.Value);
            }

            return new ViewResponse(ResponseStatusCode.Ok, new FileView(layoutHtmlFile.Replace("{{{content}}}", htmlFile)));
        }

        protected void ShowError(string errorMessage = "")
        {
            string showOrNot = "block";

            if (errorMessage == "")
            {
                showOrNot = "none";
            }

            this.ViewData[ViewDataConstants.ShowError] = showOrNot;
            this.ViewData[ViewDataConstants.Error] = errorMessage;
        }
    }
}
