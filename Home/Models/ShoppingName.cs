using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Home.Models
{
    public class ShoppingName
    {
        public int ShoppingNameID { get; set; }

        public string Name { get; set; }
        public string AppUserId { get; set; }
        //public DateTime ShoppDate { get; set; }

        public virtual IEnumerable<ShoppingList> ShoppingList { get; set; }
       

    }
}