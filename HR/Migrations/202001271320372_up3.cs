namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.personnel_transaction",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        transaction_date = c.DateTime(nullable: false),
                        Effective_date = c.DateTime(nullable: false),
                        Transaction_type = c.Int(nullable: false),
                        Employee_ID = c.Int(),
                        Position_Information_ID = c.Int(),
                        Position_Transaction_Information_ID = c.Int(),
                        status_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employee_Profile", t => t.Employee_ID)
                .ForeignKey("dbo.Position_Information", t => t.Position_Information_ID)
                .ForeignKey("dbo.Position_Transaction_Information", t => t.Position_Transaction_Information_ID)
                .ForeignKey("dbo.status", t => t.status_ID)
                .Index(t => t.Employee_ID)
                .Index(t => t.Position_Information_ID)
                .Index(t => t.Position_Transaction_Information_ID)
                .Index(t => t.status_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.personnel_transaction", "status_ID", "dbo.status");
            DropForeignKey("dbo.personnel_transaction", "Position_Transaction_Information_ID", "dbo.Position_Transaction_Information");
            DropForeignKey("dbo.personnel_transaction", "Position_Information_ID", "dbo.Position_Information");
            DropForeignKey("dbo.personnel_transaction", "Employee_ID", "dbo.Employee_Profile");
            DropIndex("dbo.personnel_transaction", new[] { "status_ID" });
            DropIndex("dbo.personnel_transaction", new[] { "Position_Transaction_Information_ID" });
            DropIndex("dbo.personnel_transaction", new[] { "Position_Information_ID" });
            DropIndex("dbo.personnel_transaction", new[] { "Employee_ID" });
            DropTable("dbo.personnel_transaction");
        }
    }
}
