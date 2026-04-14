using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using System.Xml.Linq;

namespace ShoppingSystem
{
    public class Shop
    {
        private Cart cart = new();
        private Logs logs = new();

        public void Screen()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("SHOPPING SYSTEM\n\n");
                Console.WriteLine("[1] Add Item To Cart");
                Console.WriteLine("[2] View Cart");
                Console.WriteLine("[3] Remove Item From Cart");
                Console.WriteLine("[4] Checkout");
                Console.WriteLine("[5] Undo Last Action");
                Console.WriteLine("[6] Exit");

                Console.Write("\nCHOOSE: ");
                int choose = -1;
                while (!int.TryParse(Console.ReadLine(), out choose))
                    Console.Write("Invalid Option, CHOOSE: ");
                Console.Clear();

                switch (choose)
                {
                    case 1:
                        AddToCart(cart, logs);

                        break;
                    
                    case 2:
                        ViewCart(cart);

                        break;
                    
                    case 3:
                        RemoveFromCart(cart, logs);

                        break;
                    
                    case 4:
                        Checkout(cart , logs);

                        break;
                    
                    case 5:
                        UndoAction(cart , logs);
                        break;
                    
                    case 6:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }

                Console.WriteLine("\n\n PRESS ENTER...");
                Console.ReadKey();
            }
        }


        private static void AddToCart(Cart cart , Logs logs)
        {
            Product p = InputProduct();
            cart.Add(p);

            logs.ActionLogs.Push(new Log(p, Action.Add));

            p.Print();

        }

        private static void ViewCart(Cart cart)
        {
            var products = cart.Products;

            if (products == null || products.Count == 0)
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            Console.WriteLine($"┌{new string('─', 6)}┬{new string('─', 22)}┬{new string('─', 12)}┐");
            Console.WriteLine($"│ {"ID",-4} │ {"Name",-20} │ {"Price",-10} │");
            Console.WriteLine($"├{new string('─', 6)}┼{new string('─', 22)}┼{new string('─', 12)}┤");

            for (int i = 0; i < products.Count; i++)
            {

                Console.WriteLine($"│ {products[i].ID,-4} │ {products[i].Name,-20} │ {products[i].Price,-10 :c} │");

                if(i != products.Count - 1)
                    Console.WriteLine($"├{new string('─', 6)}┼{new string('─', 22)}┼{new string('─', 12)}┤");
                else
                    Console.WriteLine($"└{new string('─', 6)}┴{new string('─', 22)}┴{new string('─', 12)}┘");

            }

        }

        private static void RemoveFromCart(Cart cart, Logs logs)
        {
            var products = cart.Products;

            if (products == null || products.Count == 0)
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            Console.Write("Enter item ID to remove: ");
            int input;
            while (!int.TryParse(Console.ReadLine(), out input))
                Console.Write("Invalid input, enter a number: ");

            Product? product = products.FirstOrDefault(p => p.ID == input);

            if (product == null)
            {
                Console.WriteLine("No matching item found.");
                return;
            }

            cart.Remove(product);
            logs.ActionLogs.Push(new Log(product, Action.Remove));

            Console.WriteLine("Removed item:");
            product.Print();

        }

        private static void Checkout(Cart cart, Logs logs)
        {
            ViewCart(cart);
            decimal total = 0;
            int count = 0;
            foreach(var p in cart.Products) { total += p.Price; count++; }
            Console.WriteLine($" Products: {count}");
            Console.WriteLine($" TOTAL:   {total:c}");

            cart.Products.Clear();
            logs.ActionLogs.Clear();
        }

        private void UndoAction(Cart cart, Logs Logs)
        {
            Stack<Log> logs = Logs.ActionLogs;
            Log log;

            if (logs.Count != 0)
                log = logs.Pop();
            else
            {
                Console.WriteLine("There is not any action to undo");
                return;
            }

            Product product = log.Product;
            Action action = log.Action;

            if (action == Action.Add)
            {
                cart.Remove(product);
                Console.WriteLine($"{product.Name} is Removed\n\n");
            }
            else if(action == Action.Remove)
            {
                cart.Add(product);
                Console.WriteLine($"{product.Name} is Added\n\n");
            }

            ViewCart(cart);
        }


        private static Product InputProduct()
        {
            string name = "";
            decimal price = 0;

            Console.Write("Input Name: ");
            name = Console.ReadLine() ?? "";
            while (string.IsNullOrEmpty(name))
            {
                Console.Write("Must Input a Name : ");
                name = Console.ReadLine() ?? "";
            }

            Console.Write("Input Price: ");
            while (!decimal.TryParse(Console.ReadLine(), out price))
                Console.Write("Invalid Option, Input Price: ");

            return new Product(name, price);
        }



    }
}
