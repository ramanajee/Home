using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;

namespace Home.Models
{
    public enum TransactionType
    {
        [Display(Name = "Income")]
        Income = 1,
        [Display(Name = "Expenditure")]
        Expenditure = 2
    }
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        [Required]
        [Display(Name = "Transaction Name")]
        public string TransactionName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateAction { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Today { get; set; }

        public TransactionType TransactionTypes { get; set; }

        public int CategoryId { get; set; }

        public string ApplicationUsersId { get; set; }

        public virtual Category Categories { get; set; }

        //every user has single user id

    }
}