namespace Home.Migrations.IdentityFolder
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Secondmigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "ProfileUserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ProfileUserName", c => c.String());
        }
    }
}
