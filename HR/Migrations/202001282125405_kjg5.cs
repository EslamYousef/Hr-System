namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kjg5 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Qualification_Major", new[] { "Code" });
            AlterColumn("dbo.Qualification_Major", "Code", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Qualification_Major", "Code", c => c.Double(nullable: false));
            CreateIndex("dbo.Qualification_Major", "Code", unique: true);
        }
    }
}
