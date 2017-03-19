namespace Home.Migrations.HomeFolder
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        TransactionType = c.String(),
                        ApplicationUsersIdCategory = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.GroceriesCategories",
                c => new
                    {
                        GroceriesCategoriesId = c.Int(nullable: false, identity: true),
                        GroceriesName = c.String(),
                        AppUserId = c.String(),
                    })
                .PrimaryKey(t => t.GroceriesCategoriesId);
            
            CreateTable(
                "dbo.GroceriesLists",
                c => new
                    {
                        GroceriesListId = c.Int(nullable: false, identity: true),
                        ItemName = c.String(nullable: false),
                        GroceriesCategoriesId = c.Int(nullable: false),
                        appUserId = c.String(),
                    })
                .PrimaryKey(t => t.GroceriesListId)
                .ForeignKey("dbo.GroceriesCategories", t => t.GroceriesCategoriesId, cascadeDelete: true)
                .Index(t => t.GroceriesCategoriesId);
            
            CreateTable(
                "dbo.ShoppingLists",
                c => new
                    {
                        ShoppingListID = c.Int(nullable: false, identity: true),
                        Item = c.String(),
                        Category = c.String(),
                        PricePerPacket = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShoppingNameID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShoppingListID)
                .ForeignKey("dbo.ShoppingNames", t => t.ShoppingNameID, cascadeDelete: true)
                .Index(t => t.ShoppingNameID);
            
            CreateTable(
                "dbo.ShoppingNames",
                c => new
                    {
                        ShoppingNameID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AppUserId = c.String(),
                    })
                .PrimaryKey(t => t.ShoppingNameID);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        TransactionName = c.String(nullable: false),
                        Description = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateAction = c.DateTime(nullable: false),
                        Today = c.DateTime(nullable: false),
                        TransactionTypes = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        ApplicationUsersId = c.String(),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.ShoppingLists", "ShoppingNameID", "dbo.ShoppingNames");
            DropForeignKey("dbo.GroceriesLists", "GroceriesCategoriesId", "dbo.GroceriesCategories");
            DropIndex("dbo.Transactions", new[] { "CategoryId" });
            DropIndex("dbo.ShoppingLists", new[] { "ShoppingNameID" });
            DropIndex("dbo.GroceriesLists", new[] { "GroceriesCategoriesId" });
            DropTable("dbo.Transactions");
            DropTable("dbo.ShoppingNames");
            DropTable("dbo.ShoppingLists");
            DropTable("dbo.GroceriesLists");
            DropTable("dbo.GroceriesCategories");
            DropTable("dbo.Categories");
        }
    }
}
