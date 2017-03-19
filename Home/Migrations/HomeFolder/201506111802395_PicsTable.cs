namespace Home.Migrations.HomeFolder
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PicsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.userPics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        image = c.Binary(),
                        Userid = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.userPics");
        }
    }
}
