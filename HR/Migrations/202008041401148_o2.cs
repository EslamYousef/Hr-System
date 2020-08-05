namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class o2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organization_Chart", "cost_center_id", c => c.Int(nullable: false));
            AddColumn("dbo.Organization_Chart", "shift_code_id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organization_Chart", "shift_code_id");
            DropColumn("dbo.Organization_Chart", "cost_center_id");
        }
    }
}
