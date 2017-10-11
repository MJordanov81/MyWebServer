namespace App.Controllers
{
    using Constants;
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

    public class CartController : Controller
    {
        private readonly IProductService products;

        public CartController()
        {
            this.products = new ProductService();
        }

        public IHttpResponse PutToCart(IHttpContext httpContext)
        {
            int productId = NumberParser.ParseInt(httpContext.Request.UrlParameters["id"]);

            if (!this.products.ExistsProduct(productId))
            {
                throw new InvalidOperationException(ErrorConstants.MisingProductInDb);
            }

            try
            {
                Cart cart = (Cart)httpContext.Request.Session.GetParameter(SessionParamsConstants.Cart);

                cart.Add(productId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new RedirectResponse($"/product/search");
        }

        public IHttpResponse Details(IHttpContext httpContext)
        {
            Cart cart = (Cart)httpContext.Request.Session.GetParameter(SessionParamsConstants.Cart);

            List<string> headers = new List<string> { "Product", "Count", "Amount" };

            IDictionary<int, int> orderedProductsIds = cart.OrderedItems;

            IDictionary<ProductInCartViewModel, int> orderedProducts = new Dictionary<ProductInCartViewModel, int>();

            foreach (KeyValuePair<int, int> productId in orderedProductsIds)
            {
                ProductInCartViewModel product = this.products.GetOrderedProductDetails(productId.Key);

                orderedProducts.Add(product, productId.Value);
            }

            decimal totalAmount = 0;

            string[][] data = new string[orderedProducts.Count][];

            for (int i = 0; i < data.Length; i++)
            {
                string productName = orderedProducts.ToList()[i].Key.Name;
                int productsCount = orderedProducts.ToList()[i].Value;
                decimal price = orderedProducts.ToList()[i].Key.Price;
                decimal amount = price * productsCount;

                totalAmount += amount;

                data[i] = new[] { $"{productName}", $"{productsCount}", $"{amount:f2}" };
            }

            HtmlTableDataModel dataModel = new HtmlTableDataModel(headers, data);

            string table = HtmlHelper.HtmlTable(dataModel);

            this.ViewData[ViewDataConstants.Details] = table;
            this.ViewData[ViewDataConstants.TotalAmount] = $"{totalAmount:f2}";

            return this.HtmlViewResponse("/cart/details");
        }
    }
}
