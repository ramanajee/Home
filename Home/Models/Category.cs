using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;
using System.Web;

namespace Home.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Display(Name = "Category Name")]
        [Required]
        public string CategoryName { get; set; }
        public string TransactionType { get; set; }

        //[Display(Name = "Date")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime DateOfCreation { get; set; }

        public string Remarks { get; set; }

        public string ApplicationUsersIdCategory { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
