namespace Home.Migrations.IdentityFolder
{
    using Home.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Home.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\IdentityFolder";
        }

        protected override void Seed(Home.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            SeedMethod();

        }


        public void SeedMethod()
        {
            Home.DAL.HomeContext context = new DAL.HomeContext();

            var category = new List<Category>()
            {
                new Category{CategoryName="salary",TransactionType="INCOME",ApplicationUsersIdCategory=null},
                new  Category{CategoryName="Rent",TransactionType="INCOME",ApplicationUsersIdCategory=null},
                new Category{CategoryName="Business",TransactionType="INCOME",ApplicationUsersIdCategory=null},
                new Category{CategoryName="Gifts",TransactionType="INCOME",ApplicationUsersIdCategory=null},
                new Category{CategoryName="Interests on Savings",TransactionType="INCOME",ApplicationUsersIdCategory=null},

                new Category{CategoryName="Market",TransactionType="EXPENDITURE",ApplicationUsersIdCategory=null},
                new Category{CategoryName="Home Maintainance",TransactionType="EXPENDITURE",ApplicationUsersIdCategory=null},
                new Category{CategoryName="Health",TransactionType="EXPENDITURE",ApplicationUsersIdCategory=null},
                new Category{CategoryName="School Fee",TransactionType="EXPENDITURE",ApplicationUsersIdCategory=null},
                new Category{CategoryName="Loans",TransactionType="EXPENDITURE",ApplicationUsersIdCategory=null}

            };
            category.ForEach(c => context.Categories.AddOrUpdate(p => p.CategoryName, c));
            context.SaveChanges();

            var groceryCategoey = new List<GroceriesCategories>()
            {
                new GroceriesCategories{GroceriesName="Beverages",AppUserId=null},
                new GroceriesCategories{GroceriesName="Bread/Bakery",AppUserId=null},
                new GroceriesCategories{GroceriesName="Canned/Jarred Goods",AppUserId=null},
                new GroceriesCategories{GroceriesName="Dairy ",AppUserId=null},
                new GroceriesCategories{GroceriesName="Dry/Baking Goods",AppUserId=null},
                new GroceriesCategories{GroceriesName="Frozen Foods",AppUserId=null},
                new GroceriesCategories{GroceriesName="Meat",AppUserId=null},
                new GroceriesCategories{GroceriesName="fruits",AppUserId=null},
                new GroceriesCategories{GroceriesName="vegetables",AppUserId=null},
                new GroceriesCategories{GroceriesName="Cleaners ",AppUserId=null},
                new GroceriesCategories{GroceriesName="Paper Goods ",AppUserId=null}
            };
            groceryCategoey.ForEach(c => context.GroceriesCategory.AddOrUpdate(p => p.GroceriesName, c));
            context.SaveChanges();
        }
    }
}
