namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.personnel_transaction", "default_cost_center_id", c => c.Int(nullable: false));
            AddColumn("dbo.personnel_transaction", "cost_center_id", c => c.Int(nullable: false));
            AddColumn("dbo.personnel_transaction", "default_shift_id", c => c.Int(nullable: false));
            AddColumn("dbo.personnel_transaction", "shift_id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.personnel_transaction", "shift_id");
            DropColumn("dbo.personnel_transaction", "default_shift_id");
            DropColumn("dbo.personnel_transaction", "cost_center_id");
            DropColumn("dbo.personnel_transaction", "default_cost_center_id");
        }
    }
}
