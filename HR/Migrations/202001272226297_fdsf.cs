namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fdsf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EOS_Request", "req_date", c => c.String());
            AddColumn("dbo.EOS_Request", "eos_Date", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EOS_Request", "eos_Date");
            DropColumn("dbo.EOS_Request", "req_date");
        }
    }
}
