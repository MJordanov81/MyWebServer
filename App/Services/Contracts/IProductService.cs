namespace App.Services.Contracts
{
    using System.Collections.Generic;
    using Views.ViewModels.Product;

    public interface IProductService
    {
        bool Add(string name, decimal price, string imageUrl, out int productId);

        bool ExistsProduct(int id);

        ProductDetailsViewModel GetDetails(int id);

        IList<SearchProductsViewModel> GetProducts(string[] query);

        ProductInCartViewModel GetOrderedProductDetails(int id);
    }
}
