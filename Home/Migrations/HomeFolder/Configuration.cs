namespace Home.Migrations.HomeFolder
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Home.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<Home.DAL.HomeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\HomeFolder";
        }

        protected override void Seed(Home.DAL.HomeContext context)
        {
            //var income = new List<Income>()
            //{
            //    new Income{CategoryName="salary",ApplicationUsersIdCategory=null},
            //    new Income{CategoryName="Rent",ApplicationUsersIdCategory=null},
            //    new Income{CategoryName="Business",ApplicationUsersIdCategory=null},
            //    new Income{CategoryName="Gifts",ApplicationUsersIdCategory=null},
            //    new Income{CategoryName="Interests on Savings",ApplicationUsersIdCategory=null}
            //};
            //income.ForEach(c => context.Categories.AddOrUpdate(p => p.CategoryName, c));
            //context.SaveChanges();
            //var expenditure = new List<Expenditure>()
            //{
            //    new Expenditure{CategoryName="Market",ApplicationUsersIdCategory=null},
            //    new Expenditure{CategoryName="Home Maintainance",ApplicationUsersIdCategory=null},
            //    new Expenditure{CategoryName="Health",ApplicationUsersIdCategory=null},
            //    new Expenditure{CategoryName="School Fee",ApplicationUsersIdCategory=null},
            //    new Expenditure{CategoryName="Loans",ApplicationUsersIdCategory=null}
            //};
            //expenditure.ForEach(c => context.Categories.AddOrUpdate(p => p.CategoryName, c));
            //context.SaveChanges();

            var category = new List<Category>()
            {
                new Category{CategoryName="salary",TransactionType="INCOME",ApplicationUsersIdCategory="all"},
                new  Category{CategoryName="Rent",TransactionType="INCOME",ApplicationUsersIdCategory="all"},
                new Category{CategoryName="Business",TransactionType="INCOME",ApplicationUsersIdCategory="all"},
                new Category{CategoryName="Gifts",TransactionType="INCOME",ApplicationUsersIdCategory="all"},
                new Category{CategoryName="Interests on Savings",TransactionType="INCOME",ApplicationUsersIdCategory="all"},

                new Category{CategoryName="Market",TransactionType="EXPENDITURE",ApplicationUsersIdCategory="all"},
                new Category{CategoryName="Home Maintainance",TransactionType="EXPENDITURE",ApplicationUsersIdCategory="all"},
                new Category{CategoryName="Health",TransactionType="EXPENDITURE",ApplicationUsersIdCategory="all"},
                new Category{CategoryName="School Fee",TransactionType="EXPENDITURE",ApplicationUsersIdCategory="all"},
                new Category{CategoryName="Loans",TransactionType="EXPENDITURE",ApplicationUsersIdCategory="all"}

            };
            category.ForEach(c => context.Categories.AddOrUpdate(p => p.CategoryName, c));
            context.SaveChanges();

            var groceryCategoey = new List<GroceriesCategories>()
            {
                new GroceriesCategories{GroceriesName="Beverages",AppUserId="all"},
                new GroceriesCategories{GroceriesName="Bread/Bakery",AppUserId="all"},
                new GroceriesCategories{GroceriesName="Canned/Jarred Goods",AppUserId="all"},
                new GroceriesCategories{GroceriesName="Dairy ",AppUserId="all"},
                new GroceriesCategories{GroceriesName="Dry/Baking Goods",AppUserId="all"},
                new GroceriesCategories{GroceriesName="Frozen Foods",AppUserId="all"},
                new GroceriesCategories{GroceriesName="Meat",AppUserId="all"},
                new GroceriesCategories{GroceriesName="fruits",AppUserId="all"},
                new GroceriesCategories{GroceriesName="vegetables",AppUserId="all"},
                new GroceriesCategories{GroceriesName="Cleaners ",AppUserId="all"},
                new GroceriesCategories{GroceriesName="Paper Goods ",AppUserId="all"}
            };
            groceryCategoey.ForEach(c => context.GroceriesCategory.AddOrUpdate(p => p.GroceriesName, c));
            context.SaveChanges();
        }
    }
}
