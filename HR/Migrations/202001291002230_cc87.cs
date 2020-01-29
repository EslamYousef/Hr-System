namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cc87 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.check_request_change_status", "selected_signby", c => c.Int(nullable: false));
            AddColumn("dbo.check_request_change_status", "selected_directedto", c => c.Int(nullable: false));
            AddColumn("dbo.check_request_change_status", "Directed_to_ID", c => c.Int());
            AddColumn("dbo.check_request_change_status", "Sign_by_ID", c => c.Int());
            CreateIndex("dbo.check_request_change_status", "Directed_to_ID");
            CreateIndex("dbo.check_request_change_status", "Sign_by_ID");
            AddForeignKey("dbo.check_request_change_status", "Directed_to_ID", "dbo.Employee_Profile", "ID");
            AddForeignKey("dbo.check_request_change_status", "Sign_by_ID", "dbo.Employee_Profile", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.check_request_change_status", "Sign_by_ID", "dbo.Employee_Profile");
            DropForeignKey("dbo.check_request_change_status", "Directed_to_ID", "dbo.Employee_Profile");
            DropIndex("dbo.check_request_change_status", new[] { "Sign_by_ID" });
            DropIndex("dbo.check_request_change_status", new[] { "Directed_to_ID" });
            DropColumn("dbo.check_request_change_status", "Sign_by_ID");
            DropColumn("dbo.check_request_change_status", "Directed_to_ID");
            DropColumn("dbo.check_request_change_status", "selected_directedto");
            DropColumn("dbo.check_request_change_status", "selected_signby");
        }
    }
}
