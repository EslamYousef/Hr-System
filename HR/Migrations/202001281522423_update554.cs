namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update554 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee_records", "sss", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee_records", "sss");
        }
    }
}
