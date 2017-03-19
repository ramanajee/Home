using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Home.Models
{
    public class GroceriesCategories
    {
        public int GroceriesCategoriesId { get; set; }

        public string GroceriesName { get; set; }

        public string Remarks { get; set; }
        public string AppUserId { get; set; }

        public virtual ICollection<GroceriesList> Grocerieslists { get; set; }
    }
}