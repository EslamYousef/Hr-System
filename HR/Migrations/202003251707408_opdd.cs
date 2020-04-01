namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class opdd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Evalu_Element_Tran", "EvaluationGradeID", "dbo.EvaluationGrades");
            DropIndex("dbo.Evalu_Element_Tran", new[] { "EvaluationGradeID" });
            AddColumn("dbo.obje_eval_tran", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Evalu_Element_Tran", "EvaluationGradeID", c => c.Int());
            CreateIndex("dbo.Evalu_Element_Tran", "EvaluationGradeID");
            AddForeignKey("dbo.Evalu_Element_Tran", "EvaluationGradeID", "dbo.EvaluationGrades", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Evalu_Element_Tran", "EvaluationGradeID", "dbo.EvaluationGrades");
            DropIndex("dbo.Evalu_Element_Tran", new[] { "EvaluationGradeID" });
            AlterColumn("dbo.Evalu_Element_Tran", "EvaluationGradeID", c => c.Int(nullable: false));
            DropColumn("dbo.obje_eval_tran", "Date");
            CreateIndex("dbo.Evalu_Element_Tran", "EvaluationGradeID");
            AddForeignKey("dbo.Evalu_Element_Tran", "EvaluationGradeID", "dbo.EvaluationGrades", "ID", cascadeDelete: true);
        }
    }
}
