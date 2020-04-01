namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lfgkfslgjslgs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.skill_eval",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        J_rate = c.Double(nullable: false),
                        em_rate = c.Double(nullable: false),
                        GAP = c.String(),
                        SkillID = c.Int(nullable: false),
                        EvaluationTransactionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.EvaluationTransactions", t => t.EvaluationTransactionID, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillID, cascadeDelete: true)
                .Index(t => t.SkillID)
                .Index(t => t.EvaluationTransactionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.skill_eval", "SkillID", "dbo.Skills");
            DropForeignKey("dbo.skill_eval", "EvaluationTransactionID", "dbo.EvaluationTransactions");
            DropIndex("dbo.skill_eval", new[] { "EvaluationTransactionID" });
            DropIndex("dbo.skill_eval", new[] { "SkillID" });
            DropTable("dbo.skill_eval");
        }
    }
}
