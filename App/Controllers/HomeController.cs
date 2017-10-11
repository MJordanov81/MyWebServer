namespace App.Controllers
{
    using MyWebServer.Server.HTTP.Contracts;

    public class HomeController : Controller
    {
        public IHttpResponse Index()
        {
            IHttpResponse response = this.HtmlViewResponse("Home/Index");

            return response;
        }

        public IHttpResponse AboutUs()
        {
            return this.HtmlViewResponse("Home/AboutUs");
        }
    }
}
