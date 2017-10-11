namespace App.Context.Models
{
    using System;
    using System.Collections.Generic;

    public class Order
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public IList<ProductOrder> Products { get; set; } = new List<ProductOrder>();

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
