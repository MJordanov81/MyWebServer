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
    using Views.ViewModels.Orders;

    public class OrderController : Controller
    {
        private readonly IOrderService orders;

        public OrderController()
        {
            this.orders = new OrderService();
        }

        public IHttpResponse Create(IHttpContext httpContext)
        {
            Cart cart = (Cart) httpContext.Request.Session.GetParameter(SessionParamsConstants.Cart);
            string username = (string) httpContext.Request.Session.GetParameter(SessionParamsConstants.CurrentUser);

            int orderId = this.orders.Add(username, cart);

            cart.Finish();

            return new RedirectResponse($"/order/{orderId}");

        }

        public IHttpResponse List(IHttpContext httpContext)
        {
            string username = httpContext.Request.Session.GetParameter(SessionParamsConstants.CurrentUser).ToString();

            IList<ShortOrderDetailsViewModel> orderDetails = this.orders.ShortDetailsList(username);

            List<string> headers = new List<string> { "OrderID", "Created On", "Sum" };

            string[][] data = new string[orderDetails.Count][];

            for (int i = 0; i < orderDetails.Count; i++)
            {
                data[i] = new[] { $"<a href=\"\\order\\{orderDetails[i].Id}\">{orderDetails[i].Id}</a>", $"{orderDetails[i].CreationDate}", $"{orderDetails[i].TotalAmount}" };
            }

            HtmlTableDataModel dataModel = new HtmlTableDataModel(headers, data);

            string table = HtmlHelper.HtmlTable(dataModel);

            this.ViewData[ViewDataConstants.Orders] = table;

            return this.HtmlViewResponse("order/list");

        }

        public IHttpResponse Details(IHttpContext httpContext)
        {
            int orderId = NumberParser.ParseInt(httpContext.Request.UrlParameters["id"]);

            OrderDetailsViewModel order = this.orders.Details(orderId);

            List<string> headers = new List<string> { "Product", "Quantity", "Amount" };

            string[][] data = new string[order.Products.Count][];

            decimal totalAmount = 0;

            for (int i = 0; i < order.Products.Count; i++)
            {
                string productName = order.Products[i].Item1;
                int quantity = order.Products[i].Item2;
                decimal price = order.Products[i].Item3;
                decimal amount = quantity * price;
                totalAmount += amount;


                data[i] = new[] { $"{productName}", $"{quantity}", $"{amount:f2}" };
            }

            HtmlTableDataModel dataModel = new HtmlTableDataModel(headers, data);

            string table = HtmlHelper.HtmlTable(dataModel);

            this.ViewData[ViewDataConstants.Id] = order.Id.ToString();
            this.ViewData[ViewDataConstants.CreationDate] = DateTime.Now.ToString("dd-MM-yyyy");
            this.ViewData[ViewDataConstants.TotalAmount] = $"{totalAmount:f2}";
            this.ViewData[ViewDataConstants.Products] = table;

            return this.HtmlViewResponse("order/details");
        }
    }
}
