namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ffl : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.personnel_transaction", "Position_Information_ID", "dbo.Position_Information");
            DropForeignKey("dbo.personnel_transaction", "Position_Transaction_Information_ID", "dbo.Position_Transaction_Information");
            DropIndex("dbo.personnel_transaction", new[] { "Position_Information_ID" });
            DropIndex("dbo.personnel_transaction", new[] { "Position_Transaction_Information_ID" });
            AddColumn("dbo.personnel_transaction", "Position_transaction_no", c => c.String());
            AddColumn("dbo.personnel_transaction", "Position_transaction", c => c.DateTime(nullable: false));
            AddColumn("dbo.personnel_transaction", "Fixed_basic_salary_by", c => c.Int(nullable: false));
            AddColumn("dbo.personnel_transaction", "Resolution_number", c => c.String());
            AddColumn("dbo.personnel_transaction", "Resolution_date", c => c.DateTime(nullable: false));
            AddColumn("dbo.personnel_transaction", "Activity_number", c => c.String());
            AddColumn("dbo.personnel_transaction", "Memo_number", c => c.String());
            AddColumn("dbo.personnel_transaction", "Memo_date", c => c.DateTime(nullable: false));
            AddColumn("dbo.personnel_transaction", "Recommended_by", c => c.String());
            AddColumn("dbo.personnel_transaction", "Approved_by", c => c.String());
            AddColumn("dbo.personnel_transaction", "Approved_date", c => c.DateTime(nullable: false));
            AddColumn("dbo.personnel_transaction", "Employee_ProfileId", c => c.String(nullable: false));
            AddColumn("dbo.personnel_transaction", "Code", c => c.String(nullable: false));
            AddColumn("dbo.personnel_transaction", "Primary_Position", c => c.Boolean(nullable: false));
            AddColumn("dbo.personnel_transaction", "job_descId", c => c.String());
            AddColumn("dbo.personnel_transaction", "SlotdescId", c => c.String());
            AddColumn("dbo.personnel_transaction", "Default_location_descId", c => c.String());
            AddColumn("dbo.personnel_transaction", "Location_descId", c => c.String());
            AddColumn("dbo.personnel_transaction", "Job_level_gradeId", c => c.String());
            AddColumn("dbo.personnel_transaction", "Organization_ChartId", c => c.String());
            AddColumn("dbo.personnel_transaction", "From_date", c => c.DateTime(nullable: false));
            AddColumn("dbo.personnel_transaction", "To_date", c => c.DateTime(nullable: false));
            AddColumn("dbo.personnel_transaction", "Years", c => c.Int(nullable: false));
            AddColumn("dbo.personnel_transaction", "Months", c => c.Int(nullable: false));
            AddColumn("dbo.personnel_transaction", "End_of_service_date", c => c.DateTime(nullable: false));
            AddColumn("dbo.personnel_transaction", "Last_working_date", c => c.DateTime(nullable: false));
            AddColumn("dbo.personnel_transaction", "Commnets", c => c.String());
            AddColumn("dbo.personnel_transaction", "working_system", c => c.Int(nullable: false));
            AddColumn("dbo.personnel_transaction", "Position_status", c => c.Int(nullable: false));
            AddColumn("dbo.personnel_transaction", "EOS_reasons", c => c.Int(nullable: false));
            AddColumn("dbo.personnel_transaction", "Job_level_grade_ID", c => c.Int());
            AddColumn("dbo.personnel_transaction", "job_level_setup_ID", c => c.Int());
            AddColumn("dbo.personnel_transaction", "job_title_cards_ID", c => c.Int());
            AddColumn("dbo.personnel_transaction", "Organization_Chart_ID", c => c.Int());
            AddColumn("dbo.personnel_transaction", "work_location_ID", c => c.Int());
            CreateIndex("dbo.personnel_transaction", "Job_level_grade_ID");
            CreateIndex("dbo.personnel_transaction", "job_level_setup_ID");
            CreateIndex("dbo.personnel_transaction", "job_title_cards_ID");
            CreateIndex("dbo.personnel_transaction", "Organization_Chart_ID");
            CreateIndex("dbo.personnel_transaction", "work_location_ID");
            AddForeignKey("dbo.personnel_transaction", "Job_level_grade_ID", "dbo.Job_level_grade", "ID");
            AddForeignKey("dbo.personnel_transaction", "job_level_setup_ID", "dbo.job_level_setup", "ID");
            AddForeignKey("dbo.personnel_transaction", "job_title_cards_ID", "dbo.job_title_cards", "ID");
            AddForeignKey("dbo.personnel_transaction", "Organization_Chart_ID", "dbo.Organization_Chart", "ID");
            AddForeignKey("dbo.personnel_transaction", "work_location_ID", "dbo.work_location", "ID");
            DropColumn("dbo.personnel_transaction", "Position_Information_ID");
            DropColumn("dbo.personnel_transaction", "Position_Transaction_Information_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.personnel_transaction", "Position_Transaction_Information_ID", c => c.Int());
            AddColumn("dbo.personnel_transaction", "Position_Information_ID", c => c.Int());
            DropForeignKey("dbo.personnel_transaction", "work_location_ID", "dbo.work_location");
            DropForeignKey("dbo.personnel_transaction", "Organization_Chart_ID", "dbo.Organization_Chart");
            DropForeignKey("dbo.personnel_transaction", "job_title_cards_ID", "dbo.job_title_cards");
            DropForeignKey("dbo.personnel_transaction", "job_level_setup_ID", "dbo.job_level_setup");
            DropForeignKey("dbo.personnel_transaction", "Job_level_grade_ID", "dbo.Job_level_grade");
            DropIndex("dbo.personnel_transaction", new[] { "work_location_ID" });
            DropIndex("dbo.personnel_transaction", new[] { "Organization_Chart_ID" });
            DropIndex("dbo.personnel_transaction", new[] { "job_title_cards_ID" });
            DropIndex("dbo.personnel_transaction", new[] { "job_level_setup_ID" });
            DropIndex("dbo.personnel_transaction", new[] { "Job_level_grade_ID" });
            DropColumn("dbo.personnel_transaction", "work_location_ID");
            DropColumn("dbo.personnel_transaction", "Organization_Chart_ID");
            DropColumn("dbo.personnel_transaction", "job_title_cards_ID");
            DropColumn("dbo.personnel_transaction", "job_level_setup_ID");
            DropColumn("dbo.personnel_transaction", "Job_level_grade_ID");
            DropColumn("dbo.personnel_transaction", "EOS_reasons");
            DropColumn("dbo.personnel_transaction", "Position_status");
            DropColumn("dbo.personnel_transaction", "working_system");
            DropColumn("dbo.personnel_transaction", "Commnets");
            DropColumn("dbo.personnel_transaction", "Last_working_date");
            DropColumn("dbo.personnel_transaction", "End_of_service_date");
            DropColumn("dbo.personnel_transaction", "Months");
            DropColumn("dbo.personnel_transaction", "Years");
            DropColumn("dbo.personnel_transaction", "To_date");
            DropColumn("dbo.personnel_transaction", "From_date");
            DropColumn("dbo.personnel_transaction", "Organization_ChartId");
            DropColumn("dbo.personnel_transaction", "Job_level_gradeId");
            DropColumn("dbo.personnel_transaction", "Location_descId");
            DropColumn("dbo.personnel_transaction", "Default_location_descId");
            DropColumn("dbo.personnel_transaction", "SlotdescId");
            DropColumn("dbo.personnel_transaction", "job_descId");
            DropColumn("dbo.personnel_transaction", "Primary_Position");
            DropColumn("dbo.personnel_transaction", "Code");
            DropColumn("dbo.personnel_transaction", "Employee_ProfileId");
            DropColumn("dbo.personnel_transaction", "Approved_date");
            DropColumn("dbo.personnel_transaction", "Approved_by");
            DropColumn("dbo.personnel_transaction", "Recommended_by");
            DropColumn("dbo.personnel_transaction", "Memo_date");
            DropColumn("dbo.personnel_transaction", "Memo_number");
            DropColumn("dbo.personnel_transaction", "Activity_number");
            DropColumn("dbo.personnel_transaction", "Resolution_date");
            DropColumn("dbo.personnel_transaction", "Resolution_number");
            DropColumn("dbo.personnel_transaction", "Fixed_basic_salary_by");
            DropColumn("dbo.personnel_transaction", "Position_transaction");
            DropColumn("dbo.personnel_transaction", "Position_transaction_no");
            CreateIndex("dbo.personnel_transaction", "Position_Transaction_Information_ID");
            CreateIndex("dbo.personnel_transaction", "Position_Information_ID");
            AddForeignKey("dbo.personnel_transaction", "Position_Transaction_Information_ID", "dbo.Position_Transaction_Information", "ID");
            AddForeignKey("dbo.personnel_transaction", "Position_Information_ID", "dbo.Position_Information", "ID");
        }
    }
}
