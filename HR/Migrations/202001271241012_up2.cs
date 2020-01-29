namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employee_family_profile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Employee_ProfileId = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Is_Reliable = c.Boolean(nullable: false),
                        Gender = c.Int(nullable: false),
                        Family_member_type = c.Int(nullable: false),
                        Degree_of_kinship = c.Int(nullable: false),
                        Marital_Status = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Id_type = c.Int(nullable: false),
                        Health_Status2 = c.Int(nullable: false),
                        Working_status = c.Int(nullable: false),
                        Emergency_level = c.Int(nullable: false),
                        NationalityId = c.String(),
                        Educate_TitleId = c.String(),
                        Name_of_educational_qualificationId = c.String(),
                        Start_relation_date = c.DateTime(nullable: false),
                        End_relation_date = c.DateTime(nullable: false),
                        Birth_date = c.DateTime(nullable: false),
                        Death_date = c.DateTime(nullable: false),
                        Id_number = c.String(),
                        Father_name = c.String(),
                        Mother_name = c.String(),
                        Subscribed = c.Boolean(nullable: false),
                        Working_in_oil_sector = c.Boolean(nullable: false),
                        Position_title = c.String(),
                        Company_name = c.String(),
                        Company_address = c.String(),
                        Working_in_company = c.Boolean(nullable: false),
                        Employee_Profile_WorkId = c.String(),
                        Is_emergency_contact_person = c.Boolean(nullable: false),
                        Home_phone_number = c.String(),
                        Mobil_phone_number = c.String(),
                        Address = c.String(),
                        Educate_Title_ID = c.Int(),
                        Name_of_educational_qualification_ID = c.Int(),
                        Nationality_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Educate_Title", t => t.Educate_Title_ID)
                .ForeignKey("dbo.Name_of_educational_qualification", t => t.Name_of_educational_qualification_ID)
                .ForeignKey("dbo.Nationalities", t => t.Nationality_ID)
                .Index(t => t.Educate_Title_ID)
                .Index(t => t.Name_of_educational_qualification_ID)
                .Index(t => t.Nationality_ID);
            
            AddColumn("dbo.Employee_Profile", "Employee_family_profile_ID", c => c.Int());
            AddColumn("dbo.Position_Information", "Organization_ChartId", c => c.String());
            AddColumn("dbo.Position_Information", "job_level_setup_ID", c => c.Int());
            AddColumn("dbo.Position_Information", "Organization_Chart_ID", c => c.Int());
            AddColumn("dbo.Position_Information", "Position_Transaction_Information_ID", c => c.Int());
            CreateIndex("dbo.Employee_Profile", "Employee_family_profile_ID");
            CreateIndex("dbo.Position_Information", "job_level_setup_ID");
            CreateIndex("dbo.Position_Information", "Organization_Chart_ID");
            CreateIndex("dbo.Position_Information", "Position_Transaction_Information_ID");
            AddForeignKey("dbo.Employee_Profile", "Employee_family_profile_ID", "dbo.Employee_family_profile", "ID");
            AddForeignKey("dbo.Position_Information", "job_level_setup_ID", "dbo.job_level_setup", "ID");
            AddForeignKey("dbo.Position_Information", "Organization_Chart_ID", "dbo.Organization_Chart", "ID");
            AddForeignKey("dbo.Position_Information", "Position_Transaction_Information_ID", "dbo.Position_Transaction_Information", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Position_Information", "Position_Transaction_Information_ID", "dbo.Position_Transaction_Information");
            DropForeignKey("dbo.Position_Information", "Organization_Chart_ID", "dbo.Organization_Chart");
            DropForeignKey("dbo.Position_Information", "job_level_setup_ID", "dbo.job_level_setup");
            DropForeignKey("dbo.Employee_Profile", "Employee_family_profile_ID", "dbo.Employee_family_profile");
            DropForeignKey("dbo.Employee_family_profile", "Nationality_ID", "dbo.Nationalities");
            DropForeignKey("dbo.Employee_family_profile", "Name_of_educational_qualification_ID", "dbo.Name_of_educational_qualification");
            DropForeignKey("dbo.Employee_family_profile", "Educate_Title_ID", "dbo.Educate_Title");
            DropIndex("dbo.Position_Information", new[] { "Position_Transaction_Information_ID" });
            DropIndex("dbo.Position_Information", new[] { "Organization_Chart_ID" });
            DropIndex("dbo.Position_Information", new[] { "job_level_setup_ID" });
            DropIndex("dbo.Employee_family_profile", new[] { "Nationality_ID" });
            DropIndex("dbo.Employee_family_profile", new[] { "Name_of_educational_qualification_ID" });
            DropIndex("dbo.Employee_family_profile", new[] { "Educate_Title_ID" });
            DropIndex("dbo.Employee_Profile", new[] { "Employee_family_profile_ID" });
            DropColumn("dbo.Position_Information", "Position_Transaction_Information_ID");
            DropColumn("dbo.Position_Information", "Organization_Chart_ID");
            DropColumn("dbo.Position_Information", "job_level_setup_ID");
            DropColumn("dbo.Position_Information", "Organization_ChartId");
            DropColumn("dbo.Employee_Profile", "Employee_family_profile_ID");
            DropTable("dbo.Employee_family_profile");
        }
    }
}
