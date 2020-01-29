namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EOS_Request", "sss", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EOS_Request", "sss");
        }
    }
}
