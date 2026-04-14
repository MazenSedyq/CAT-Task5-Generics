using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ShoppingSystem
{
    public class Product
    {
        Random rand = new();
        public int ID { get;}
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(string name,decimal price)
        {
            ID = (int)(rand.NextDouble()*1000);
            this.Name = name;
            this.Price = price;
        }

        public void Print()
        {
            Console.WriteLine($"┌{new string('─',30)}┐");
            Console.WriteLine($"│ Name:  {this.Name,-20}  │");
            Console.WriteLine($"│ Price: {this.Price,-20 :c}  │");
            Console.WriteLine($"└{new string('─',30)}┘");
        }
    }
}
