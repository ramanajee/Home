namespace Home.Migrations.HomeFolder
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShoppingListUserID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingLists", "userID", c => c.String());
        }
        public override void Down()
        {
            DropColumn("dbo.ShoppingLists", "userID");
        }
    }
}
