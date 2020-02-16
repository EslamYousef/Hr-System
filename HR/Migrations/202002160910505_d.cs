namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subscription_Syndicate", "Contact_methodsId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subscription_Syndicate", "Contact_methodsId", c => c.String(nullable: false));
        }
    }
}
