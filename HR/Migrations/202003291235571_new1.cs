namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Organization_Chart", "worklocationid", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Organization_Chart", "worklocationid", c => c.String(nullable: false));
        }
    }
}
