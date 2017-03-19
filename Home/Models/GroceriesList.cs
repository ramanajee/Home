using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Home.Models
{
    public class GroceriesList
    {
        public int GroceriesListId { get; set; }

        [Required]
        [Display(Name="Item")]
        public string  ItemName { get; set; }

        [Required]
        [Display(Name="Category")]
        public int GroceriesCategoriesId { get; set; }
        public string appUserId { get; set; }


        public virtual GroceriesCategories GroceriesCategory { get; set; }
    }
}