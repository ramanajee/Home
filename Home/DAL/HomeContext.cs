using Home.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Home.DAL
{
    public class HomeContext:DbContext
    {
        public HomeContext():base("HomeDataBase")
        {

        }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<ShoppingName> ShoppingNames { get; set; }
        public DbSet<GroceriesCategories> GroceriesCategory { get; set; }
        public DbSet<GroceriesList> GroceriesLists { get; set; }
        public DbSet<userPics> userPics { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasRequired(t => t.Categories)
                .WithMany(t => t.Transactions)
                .HasForeignKey(d => d.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GroceriesList>()
                .HasRequired(t => t.GroceriesCategory)
                .WithMany(t => t.Grocerieslists)
                .HasForeignKey(d => d.GroceriesCategoriesId)
                .WillCascadeOnDelete(false);
        }
    }
}