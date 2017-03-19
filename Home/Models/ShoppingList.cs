using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Home.Models
{
    public class ShoppingList
    {
        public int ShoppingListID { get; set; }
        public string Item { get; set; }
        public string Category { get; set; }
        public decimal PricePerPacket { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public int ShoppingNameID { get; set; }
        public string userID { get; set; }
        public virtual ShoppingName ShoppingName { get; set; }
    }
}