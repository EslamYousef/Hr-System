namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update55 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Air_ports", new[] { "Code" });
            DropIndex("dbo.Nationalities", new[] { "Code" });
            DropIndex("dbo.Religions", new[] { "Code" });
            DropIndex("dbo.Contact_methods", new[] { "Code" });
            DropIndex("dbo.Military_Service_Rank", new[] { "Code" });
            DropIndex("dbo.Military_Service_Status", new[] { "Code" });
            DropIndex("dbo.Rejection_Reasons", new[] { "Code" });
            AlterColumn("dbo.Air_ports", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.Nationalities", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.Religions", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.Contact_methods", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.Military_Service_Rank", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.Military_Service_Status", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.Rejection_Reasons", "Code", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rejection_Reasons", "Code", c => c.Double(nullable: false));
            AlterColumn("dbo.Military_Service_Status", "Code", c => c.Double(nullable: false));
            AlterColumn("dbo.Military_Service_Rank", "Code", c => c.Double(nullable: false));
            AlterColumn("dbo.Contact_methods", "Code", c => c.Double(nullable: false));
            AlterColumn("dbo.Religions", "Code", c => c.Double(nullable: false));
            AlterColumn("dbo.Nationalities", "Code", c => c.Double(nullable: false));
            AlterColumn("dbo.Air_ports", "Code", c => c.Double(nullable: false));
            CreateIndex("dbo.Rejection_Reasons", "Code", unique: true);
            CreateIndex("dbo.Military_Service_Status", "Code", unique: true);
            CreateIndex("dbo.Military_Service_Rank", "Code", unique: true);
            CreateIndex("dbo.Contact_methods", "Code", unique: true);
            CreateIndex("dbo.Religions", "Code", unique: true);
            CreateIndex("dbo.Nationalities", "Code", unique: true);
            CreateIndex("dbo.Air_ports", "Code", unique: true);
        }
    }
}
