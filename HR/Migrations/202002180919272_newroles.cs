namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newroles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        iD = c.Int(nullable: false, identity: true),
                        roles = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.iD);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Roles");
        }
    }
}
