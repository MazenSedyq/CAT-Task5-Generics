using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingSystem
{
    public class Logs
    {
        public Stack<Log> ActionLogs = new();
    }
    public class Log
    {
        public Product Product { get; set; }
        public Action Action { get; set; }

        public Log(Product product , Action action)
        {
            this.Product = product;
            this.Action = action;
        }

    }

    public enum Action
    {
        Add,
        Remove
    }
}
