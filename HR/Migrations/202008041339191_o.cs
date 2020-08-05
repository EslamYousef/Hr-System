namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class o : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Discipline_PunishmentTransaction", "Posted_to_payroll", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Discipline_PunishmentTransaction", "Posted_to_payroll");
        }
    }
}
