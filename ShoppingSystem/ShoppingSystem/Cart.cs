using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingSystem
{
    public class Cart
    {
        public List<Product> Products = [];

        public void Add(Product product)
        {
            Products.Add(product);
        }

        public void Remove(Product product)
        {
            Products.Remove(product);
        }

    }
}
