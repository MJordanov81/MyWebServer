namespace App.Views.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;

    public class OrderDetailsViewModel
    {
        public int Id { get; set; }

        public IList<Tuple<string, int, decimal>> Products { get; set; } = new List<Tuple<string, int, decimal>>();
    }
}
