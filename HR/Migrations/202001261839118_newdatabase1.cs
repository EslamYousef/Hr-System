namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newdatabase1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee_Profile", "Position_Transaction_Information_ID", c => c.Int());
            CreateIndex("dbo.Employee_Profile", "Position_Transaction_Information_ID");
            AddForeignKey("dbo.Employee_Profile", "Position_Transaction_Information_ID", "dbo.Position_Transaction_Information", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employee_Profile", "Position_Transaction_Information_ID", "dbo.Position_Transaction_Information");
            DropIndex("dbo.Employee_Profile", new[] { "Position_Transaction_Information_ID" });
            DropColumn("dbo.Employee_Profile", "Position_Transaction_Information_ID");
        }
    }
}
