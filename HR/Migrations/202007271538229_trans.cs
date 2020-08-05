namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trans : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Discipline_PunishmentTransaction",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Transaction_Number = c.String(nullable: false, maxLength: 50),
                        Transaction_Date = c.DateTime(nullable: false),
                        Event_Date = c.DateTime(nullable: false),
                        Employee_Code = c.String(nullable: false),
                        Transaction_Statement = c.String(),
                        Custom_Rest = c.Boolean(),
                        RestOption_Code = c.String(),
                        RestOption_Date = c.DateTime(),
                        Transaction_Status = c.Short(),
                        ReportAsReadyBy = c.String(),
                        ReportAsReadyDate = c.DateTime(),
                        ReportAsReady_Comment = c.String(),
                        ApprovedBy = c.String(),
                        ApprovedDate = c.DateTime(),
                        Approved_Comment = c.String(),
                        RejectedBy = c.String(),
                        RejectedDate = c.DateTime(),
                        Rejected_Comment = c.String(),
                        CanceledBy = c.String(),
                        CanceledDate = c.DateTime(),
                        Canceled_Comment = c.String(),
                        ClosedBy = c.String(),
                        ClosedDate = c.DateTime(),
                        Closed_Comment = c.String(),
                        PostedBy = c.String(),
                        PostedDate = c.DateTime(),
                        Posted_Comment = c.String(),
                        Company_ID = c.String(),
                        Created_By = c.String(),
                        Created_Date = c.DateTime(),
                        Modified_By = c.String(),
                        Modified_Date = c.DateTime(),
                        RowIndx = c.Int(nullable: false),
                        stat_ID = c.Int(nullable: false),
                        stat_ID1 = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.status", t => t.stat_ID1)
                .Index(t => t.Transaction_Number, unique: true)
                .Index(t => t.stat_ID1);
            
            CreateTable(
                "dbo.Discipline_PunishmentTransaction_Detail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Transaction_Number = c.String(nullable: false),
                        Punishment_Code = c.String(nullable: false),
                        punis_des = c.String(),
                        Punishment_Frequency = c.Short(nullable: false),
                        PenaltyItem_Code = c.String(nullable: false),
                        penal_des = c.String(),
                        Punishment_RestDate = c.DateTime(nullable: false),
                        Company_ID = c.String(),
                        Created_By = c.String(),
                        Created_Date = c.DateTime(),
                        Modified_By = c.String(),
                        Modified_Date = c.DateTime(),
                        RowIndx = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Discipline_PunishmentTransaction", "stat_ID1", "dbo.status");
            DropIndex("dbo.Discipline_PunishmentTransaction", new[] { "stat_ID1" });
            DropIndex("dbo.Discipline_PunishmentTransaction", new[] { "Transaction_Number" });
            DropTable("dbo.Discipline_PunishmentTransaction_Detail");
            DropTable("dbo.Discipline_PunishmentTransaction");
        }
    }
}
