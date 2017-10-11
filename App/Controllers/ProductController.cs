namespace App.Controllers
{
    using HtmlUtility.DataModels;
    using HtmlUtility.HtmlHelpers;
    using Models;
    using MyWebServer.Server.HTTP.Contracts;
    using MyWebServer.Server.HTTP.Response;
    using MyWebServer.Server.Utils;
    using Services;
    using Services.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Views.ViewModels.Product;
    using Constants;

    public class ProductController : Controller
    {
        private readonly IProductService products;

        public ProductController()
        {
            this.products = new ProductService();
        }

        public IHttpResponse Add()
        {
            return this.HtmlViewResponse("Product/Add");
        }

        public IHttpResponse AddPost(ProductAddViewModel model)
        {
            int createdProductId;

            bool isSuccessfullyAdded = this.products.Add(model.Name, model.Price, model.ImageUrl, out createdProductId);

            if (!isSuccessfullyAdded)
            {
                this.ShowError(ErrorConstants.DuplicatedProductName);
                return this.HtmlViewResponse("/product/add");
            }

            return new RedirectResponse($"/product/{createdProductId}");
        }

        public IHttpResponse Search(IHttpContext httpContext)
        {
            this.SearchProducts(httpContext);

            this.SetCartDisplay(httpContext);

            return this.HtmlViewResponse("Product/Search");
        }

        public IHttpResponse SearchPost(IHttpContext httpContext)
        {
            this.SearchProducts(httpContext);

            this.SetCartDisplay(httpContext);

            return this.HtmlViewResponse("/Product/Search");
        }

        public IHttpResponse Details(IHttpContext httpContext)
        {
            int id = NumberParser.ParseInt(httpContext.Request.UrlParameters["id"]);

            ProductDetailsViewModel product = this.products.GetDetails(id);

            this.ViewData[ViewDataConstants.Name] = product.Name;
            this.ViewData[ViewDataConstants.Price] = product.Price;
            this.ViewData[ViewDataConstants.ImageUrl] = product.ImageUrl;

            return this.HtmlViewResponse("/product/details");

        }

        private void SearchProducts(IHttpContext httpContext)
        {
            string[] query = null;

            if (httpContext.Request.FormData.ContainsKey("query"))
            {
                query = httpContext.Request.FormData["query"].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
            
            List<string> headers = new List<string> { "Product", "Price" };

            IList<SearchProductsViewModel> allProducts = this.products.GetProducts(query);

            string[][] data = new string[allProducts.Count][];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = new[] { $"<a href=\"{allProducts[i].Id}\">{allProducts[i].Name}</a>", $"{allProducts[i].Price:f2}", $"<button><a href=\"/cart/order/{allProducts[i].Id}\">Order</a></button>" };
            }

            HtmlTableDataModel dataModel = new HtmlTableDataModel(headers, data);

            string table = HtmlHelper.HtmlTable(dataModel);

            this.ViewData[ViewDataConstants.SearchResult] = table;
        }

        private void SetCartDisplay(IHttpContext httpContext)
        {
            Cart cart = (Cart) httpContext.Request.Session.GetParameter(SessionParamsConstants.Cart);

            if (cart.OrderedItems.Values.Sum() > 0)
            {
                this.ViewData[ViewDataConstants.ShowCart] = "block";
                this.ViewData[ViewDataConstants.ProductsCount] = cart.OrderedItems.Values.Sum().ToString();
            }
            else
            {
                this.ViewData[ViewDataConstants.ShowCart] = "none";
            }
        }
    }
}
