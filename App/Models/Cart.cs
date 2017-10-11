namespace App.Models
{
    using System.Collections.Generic;

    public class Cart
    {
        private IDictionary<int, int> orderedItems;

        public Cart()
        {
            this.orderedItems = new Dictionary<int, int>();
        }

        public IDictionary<int, int> OrderedItems => this.orderedItems;

        public void Add(int productId)
        {
            if (!this.orderedItems.ContainsKey(productId))
            {
                this.orderedItems[productId] = 0;
            }

            this.orderedItems[productId] += 1;
        }

        public void Remove(int productId)
        {
            if (this.orderedItems.ContainsKey(productId) && this.orderedItems[productId] > 0)
            {
                this.orderedItems[productId] -= 1;
            }
        }

        public void Finish()
        {
            this.orderedItems.Clear();
        }
    }
}
