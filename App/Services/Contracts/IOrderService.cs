namespace App.Services.Contracts
{
    using System.Collections.Generic;
    using Models;
    using Views.ViewModels.Orders;

    public interface IOrderService
    {
        int Add(string username, Cart cart);

        OrderDetailsViewModel Details(int orderId);

        IList<ShortOrderDetailsViewModel> ShortDetailsList(string username);
    }
}
