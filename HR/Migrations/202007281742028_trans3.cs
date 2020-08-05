namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trans3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Discipline_PunishmentTransaction_Detail", "guide", c => c.Guid(nullable: false));
            AddColumn("dbo.Discipline_PunishmentTransaction_Detail", "extra_frecuany", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Discipline_PunishmentTransaction_Detail", "extra_frecuany");
            DropColumn("dbo.Discipline_PunishmentTransaction_Detail", "guide");
        }
    }
}
