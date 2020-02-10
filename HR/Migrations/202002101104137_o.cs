namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class o : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employee_Address_Profile", "countryid", c => c.String(nullable: false));
            AlterColumn("dbo.Employee_Address_Profile", "areaid", c => c.String(nullable: false));
            AlterColumn("dbo.Employee_Address_Profile", "stateid", c => c.String(nullable: false));
            AlterColumn("dbo.Employee_experience_profile", "External_compainesId", c => c.String());
            AlterColumn("dbo.Employee_experience_profile", "Rejection_ReasonsId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employee_experience_profile", "Rejection_ReasonsId", c => c.String(nullable: false));
            AlterColumn("dbo.Employee_experience_profile", "External_compainesId", c => c.String(nullable: false));
            AlterColumn("dbo.Employee_Address_Profile", "stateid", c => c.String());
            AlterColumn("dbo.Employee_Address_Profile", "areaid", c => c.String());
            AlterColumn("dbo.Employee_Address_Profile", "countryid", c => c.String());
        }
    }
}
