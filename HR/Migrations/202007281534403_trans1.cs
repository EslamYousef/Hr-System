namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trans1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Discipline_PunishmentTransaction", "em", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Discipline_PunishmentTransaction", "em");
        }
    }
}
