namespace Home.Migrations.HomeFolder
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cat : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroceriesLists", "GroceriesCategoriesId", "dbo.GroceriesCategories");
            DropForeignKey("dbo.Transactions", "CategoryId", "dbo.Categories");
            AddColumn("dbo.Categories", "Remarks", c => c.String());
            AddColumn("dbo.GroceriesCategories", "Remarks", c => c.String());
            AddForeignKey("dbo.GroceriesLists", "GroceriesCategoriesId", "dbo.GroceriesCategories", "GroceriesCategoriesId");
            AddForeignKey("dbo.Transactions", "CategoryId", "dbo.Categories", "CategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.GroceriesLists", "GroceriesCategoriesId", "dbo.GroceriesCategories");
            DropColumn("dbo.GroceriesCategories", "Remarks");
            DropColumn("dbo.Categories", "Remarks");
            AddForeignKey("dbo.Transactions", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
            AddForeignKey("dbo.GroceriesLists", "GroceriesCategoriesId", "dbo.GroceriesCategories", "GroceriesCategoriesId", cascadeDelete: true);
        }
    }
}
