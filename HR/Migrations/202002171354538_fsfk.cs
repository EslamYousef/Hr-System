namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fsfk : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employee_Profile", "Code", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employee_Profile", "Code", c => c.String(nullable: false));
        }
    }
}
