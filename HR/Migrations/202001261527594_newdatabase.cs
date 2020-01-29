namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newdatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abilities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Inability_reason = c.String(),
                        Inability_description = c.String(),
                        registration_number = c.String(),
                        registration_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Air_ports",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        the_statesid = c.String(nullable: false),
                        areaid = c.Int(nullable: false),
                        countryid = c.Int(nullable: false),
                        the_states_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.the_states", t => t.the_states_ID)
                .Index(t => t.Code, unique: true)
                .Index(t => t.the_states_ID);
            
            CreateTable(
                "dbo.the_states",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Areaid = c.String(nullable: false),
                        CountryID = c.String(),
                        Area_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Areas", t => t.Area_ID)
                .Index(t => t.Code, unique: true)
                .Index(t => t.Area_ID);
            
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Countryid = c.String(nullable: false),
                        Country_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Countries", t => t.Country_ID)
                .Index(t => t.Code, unique: true)
                .Index(t => t.Country_ID);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.Allergy_Type",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Allergy_Code = c.Double(nullable: false),
                        Allergy_Name = c.String(nullable: false),
                        Allergy_TName = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Allergy_Code, unique: true);
            
            CreateTable(
                "dbo.Allowance_years",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Allowance_year = c.Double(nullable: false),
                        Allowance_percentage = c.Double(nullable: false),
                        min_Allowance_amount = c.Double(nullable: false),
                        max_Allowance_amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Answer_EOS",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        answer = c.String(),
                        Notes = c.String(),
                        EOS_ID = c.Int(),
                        question_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EOS_Request", t => t.EOS_ID)
                .ForeignKey("dbo.Definition_of_EOS_Interview_Questions", t => t.question_ID)
                .Index(t => t.EOS_ID)
                .Index(t => t.question_ID);
            
            CreateTable(
                "dbo.EOS_Request",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        number = c.String(nullable: false),
                        Requset_date = c.DateTime(nullable: false),
                        EOS_type = c.Int(nullable: false),
                        Notice_period_type = c.Int(nullable: false),
                        Notice_period = c.Double(nullable: false),
                        Date_of_EOS = c.DateTime(nullable: false),
                        last_work_day_before_request = c.DateTime(nullable: false),
                        last_Date_of_work_after_notice_period = c.DateTime(nullable: false),
                        are_the_employee_has_a_loan_or_advanced = c.Boolean(nullable: false),
                        are_the_settlement_transferred_to_payroll = c.Boolean(nullable: false),
                        date_of_eos_interview = c.DateTime(nullable: false),
                        date_of_settlement = c.DateTime(nullable: false),
                        note = c.String(),
                        check_status = c.Int(nullable: false),
                        Check_List_Item_Groups_ID = c.Int(),
                        Employee_ID = c.Int(),
                        EOS_group_ID = c.Int(),
                        status_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Check_List_Item_Groups", t => t.Check_List_Item_Groups_ID)
                .ForeignKey("dbo.Employee_Profile", t => t.Employee_ID)
                .ForeignKey("dbo.EOS_Interview_Questions_Groups", t => t.EOS_group_ID)
                .ForeignKey("dbo.status", t => t.status_ID)
                .Index(t => t.Check_List_Item_Groups_ID)
                .Index(t => t.Employee_ID)
                .Index(t => t.EOS_group_ID)
                .Index(t => t.status_ID);
            
            CreateTable(
                "dbo.check_list_EOS",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        interpolation = c.Boolean(nullable: false),
                        EOS_ID = c.Int(),
                        item_ID = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.EOS_Request", t => t.EOS_ID)
                .ForeignKey("dbo.Check_Lists_Items", t => t.item_ID)
                .Index(t => t.EOS_ID)
                .Index(t => t.item_ID);
            
            CreateTable(
                "dbo.Check_Lists_Items",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Check_Code = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Description_Alternative = c.String(),
                        Is_Mandatory = c.Boolean(nullable: false),
                        Check_List_Item_GroupsId = c.String(nullable: false),
                        Check_List_Item_Groups_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Check_List_Item_Groups", t => t.Check_List_Item_Groups_ID)
                .Index(t => t.Check_List_Item_Groups_ID);
            
            CreateTable(
                "dbo.Check_List_Item_Groups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Group_Code = c.String(nullable: false),
                        Description_Group = c.String(nullable: false),
                        Description_Alternative = c.String(),
                        The_Purpose = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Employee_Profile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Full_Name = c.String(nullable: false),
                        Arabic = c.String(),
                        Full = c.String(),
                        Surname = c.String(),
                        Sur_Name = c.String(),
                        Gender = c.Int(nullable: false),
                        Marital_Status = c.Int(nullable: false),
                        ReligionId = c.String(),
                        NationalityId = c.String(),
                        Citizen = c.Boolean(nullable: false),
                        Blood_group = c.Int(nullable: false),
                        ID_type = c.Int(nullable: false),
                        Health_Status = c.Int(nullable: false),
                        Birth_date = c.DateTime(nullable: false),
                        ID_number = c.String(),
                        ID_number_in_attendance_machine = c.String(),
                        Issue_date = c.DateTime(nullable: false),
                        Expire_date = c.DateTime(nullable: false),
                        Countryid = c.String(),
                        Countryaddressid = c.String(),
                        Areaid = c.String(),
                        Areaaddressid = c.String(),
                        the_statesid = c.String(),
                        the_statesaddressid = c.String(),
                        Territoriesid = c.String(),
                        Territoriesaddressid = c.String(),
                        citiesid = c.String(),
                        citiesaddressid = c.String(),
                        Ability_ID = c.Int(),
                        Area_ID = c.Int(),
                        cities_ID = c.Int(),
                        Country_ID = c.Int(),
                        Employee_Address_Profile_ID = c.Int(),
                        Employee_Positions_Profile_ID = c.Int(),
                        Employee_Qualification_Profile_ID = c.Int(),
                        Nationality_ID = c.Int(),
                        Personnel_Information_ID = c.Int(),
                        Religion_ID = c.Int(),
                        Service_Information_ID = c.Int(),
                        Territories_ID = c.Int(),
                        the_states_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Abilities", t => t.Ability_ID)
                .ForeignKey("dbo.Areas", t => t.Area_ID)
                .ForeignKey("dbo.cities", t => t.cities_ID)
                .ForeignKey("dbo.Countries", t => t.Country_ID)
                .ForeignKey("dbo.Employee_Address_Profile", t => t.Employee_Address_Profile_ID)
                .ForeignKey("dbo.Position_Information", t => t.Employee_Positions_Profile_ID)
                .ForeignKey("dbo.Employee_Qualification_Profile", t => t.Employee_Qualification_Profile_ID)
                .ForeignKey("dbo.Nationalities", t => t.Nationality_ID)
                .ForeignKey("dbo.Personnel_Information", t => t.Personnel_Information_ID)
                .ForeignKey("dbo.Religions", t => t.Religion_ID)
                .ForeignKey("dbo.Service_Information", t => t.Service_Information_ID)
                .ForeignKey("dbo.Territories", t => t.Territories_ID)
                .ForeignKey("dbo.the_states", t => t.the_states_ID)
                .Index(t => t.Ability_ID)
                .Index(t => t.Area_ID)
                .Index(t => t.cities_ID)
                .Index(t => t.Country_ID)
                .Index(t => t.Employee_Address_Profile_ID)
                .Index(t => t.Employee_Positions_Profile_ID)
                .Index(t => t.Employee_Qualification_Profile_ID)
                .Index(t => t.Nationality_ID)
                .Index(t => t.Personnel_Information_ID)
                .Index(t => t.Religion_ID)
                .Index(t => t.Service_Information_ID)
                .Index(t => t.Territories_ID)
                .Index(t => t.the_states_ID);
            
            CreateTable(
                "dbo.cities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Territoriesid = c.String(nullable: false),
                        stateid = c.Int(nullable: false),
                        areaid = c.Int(nullable: false),
                        countryid = c.Int(nullable: false),
                        Territories_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Territories", t => t.Territories_ID)
                .Index(t => t.Code, unique: true)
                .Index(t => t.Territories_ID);
            
            CreateTable(
                "dbo.Territories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        the_statesid = c.String(nullable: false),
                        areaid = c.Int(nullable: false),
                        countryid = c.Int(nullable: false),
                        the_states_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.the_states", t => t.the_states_ID)
                .Index(t => t.Code, unique: true)
                .Index(t => t.the_states_ID);
            
            CreateTable(
                "dbo.Employee_Address_Profile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Employee_ProfileId = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        Resident = c.Boolean(nullable: false),
                        countryid = c.String(),
                        areaid = c.String(),
                        stateid = c.String(),
                        Territoriesid = c.String(),
                        citiesid = c.String(),
                        postcodeId = c.String(),
                        Streetname = c.String(),
                        Streetnumber = c.Int(nullable: false),
                        Pobox = c.String(),
                        Distancetoworklocationkm = c.Int(nullable: false),
                        Transportation_method = c.Int(nullable: false),
                        DistancefromMeetingpointtoworklocationkm = c.Int(nullable: false),
                        Area_ID = c.Int(),
                        cities_ID = c.Int(),
                        Country_ID = c.Int(),
                        postcode_ID = c.Int(),
                        Territories_ID = c.Int(),
                        the_states_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Areas", t => t.Area_ID)
                .ForeignKey("dbo.cities", t => t.cities_ID)
                .ForeignKey("dbo.Countries", t => t.Country_ID)
                .ForeignKey("dbo.postcodes", t => t.postcode_ID)
                .ForeignKey("dbo.Territories", t => t.Territories_ID)
                .ForeignKey("dbo.the_states", t => t.the_states_ID)
                .Index(t => t.Area_ID)
                .Index(t => t.cities_ID)
                .Index(t => t.Country_ID)
                .Index(t => t.postcode_ID)
                .Index(t => t.Territories_ID)
                .Index(t => t.the_states_ID);
            
            CreateTable(
                "dbo.postcodes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        streetname = c.String(nullable: false),
                        numstreetfrom = c.Int(nullable: false),
                        numstreetto = c.Int(nullable: false),
                        typenumstreet = c.Int(nullable: false),
                        Territoriesid = c.String(),
                        citiesid = c.String(nullable: false),
                        stateid = c.Int(nullable: false),
                        areaid = c.Int(nullable: false),
                        countryid = c.Int(nullable: false),
                        cities_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.cities", t => t.cities_ID)
                .Index(t => t.Code, unique: true)
                .Index(t => t.cities_ID);
            
            CreateTable(
                "dbo.Position_Information",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Employee_ProfileId = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        Primary_Position = c.Boolean(nullable: false),
                        job_descId = c.String(),
                        SlotdescId = c.String(),
                        Default_location_descId = c.String(),
                        Location_descId = c.String(),
                        Job_level_gradeId = c.String(),
                        From_date = c.DateTime(nullable: false),
                        To_date = c.DateTime(nullable: false),
                        Years = c.Int(nullable: false),
                        Months = c.Int(nullable: false),
                        End_of_service_date = c.DateTime(nullable: false),
                        Last_working_date = c.DateTime(nullable: false),
                        Commnets = c.String(),
                        working_system = c.Int(nullable: false),
                        Position_status = c.Int(nullable: false),
                        EOS_reasons = c.Int(nullable: false),
                        Job_level_grade_ID = c.Int(),
                        job_title_cards_ID = c.Int(),
                        work_location_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Job_level_grade", t => t.Job_level_grade_ID)
                .ForeignKey("dbo.job_title_cards", t => t.job_title_cards_ID)
                .ForeignKey("dbo.work_location", t => t.work_location_ID)
                .Index(t => t.Job_level_grade_ID)
                .Index(t => t.job_title_cards_ID)
                .Index(t => t.work_location_ID);
            
            CreateTable(
                "dbo.Job_level_grade",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        max_monthly_allowance = c.Double(nullable: false),
                        min_basic_salary = c.Double(nullable: false),
                        mid_basic_salary = c.Double(nullable: false),
                        max_basic_salary = c.Double(nullable: false),
                        min_working_years = c.Double(nullable: false),
                        max_incentive_amount = c.Double(nullable: false),
                        max_incentive_percentage = c.Double(nullable: false),
                        dedicated_allowence = c.Double(nullable: false),
                        max_annual_increase_percentage = c.Double(nullable: false),
                        representation_allowance_value = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.job_title_cards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        name = c.String(nullable: false),
                        Description = c.String(),
                        job_Summery = c.String(),
                        Job_alt_summery = c.String(),
                        from_age = c.Int(nullable: false),
                        to_age = c.Int(nullable: false),
                        num_slots = c.Int(nullable: false),
                        parent_job = c.String(),
                        joblevelsetupID = c.String(nullable: false),
                        Default_job_title_sub_classID = c.String(nullable: false),
                        nationalityID = c.String(nullable: false),
                        gender = c.Int(nullable: false),
                        working_system = c.Int(nullable: false),
                        check_status = c.Int(nullable: false),
                        validity = c.Int(nullable: false),
                        parment = c.Int(nullable: false),
                        Job_DetailsID = c.String(),
                        number_hired = c.Int(nullable: false),
                        number_vacant = c.Int(nullable: false),
                        Organization_unit_codeID = c.String(nullable: false),
                        Job_Details_ID = c.Int(),
                        job_level_setup_ID = c.Int(),
                        Nationality_ID = c.Int(),
                        Organization_Chart_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Job_Details", t => t.Job_Details_ID)
                .ForeignKey("dbo.job_level_setup", t => t.job_level_setup_ID)
                .ForeignKey("dbo.Nationalities", t => t.Nationality_ID)
                .ForeignKey("dbo.Organization_Chart", t => t.Organization_Chart_ID)
                .Index(t => t.Job_Details_ID)
                .Index(t => t.job_level_setup_ID)
                .Index(t => t.Nationality_ID)
                .Index(t => t.Organization_Chart_ID);
            
            CreateTable(
                "dbo.Job_Details",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.qulification_job",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name_of_educational_qualificationID = c.String(),
                        GradeEducateID = c.String(),
                        Qualification_MajorID = c.String(),
                        required = c.Boolean(nullable: false),
                        GradeEducate_ID = c.Int(),
                        Name_of_educational_qualification_ID = c.Int(),
                        Qualification_Major_ID = c.Int(),
                        Job_Details_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.GradeEducates", t => t.GradeEducate_ID)
                .ForeignKey("dbo.Name_of_educational_qualification", t => t.Name_of_educational_qualification_ID)
                .ForeignKey("dbo.Qualification_Major", t => t.Qualification_Major_ID)
                .ForeignKey("dbo.Job_Details", t => t.Job_Details_ID)
                .Index(t => t.GradeEducate_ID)
                .Index(t => t.Name_of_educational_qualification_ID)
                .Index(t => t.Qualification_Major_ID)
                .Index(t => t.Job_Details_ID);
            
            CreateTable(
                "dbo.GradeEducates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.Name_of_educational_qualification",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Educate_Titleid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Educate_Title", t => t.Educate_Titleid, cascadeDelete: true)
                .Index(t => t.Code, unique: true)
                .Index(t => t.Educate_Titleid);
            
            CreateTable(
                "dbo.Educate_Title",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.Qualification_Major",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Name_of_educational_qualificationid = c.Int(nullable: false),
                        Educate_Titleid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Name_of_educational_qualification", t => t.Name_of_educational_qualificationid, cascadeDelete: true)
                .Index(t => t.Code, unique: true)
                .Index(t => t.Name_of_educational_qualificationid);
            
            CreateTable(
                "dbo.Requirments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Job_Details_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Job_Details", t => t.Job_Details_ID)
                .Index(t => t.Job_Details_ID);
            
            CreateTable(
                "dbo.Responsibilities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Job_Details_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Job_Details", t => t.Job_Details_ID)
                .Index(t => t.Job_Details_ID);
            
            CreateTable(
                "dbo.Risks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Risks_TypeId = c.String(nullable: false),
                        Risks_Type_ID = c.Int(),
                        Job_Details_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Risks_Type", t => t.Risks_Type_ID)
                .ForeignKey("dbo.Job_Details", t => t.Job_Details_ID)
                .Index(t => t.Risks_Type_ID)
                .Index(t => t.Job_Details_ID);
            
            CreateTable(
                "dbo.Risks_Type",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.skills_job",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        skillid = c.String(),
                        required = c.Boolean(nullable: false),
                        skill_ID = c.Int(),
                        Job_Details_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Skills", t => t.skill_ID)
                .ForeignKey("dbo.Job_Details", t => t.Job_Details_ID)
                .Index(t => t.skill_ID)
                .Index(t => t.Job_Details_ID);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Skill_groupId = c.String(nullable: false),
                        Skill_group_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Skill_group", t => t.Skill_group_ID)
                .Index(t => t.Skill_group_ID);
            
            CreateTable(
                "dbo.Skill_group",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.job_level_setup",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        max_monthly_allowance = c.Double(nullable: false),
                        min_basic_salary = c.Double(nullable: false),
                        mid_basic_salary = c.Double(nullable: false),
                        max_basic_salary = c.Double(nullable: false),
                        min_working_years = c.Double(nullable: false),
                        max_incentive_amount = c.Double(nullable: false),
                        max_incentive_percentage = c.Double(nullable: false),
                        dedicated_allowence = c.Double(nullable: false),
                        max_annual_increase_percentage = c.Double(nullable: false),
                        representation_allowance_value = c.Double(nullable: false),
                        Sequence_number = c.Double(nullable: false),
                        Calculate_added_military_years = c.Boolean(nullable: false),
                        Calculate_extra_qualification_years = c.Boolean(nullable: false),
                        Calculate_added_experience_years = c.Boolean(nullable: false),
                        Notes = c.String(),
                        Job_level_class__ID = c.String(nullable: false),
                        Job_level_gradeI__D = c.String(nullable: false),
                        report_job_levelID = c.String(),
                        Job_level_class_ID = c.Int(),
                        Job_level_grade_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Job_level_class", t => t.Job_level_class_ID)
                .ForeignKey("dbo.Job_level_grade", t => t.Job_level_grade_ID)
                .Index(t => t.Job_level_class_ID)
                .Index(t => t.Job_level_grade_ID);
            
            CreateTable(
                "dbo.Job_level_class",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        max_monthly_allowance = c.Double(nullable: false),
                        min_basic_salary = c.Double(nullable: false),
                        mid_basic_salary = c.Double(nullable: false),
                        max_basic_salary = c.Double(nullable: false),
                        min_working_years = c.Double(nullable: false),
                        max_incentive_amount = c.Double(nullable: false),
                        max_incentive_percentage = c.Double(nullable: false),
                        dedicated_allowence = c.Double(nullable: false),
                        max_annual_increase_percentage = c.Double(nullable: false),
                        representation_allowance_value = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.job_level_education",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        number_of_years_requires = c.Double(nullable: false),
                        Educate_Title_ID = c.Int(),
                        job_level_setup_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Educate_Title", t => t.Educate_Title_ID)
                .ForeignKey("dbo.job_level_setup", t => t.job_level_setup_ID)
                .Index(t => t.Educate_Title_ID)
                .Index(t => t.job_level_setup_ID);
            
            CreateTable(
                "dbo.Organization_Unit_Type",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        unitLevelcode = c.String(),
                        unitschemacode = c.String(),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Organization_Unit_Level_ID = c.Int(),
                        Organization_Unit_Schema_ID = c.Int(),
                        job_level_setup_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organization_Unit_Level", t => t.Organization_Unit_Level_ID)
                .ForeignKey("dbo.Organization_Unit_Schema", t => t.Organization_Unit_Schema_ID)
                .ForeignKey("dbo.job_level_setup", t => t.job_level_setup_ID)
                .Index(t => t.Organization_Unit_Level_ID)
                .Index(t => t.Organization_Unit_Schema_ID)
                .Index(t => t.job_level_setup_ID);
            
            CreateTable(
                "dbo.Organization_Unit_Level",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Level_Number = c.Double(nullable: false),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Organization_Unit_Schema",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        color = c.String(),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Job_title_sub_class",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        JOB_TYPE_ALLWANCE_PERCENTAGE = c.Single(nullable: false),
                        Dedicated_ALLWANCE_VALUE = c.Single(nullable: false),
                        Exchanging_ALLWANCE_VALUE = c.Single(nullable: false),
                        Job_title_classId = c.String(nullable: false),
                        Job_title_class_ID = c.Int(),
                        job_title_cards_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Job_title_class", t => t.Job_title_class_ID)
                .ForeignKey("dbo.job_title_cards", t => t.job_title_cards_ID)
                .Index(t => t.Job_title_class_ID)
                .Index(t => t.job_title_cards_ID);
            
            CreateTable(
                "dbo.Job_title_class",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Nationalities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.Organization_Chart",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        unit_Description = c.String(nullable: false),
                        alter_unit_Description = c.String(),
                        User_unit_code = c.String(),
                        unit_type_codeID = c.Int(nullable: false),
                        unit_status = c.Int(nullable: false),
                        number_of_direct_positions = c.Int(nullable: false),
                        unit_mail = c.String(),
                        parent = c.String(nullable: false),
                        master_node = c.String(),
                        refrence_number = c.Int(nullable: false),
                        worklocationid = c.String(nullable: false),
                        Organization_Chart_ID = c.Int(),
                        work_location_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organization_Chart", t => t.Organization_Chart_ID)
                .ForeignKey("dbo.Organization_Unit_Type", t => t.unit_type_codeID, cascadeDelete: true)
                .ForeignKey("dbo.work_location", t => t.work_location_ID)
                .Index(t => t.unit_type_codeID)
                .Index(t => t.Organization_Chart_ID)
                .Index(t => t.work_location_ID);
            
            CreateTable(
                "dbo.work_location",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Location_name = c.String(),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Slots",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        slot_code = c.String(),
                        slot_description = c.String(),
                        joblevelsetupID = c.String(),
                        check_status = c.Int(nullable: false),
                        slot_type = c.Int(nullable: false),
                        EmployeeID = c.String(),
                        EmployeeName = c.String(),
                        job_level_setup_ID = c.Int(),
                        job_title_cards_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.job_level_setup", t => t.job_level_setup_ID)
                .ForeignKey("dbo.job_title_cards", t => t.job_title_cards_ID)
                .Index(t => t.job_level_setup_ID)
                .Index(t => t.job_title_cards_ID);
            
            CreateTable(
                "dbo.Employee_Qualification_Profile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Employee_ProfileId = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        Related_to_job = c.Boolean(nullable: false),
                        Qualification_start_date = c.DateTime(nullable: false),
                        Qualification_end_date = c.DateTime(nullable: false),
                        Years = c.Int(nullable: false),
                        Months = c.Int(nullable: false),
                        Educate_categoryId = c.String(),
                        Educate_TitleId = c.String(),
                        Main_Educate_bodyId = c.String(),
                        Sub_educational_bodyId = c.String(),
                        Name_of_educational_qualificationId = c.String(),
                        Qualification_MajorId = c.String(),
                        GradeEducateId = c.String(),
                        Extra_education_years = c.Int(nullable: false),
                        Allowance_value = c.Double(nullable: false),
                        Educate_category_ID = c.Int(),
                        Educate_Title_ID = c.Int(),
                        GradeEducate_ID = c.Int(),
                        Main_Educate_body_ID = c.Int(),
                        Name_of_educational_qualification_ID = c.Int(),
                        Qualification_Major_ID = c.Int(),
                        Sub_educational_body_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Educate_category", t => t.Educate_category_ID)
                .ForeignKey("dbo.Educate_Title", t => t.Educate_Title_ID)
                .ForeignKey("dbo.GradeEducates", t => t.GradeEducate_ID)
                .ForeignKey("dbo.Main_Educate_body", t => t.Main_Educate_body_ID)
                .ForeignKey("dbo.Name_of_educational_qualification", t => t.Name_of_educational_qualification_ID)
                .ForeignKey("dbo.Qualification_Major", t => t.Qualification_Major_ID)
                .ForeignKey("dbo.Sub_educational_body", t => t.Sub_educational_body_ID)
                .Index(t => t.Educate_category_ID)
                .Index(t => t.Educate_Title_ID)
                .Index(t => t.GradeEducate_ID)
                .Index(t => t.Main_Educate_body_ID)
                .Index(t => t.Name_of_educational_qualification_ID)
                .Index(t => t.Qualification_Major_ID)
                .Index(t => t.Sub_educational_body_ID);
            
            CreateTable(
                "dbo.Educate_category",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.Main_Educate_body",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.Sub_educational_body",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Main_Educate_bodyid = c.Int(nullable: false),
                        Name_of_educational_qualification_IDD = c.String(),
                        Name_of_educational_qualification_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Main_Educate_body", t => t.Main_Educate_bodyid, cascadeDelete: true)
                .ForeignKey("dbo.Name_of_educational_qualification", t => t.Name_of_educational_qualification_ID)
                .Index(t => t.Code, unique: true)
                .Index(t => t.Main_Educate_bodyid)
                .Index(t => t.Name_of_educational_qualification_ID);
            
            CreateTable(
                "dbo.Personnel_Information",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Main_Status = c.Int(nullable: false),
                        Sub_Status = c.Int(nullable: false),
                        Hire_Date = c.DateTime(nullable: false),
                        Sector_Join_Date = c.DateTime(nullable: false),
                        Join_Date = c.DateTime(nullable: false),
                        Pratice_profession_Out_Of_Company = c.Boolean(nullable: false),
                        Social_Insurance = c.String(),
                        Social_Insurance_Date = c.DateTime(nullable: false),
                        Service_period_ins_Y = c.Int(nullable: false),
                        Service_period_ins_M = c.Int(nullable: false),
                        Boarding_membership = c.Int(nullable: false),
                        Boarding_Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Religions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.Service_Information",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Pension = c.String(),
                        Pension_source = c.String(),
                        EOS_date = c.DateTime(nullable: false),
                        Have_pension = c.Boolean(nullable: false),
                        Retired_expected_EOS = c.DateTime(nullable: false),
                        Last_working_date = c.DateTime(nullable: false),
                        CurrencyId = c.String(),
                        Is_merits_The_date_of_death = c.Boolean(nullable: false),
                        Currency_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Currencies", t => t.Currency_ID)
                .Index(t => t.Currency_ID);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Native = c.Boolean(nullable: false),
                        symbol = c.String(),
                        Nmmber_of_decimal_places = c.Int(nullable: false),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EOS_Interview_Questions_Groups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Questions_Group_Code = c.String(nullable: false),
                        Description_of = c.String(nullable: false),
                        Description_Alternative = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Definition_of_EOS_Interview_Questions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Question_Code = c.String(nullable: false),
                        Question_Description = c.String(nullable: false),
                        Description = c.String(),
                        Question_GroupId = c.String(nullable: false),
                        EOS_Interview_Questions_Groups_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EOS_Interview_Questions_Groups", t => t.EOS_Interview_Questions_Groups_ID)
                .Index(t => t.EOS_Interview_Questions_Groups_ID);
            
            CreateTable(
                "dbo.status",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        statu = c.Int(nullable: false),
                        created_by = c.String(),
                        report_as_ready_by = c.String(),
                        approved_by = c.String(),
                        Rejected_by = c.String(),
                        cancaled_by = c.String(),
                        created_bydate = c.DateTime(nullable: false),
                        report_as_ready_bydate = c.DateTime(nullable: false),
                        approved_bydate = c.DateTime(nullable: false),
                        Rejected_bydate = c.DateTime(nullable: false),
                        cancaled_bydate = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Authorities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Skill_Description = c.String(),
                        Authority_TypeId = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Authority_Type_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Authority_Type", t => t.Authority_Type_ID)
                .Index(t => t.Authority_Type_ID);
            
            CreateTable(
                "dbo.Authority_Type",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Budgets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Year_From = c.Int(nullable: false),
                        Year_To = c.Int(nullable: false),
                        Organization_unitid = c.String(),
                        budget_type = c.Int(nullable: false),
                        Currency_rate = c.Double(nullable: false),
                        amount_native = c.Double(nullable: false),
                        ammount_forigne = c.Double(nullable: false),
                        sign_native = c.String(),
                        sign_forign = c.String(),
                        Remaining_native = c.Double(nullable: false),
                        Remaining_forgin = c.Double(nullable: false),
                        justification_ID = c.Int(),
                        organization_unit_ID = c.Int(),
                        status_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.justifications", t => t.justification_ID)
                .ForeignKey("dbo.Organization_Chart", t => t.organization_unit_ID)
                .ForeignKey("dbo.status", t => t.status_ID)
                .Index(t => t.justification_ID)
                .Index(t => t.organization_unit_ID)
                .Index(t => t.status_ID);
            
            CreateTable(
                "dbo.Budget_details",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        amount_native = c.Double(nullable: false),
                        amount_forign = c.Double(nullable: false),
                        comment = c.String(),
                        sign_native = c.String(),
                        sign_forgin = c.String(),
                        Budget_class_items_ID = c.Int(),
                        Budget_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Budget_class_items", t => t.Budget_class_items_ID)
                .ForeignKey("dbo.Budgets", t => t.Budget_ID)
                .Index(t => t.Budget_class_items_ID)
                .Index(t => t.Budget_ID);
            
            CreateTable(
                "dbo.Budget_class_items",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Budget_classId = c.String(nullable: false),
                        Budget_class_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Budget_class", t => t.Budget_class_ID)
                .Index(t => t.Budget_class_ID);
            
            CreateTable(
                "dbo.Budget_class",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.justifications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        justifiaction = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Check_List_Items",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Required_on_application = c.Boolean(nullable: false),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.check_Request",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Request_number = c.String(nullable: false),
                        Request_date = c.DateTime(nullable: false),
                        check_number = c.String(nullable: false),
                        month = c.Int(nullable: false),
                        year = c.Int(nullable: false),
                        amount = c.Double(nullable: false),
                        Check_Due_date = c.DateTime(nullable: false),
                        Description = c.String(),
                        Comment = c.String(),
                        check_request_change_statusID = c.String(nullable: false),
                        ChecktypeID = c.String(nullable: false),
                        date = c.String(),
                        check_request_change_status_ID = c.Int(),
                        Checktype_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.check_request_change_status", t => t.check_request_change_status_ID)
                .ForeignKey("dbo.Checktypes", t => t.Checktype_ID)
                .Index(t => t.check_request_change_status_ID)
                .Index(t => t.Checktype_ID);
            
            CreateTable(
                "dbo.check_request_change_status",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(),
                        check_request_statusID = c.String(nullable: false),
                        check_Request_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.check_request_status", t => t.check_Request_ID)
                .Index(t => t.check_Request_ID);
            
            CreateTable(
                "dbo.check_request_status",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        status = c.Int(nullable: false),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Checktypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Directions = c.String(),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Committe_subjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Contact_methods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Contact_type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.Contract_Type",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Contract_Type_Code = c.String(nullable: false),
                        Contract_Type_Description = c.String(nullable: false),
                        Contract_Type_Alternative_Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Diseases",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Disease_Code = c.Double(nullable: false),
                        Disease_Name = c.String(nullable: false),
                        Disease_TName = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Disease_Code, unique: true);
            
            CreateTable(
                "dbo.Document_Group",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Document_Groupid = c.String(),
                        Document_Group_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Document_Group", t => t.Document_Group_ID)
                .Index(t => t.Code, unique: true)
                .Index(t => t.Document_Group_ID);
            
            CreateTable(
                "dbo.Employee_Profile_Groups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Group_Code = c.String(nullable: false),
                        Group_Description = c.String(nullable: false),
                        Group_Alternative_Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Employee_Recodes_Group",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Record_Group_Code = c.String(nullable: false),
                        Record_Group_Description = c.String(nullable: false),
                        Record_Group_Alternative = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Employee_records",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Record_date = c.DateTime(nullable: false),
                        Effictive_date = c.DateTime(nullable: false),
                        Record_expire_date = c.DateTime(nullable: false),
                        record_value = c.Double(nullable: false),
                        record_amount = c.Double(nullable: false),
                        check_status = c.Int(nullable: false),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Employee_Profile_ID = c.Int(),
                        Employee_Recodes_Group_ID = c.Int(),
                        status_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employee_Profile", t => t.Employee_Profile_ID)
                .ForeignKey("dbo.Employee_Recodes_Group", t => t.Employee_Recodes_Group_ID)
                .ForeignKey("dbo.status", t => t.status_ID)
                .Index(t => t.Employee_Profile_ID)
                .Index(t => t.Employee_Recodes_Group_ID)
                .Index(t => t.status_ID);
            
            CreateTable(
                "dbo.Exchange_Rate",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        CurrencyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Currencies", t => t.CurrencyID, cascadeDelete: true)
                .Index(t => t.CurrencyID);
            
            CreateTable(
                "dbo.months",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        value = c.Single(nullable: false),
                        Exchange_Rate_ID = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Exchange_Rate", t => t.Exchange_Rate_ID)
                .Index(t => t.Exchange_Rate_ID);
            
            CreateTable(
                "dbo.Experience_group",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.External_compaines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        oil_sector = c.Boolean(nullable: false),
                        address = c.String(),
                        Company_type = c.Int(),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FileDetailsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        FileContent = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        filename = c.String(),
                        File = c.Binary(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Medical_Contributions_Allocations_Entry",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        TName = c.String(),
                        Is_Contribution = c.Boolean(nullable: false),
                        Allowed_To = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.Medical_Contributions_Allocations_Services",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Medical_Contributions_Allocations_EntryId = c.String(nullable: false),
                        Classification_CodeId = c.String(nullable: false),
                        Service_CodeId = c.String(nullable: false),
                        Medical_Contributions_Allocations_Entry_ID = c.Int(),
                        Medical_Service_ID = c.Int(),
                        Medical_Service_Classification_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Medical_Contributions_Allocations_Entry", t => t.Medical_Contributions_Allocations_Entry_ID)
                .ForeignKey("dbo.Medical_Service", t => t.Medical_Service_ID)
                .ForeignKey("dbo.Medical_Service_Classification", t => t.Medical_Service_Classification_ID)
                .Index(t => t.Medical_Contributions_Allocations_Entry_ID)
                .Index(t => t.Medical_Service_ID)
                .Index(t => t.Medical_Service_Classification_ID);
            
            CreateTable(
                "dbo.Medical_Service",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Service_Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        TName = c.String(),
                        Medical_Service_ClassificationId = c.String(nullable: false),
                        Medical_Service_Classification_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Medical_Service_Classification", t => t.Medical_Service_Classification_ID)
                .Index(t => t.Service_Code, unique: true)
                .Index(t => t.Medical_Service_Classification_ID);
            
            CreateTable(
                "dbo.Medical_Service_Classification",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Classification_Code = c.Double(nullable: false),
                        Description = c.String(nullable: false),
                        TDescription = c.String(),
                        Group_Medical_Service_GroupId = c.String(nullable: false),
                        Medical_Service_Group_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Medical_Service_Group", t => t.Medical_Service_Group_ID)
                .Index(t => t.Classification_Code, unique: true)
                .Index(t => t.Medical_Service_Group_ID);
            
            CreateTable(
                "dbo.Medical_Service_Group",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Group_Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        TName = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Group_Code, unique: true);
            
            CreateTable(
                "dbo.Medical_Doctors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Doctor_Name = c.String(nullable: false),
                        Doctor_TName = c.String(nullable: false),
                        Notes = c.String(),
                        Medical_Doctors_LevelId = c.String(nullable: false),
                        Medical_Doctors_Level_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Medical_Doctors_Level", t => t.Medical_Doctors_Level_ID)
                .Index(t => t.Medical_Doctors_Level_ID);
            
            CreateTable(
                "dbo.Medical_Doctors_Level",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Level_Code = c.Double(nullable: false),
                        Level_Name = c.String(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Level_Code, unique: true);
            
            CreateTable(
                "dbo.Medical_Medicine_Classfication",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.Medicines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Is_Foreign = c.Boolean(nullable: false),
                        price = c.Double(nullable: false),
                        Indication = c.String(),
                        Usual_Dosage = c.String(),
                        Contraindication = c.String(),
                        Precaution_Warnings = c.String(),
                        Note = c.String(),
                        Medical_Medicine_ClassficationId = c.String(),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Medical_Medicine_Classfication_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Medical_Medicine_Classfication", t => t.Medical_Medicine_Classfication_ID)
                .Index(t => t.Medical_Medicine_Classfication_ID);
            
            CreateTable(
                "dbo.Military_Service_Rank",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.Military_Service_Status",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.Position_Transaction_Information",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Position_transaction_no = c.String(),
                        Position_transaction = c.DateTime(nullable: false),
                        Transaction_Type = c.Int(nullable: false),
                        Fixed_basic_salary_by = c.Int(nullable: false),
                        Resolution_number = c.String(),
                        Resolution_date = c.DateTime(nullable: false),
                        Activity_number = c.String(),
                        Memo_number = c.String(),
                        Memo_date = c.DateTime(nullable: false),
                        Recommended_by = c.String(),
                        Approved_by = c.String(),
                        Approved_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Question = c.String(),
                        Standart_Question = c.String(),
                        Filesid = c.String(),
                        Files_id = c.Int(),
                        Test_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Files", t => t.Files_id)
                .ForeignKey("dbo.Tests", t => t.Test_ID)
                .Index(t => t.Files_id)
                .Index(t => t.Test_ID);
            
            CreateTable(
                "dbo.Rejection_Reasons",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        purpose = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Sings_Symptom",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Sing_Code = c.Double(nullable: false),
                        Sing_Name = c.String(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Sing_Code, unique: true);
            
            CreateTable(
                "dbo.Special_Allwonce_History",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Year = c.Double(nullable: false),
                        Month = c.Double(nullable: false),
                        Percentage = c.Double(nullable: false),
                        Allowance_amount = c.Double(nullable: false),
                        pervious_basic = c.Double(nullable: false),
                        new_basic_sallary = c.Double(nullable: false),
                        type_allowance = c.Int(nullable: false),
                        selectedID = c.Int(nullable: false),
                        Job_level_class_ID = c.Int(),
                        Job_level_grade_ID = c.Int(),
                        job_level_setup_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Job_level_class", t => t.Job_level_class_ID)
                .ForeignKey("dbo.Job_level_grade", t => t.Job_level_grade_ID)
                .ForeignKey("dbo.job_level_setup", t => t.job_level_setup_ID)
                .Index(t => t.Job_level_class_ID)
                .Index(t => t.Job_level_grade_ID)
                .Index(t => t.job_level_setup_ID);
            
            CreateTable(
                "dbo.Sponsors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        corporation = c.String(),
                        sponsorid = c.String(),
                        IBAN = c.String(),
                        Others1 = c.String(),
                        Others2 = c.String(),
                        Others3 = c.String(),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StructureModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Structure_Code = c.String(nullable: false),
                        All_Models = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.All_Models, unique: true);
            
            CreateTable(
                "dbo.Subscription_Syndicate",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Server_Legatees = c.Boolean(nullable: false),
                        Subscription_Code = c.String(nullable: false),
                        Subscription_Description = c.String(nullable: false),
                        Subscription_Alternative_Description = c.String(),
                        Contact_Detail = c.String(),
                        Default_Subscription_Fees = c.Int(nullable: false),
                        Deduction_Period = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Phone_1 = c.String(),
                        Phone_2 = c.String(),
                        Fax = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Contact_methodsId = c.String(nullable: false),
                        Contact_methods_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Contact_methods", t => t.Contact_methods_ID)
                .Index(t => t.Contact_methods_ID);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Test_type = c.Int(nullable: false),
                        Pass_mark = c.Double(nullable: false),
                        Full_mark = c.Double(nullable: false),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TicketPrices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        classtype = c.Int(nullable: false),
                        From_air_port_Idd = c.Int(nullable: false),
                        To_air_port_Idd = c.Int(nullable: false),
                        From_Date = c.DateTime(nullable: false),
                        TO_Date = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        From_Air_port_ID = c.Int(),
                        To_Air_port_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Air_ports", t => t.From_Air_port_ID)
                .ForeignKey("dbo.Air_ports", t => t.To_Air_port_ID)
                .Index(t => t.From_Air_port_ID)
                .Index(t => t.To_Air_port_ID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ImagePath = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketPrices", "To_Air_port_ID", "dbo.Air_ports");
            DropForeignKey("dbo.TicketPrices", "From_Air_port_ID", "dbo.Air_ports");
            DropForeignKey("dbo.Questions", "Test_ID", "dbo.Tests");
            DropForeignKey("dbo.Subscription_Syndicate", "Contact_methods_ID", "dbo.Contact_methods");
            DropForeignKey("dbo.Special_Allwonce_History", "job_level_setup_ID", "dbo.job_level_setup");
            DropForeignKey("dbo.Special_Allwonce_History", "Job_level_grade_ID", "dbo.Job_level_grade");
            DropForeignKey("dbo.Special_Allwonce_History", "Job_level_class_ID", "dbo.Job_level_class");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Questions", "Files_id", "dbo.Files");
            DropForeignKey("dbo.Medicines", "Medical_Medicine_Classfication_ID", "dbo.Medical_Medicine_Classfication");
            DropForeignKey("dbo.Medical_Doctors", "Medical_Doctors_Level_ID", "dbo.Medical_Doctors_Level");
            DropForeignKey("dbo.Medical_Contributions_Allocations_Services", "Medical_Service_Classification_ID", "dbo.Medical_Service_Classification");
            DropForeignKey("dbo.Medical_Contributions_Allocations_Services", "Medical_Service_ID", "dbo.Medical_Service");
            DropForeignKey("dbo.Medical_Service", "Medical_Service_Classification_ID", "dbo.Medical_Service_Classification");
            DropForeignKey("dbo.Medical_Service_Classification", "Medical_Service_Group_ID", "dbo.Medical_Service_Group");
            DropForeignKey("dbo.Medical_Contributions_Allocations_Services", "Medical_Contributions_Allocations_Entry_ID", "dbo.Medical_Contributions_Allocations_Entry");
            DropForeignKey("dbo.months", "Exchange_Rate_ID", "dbo.Exchange_Rate");
            DropForeignKey("dbo.Exchange_Rate", "CurrencyID", "dbo.Currencies");
            DropForeignKey("dbo.Employee_records", "status_ID", "dbo.status");
            DropForeignKey("dbo.Employee_records", "Employee_Recodes_Group_ID", "dbo.Employee_Recodes_Group");
            DropForeignKey("dbo.Employee_records", "Employee_Profile_ID", "dbo.Employee_Profile");
            DropForeignKey("dbo.Documents", "Document_Group_ID", "dbo.Document_Group");
            DropForeignKey("dbo.check_Request", "Checktype_ID", "dbo.Checktypes");
            DropForeignKey("dbo.check_Request", "check_request_change_status_ID", "dbo.check_request_change_status");
            DropForeignKey("dbo.check_request_change_status", "check_Request_ID", "dbo.check_request_status");
            DropForeignKey("dbo.Budgets", "status_ID", "dbo.status");
            DropForeignKey("dbo.Budgets", "organization_unit_ID", "dbo.Organization_Chart");
            DropForeignKey("dbo.Budgets", "justification_ID", "dbo.justifications");
            DropForeignKey("dbo.Budget_details", "Budget_ID", "dbo.Budgets");
            DropForeignKey("dbo.Budget_details", "Budget_class_items_ID", "dbo.Budget_class_items");
            DropForeignKey("dbo.Budget_class_items", "Budget_class_ID", "dbo.Budget_class");
            DropForeignKey("dbo.Authorities", "Authority_Type_ID", "dbo.Authority_Type");
            DropForeignKey("dbo.Answer_EOS", "question_ID", "dbo.Definition_of_EOS_Interview_Questions");
            DropForeignKey("dbo.EOS_Request", "status_ID", "dbo.status");
            DropForeignKey("dbo.EOS_Request", "EOS_group_ID", "dbo.EOS_Interview_Questions_Groups");
            DropForeignKey("dbo.Definition_of_EOS_Interview_Questions", "EOS_Interview_Questions_Groups_ID", "dbo.EOS_Interview_Questions_Groups");
            DropForeignKey("dbo.EOS_Request", "Employee_ID", "dbo.Employee_Profile");
            DropForeignKey("dbo.Employee_Profile", "the_states_ID", "dbo.the_states");
            DropForeignKey("dbo.Employee_Profile", "Territories_ID", "dbo.Territories");
            DropForeignKey("dbo.Employee_Profile", "Service_Information_ID", "dbo.Service_Information");
            DropForeignKey("dbo.Service_Information", "Currency_ID", "dbo.Currencies");
            DropForeignKey("dbo.Employee_Profile", "Religion_ID", "dbo.Religions");
            DropForeignKey("dbo.Employee_Profile", "Personnel_Information_ID", "dbo.Personnel_Information");
            DropForeignKey("dbo.Employee_Profile", "Nationality_ID", "dbo.Nationalities");
            DropForeignKey("dbo.Employee_Profile", "Employee_Qualification_Profile_ID", "dbo.Employee_Qualification_Profile");
            DropForeignKey("dbo.Employee_Qualification_Profile", "Sub_educational_body_ID", "dbo.Sub_educational_body");
            DropForeignKey("dbo.Sub_educational_body", "Name_of_educational_qualification_ID", "dbo.Name_of_educational_qualification");
            DropForeignKey("dbo.Sub_educational_body", "Main_Educate_bodyid", "dbo.Main_Educate_body");
            DropForeignKey("dbo.Employee_Qualification_Profile", "Qualification_Major_ID", "dbo.Qualification_Major");
            DropForeignKey("dbo.Employee_Qualification_Profile", "Name_of_educational_qualification_ID", "dbo.Name_of_educational_qualification");
            DropForeignKey("dbo.Employee_Qualification_Profile", "Main_Educate_body_ID", "dbo.Main_Educate_body");
            DropForeignKey("dbo.Employee_Qualification_Profile", "GradeEducate_ID", "dbo.GradeEducates");
            DropForeignKey("dbo.Employee_Qualification_Profile", "Educate_Title_ID", "dbo.Educate_Title");
            DropForeignKey("dbo.Employee_Qualification_Profile", "Educate_category_ID", "dbo.Educate_category");
            DropForeignKey("dbo.Employee_Profile", "Employee_Positions_Profile_ID", "dbo.Position_Information");
            DropForeignKey("dbo.Position_Information", "work_location_ID", "dbo.work_location");
            DropForeignKey("dbo.Position_Information", "job_title_cards_ID", "dbo.job_title_cards");
            DropForeignKey("dbo.Slots", "job_title_cards_ID", "dbo.job_title_cards");
            DropForeignKey("dbo.Slots", "job_level_setup_ID", "dbo.job_level_setup");
            DropForeignKey("dbo.job_title_cards", "Organization_Chart_ID", "dbo.Organization_Chart");
            DropForeignKey("dbo.Organization_Chart", "work_location_ID", "dbo.work_location");
            DropForeignKey("dbo.Organization_Chart", "unit_type_codeID", "dbo.Organization_Unit_Type");
            DropForeignKey("dbo.Organization_Chart", "Organization_Chart_ID", "dbo.Organization_Chart");
            DropForeignKey("dbo.job_title_cards", "Nationality_ID", "dbo.Nationalities");
            DropForeignKey("dbo.Job_title_sub_class", "job_title_cards_ID", "dbo.job_title_cards");
            DropForeignKey("dbo.Job_title_sub_class", "Job_title_class_ID", "dbo.Job_title_class");
            DropForeignKey("dbo.job_title_cards", "job_level_setup_ID", "dbo.job_level_setup");
            DropForeignKey("dbo.Organization_Unit_Type", "job_level_setup_ID", "dbo.job_level_setup");
            DropForeignKey("dbo.Organization_Unit_Type", "Organization_Unit_Schema_ID", "dbo.Organization_Unit_Schema");
            DropForeignKey("dbo.Organization_Unit_Type", "Organization_Unit_Level_ID", "dbo.Organization_Unit_Level");
            DropForeignKey("dbo.job_level_setup", "Job_level_grade_ID", "dbo.Job_level_grade");
            DropForeignKey("dbo.job_level_education", "job_level_setup_ID", "dbo.job_level_setup");
            DropForeignKey("dbo.job_level_education", "Educate_Title_ID", "dbo.Educate_Title");
            DropForeignKey("dbo.job_level_setup", "Job_level_class_ID", "dbo.Job_level_class");
            DropForeignKey("dbo.job_title_cards", "Job_Details_ID", "dbo.Job_Details");
            DropForeignKey("dbo.skills_job", "Job_Details_ID", "dbo.Job_Details");
            DropForeignKey("dbo.skills_job", "skill_ID", "dbo.Skills");
            DropForeignKey("dbo.Skills", "Skill_group_ID", "dbo.Skill_group");
            DropForeignKey("dbo.Risks", "Job_Details_ID", "dbo.Job_Details");
            DropForeignKey("dbo.Risks", "Risks_Type_ID", "dbo.Risks_Type");
            DropForeignKey("dbo.Responsibilities", "Job_Details_ID", "dbo.Job_Details");
            DropForeignKey("dbo.Requirments", "Job_Details_ID", "dbo.Job_Details");
            DropForeignKey("dbo.qulification_job", "Job_Details_ID", "dbo.Job_Details");
            DropForeignKey("dbo.qulification_job", "Qualification_Major_ID", "dbo.Qualification_Major");
            DropForeignKey("dbo.Qualification_Major", "Name_of_educational_qualificationid", "dbo.Name_of_educational_qualification");
            DropForeignKey("dbo.qulification_job", "Name_of_educational_qualification_ID", "dbo.Name_of_educational_qualification");
            DropForeignKey("dbo.Name_of_educational_qualification", "Educate_Titleid", "dbo.Educate_Title");
            DropForeignKey("dbo.qulification_job", "GradeEducate_ID", "dbo.GradeEducates");
            DropForeignKey("dbo.Position_Information", "Job_level_grade_ID", "dbo.Job_level_grade");
            DropForeignKey("dbo.Employee_Profile", "Employee_Address_Profile_ID", "dbo.Employee_Address_Profile");
            DropForeignKey("dbo.Employee_Address_Profile", "the_states_ID", "dbo.the_states");
            DropForeignKey("dbo.Employee_Address_Profile", "Territories_ID", "dbo.Territories");
            DropForeignKey("dbo.Employee_Address_Profile", "postcode_ID", "dbo.postcodes");
            DropForeignKey("dbo.postcodes", "cities_ID", "dbo.cities");
            DropForeignKey("dbo.Employee_Address_Profile", "Country_ID", "dbo.Countries");
            DropForeignKey("dbo.Employee_Address_Profile", "cities_ID", "dbo.cities");
            DropForeignKey("dbo.Employee_Address_Profile", "Area_ID", "dbo.Areas");
            DropForeignKey("dbo.Employee_Profile", "Country_ID", "dbo.Countries");
            DropForeignKey("dbo.Employee_Profile", "cities_ID", "dbo.cities");
            DropForeignKey("dbo.cities", "Territories_ID", "dbo.Territories");
            DropForeignKey("dbo.Territories", "the_states_ID", "dbo.the_states");
            DropForeignKey("dbo.Employee_Profile", "Area_ID", "dbo.Areas");
            DropForeignKey("dbo.Employee_Profile", "Ability_ID", "dbo.Abilities");
            DropForeignKey("dbo.EOS_Request", "Check_List_Item_Groups_ID", "dbo.Check_List_Item_Groups");
            DropForeignKey("dbo.check_list_EOS", "item_ID", "dbo.Check_Lists_Items");
            DropForeignKey("dbo.Check_Lists_Items", "Check_List_Item_Groups_ID", "dbo.Check_List_Item_Groups");
            DropForeignKey("dbo.check_list_EOS", "EOS_ID", "dbo.EOS_Request");
            DropForeignKey("dbo.Answer_EOS", "EOS_ID", "dbo.EOS_Request");
            DropForeignKey("dbo.Air_ports", "the_states_ID", "dbo.the_states");
            DropForeignKey("dbo.the_states", "Area_ID", "dbo.Areas");
            DropForeignKey("dbo.Areas", "Country_ID", "dbo.Countries");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.TicketPrices", new[] { "To_Air_port_ID" });
            DropIndex("dbo.TicketPrices", new[] { "From_Air_port_ID" });
            DropIndex("dbo.Subscription_Syndicate", new[] { "Contact_methods_ID" });
            DropIndex("dbo.StructureModels", new[] { "All_Models" });
            DropIndex("dbo.Special_Allwonce_History", new[] { "job_level_setup_ID" });
            DropIndex("dbo.Special_Allwonce_History", new[] { "Job_level_grade_ID" });
            DropIndex("dbo.Special_Allwonce_History", new[] { "Job_level_class_ID" });
            DropIndex("dbo.Sings_Symptom", new[] { "Sing_Code" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Rejection_Reasons", new[] { "Code" });
            DropIndex("dbo.Questions", new[] { "Test_ID" });
            DropIndex("dbo.Questions", new[] { "Files_id" });
            DropIndex("dbo.Military_Service_Status", new[] { "Code" });
            DropIndex("dbo.Military_Service_Rank", new[] { "Code" });
            DropIndex("dbo.Medicines", new[] { "Medical_Medicine_Classfication_ID" });
            DropIndex("dbo.Medical_Medicine_Classfication", new[] { "Code" });
            DropIndex("dbo.Medical_Doctors_Level", new[] { "Level_Code" });
            DropIndex("dbo.Medical_Doctors", new[] { "Medical_Doctors_Level_ID" });
            DropIndex("dbo.Medical_Service_Group", new[] { "Group_Code" });
            DropIndex("dbo.Medical_Service_Classification", new[] { "Medical_Service_Group_ID" });
            DropIndex("dbo.Medical_Service_Classification", new[] { "Classification_Code" });
            DropIndex("dbo.Medical_Service", new[] { "Medical_Service_Classification_ID" });
            DropIndex("dbo.Medical_Service", new[] { "Service_Code" });
            DropIndex("dbo.Medical_Contributions_Allocations_Services", new[] { "Medical_Service_Classification_ID" });
            DropIndex("dbo.Medical_Contributions_Allocations_Services", new[] { "Medical_Service_ID" });
            DropIndex("dbo.Medical_Contributions_Allocations_Services", new[] { "Medical_Contributions_Allocations_Entry_ID" });
            DropIndex("dbo.Medical_Contributions_Allocations_Entry", new[] { "Code" });
            DropIndex("dbo.months", new[] { "Exchange_Rate_ID" });
            DropIndex("dbo.Exchange_Rate", new[] { "CurrencyID" });
            DropIndex("dbo.Employee_records", new[] { "status_ID" });
            DropIndex("dbo.Employee_records", new[] { "Employee_Recodes_Group_ID" });
            DropIndex("dbo.Employee_records", new[] { "Employee_Profile_ID" });
            DropIndex("dbo.Documents", new[] { "Document_Group_ID" });
            DropIndex("dbo.Documents", new[] { "Code" });
            DropIndex("dbo.Document_Group", new[] { "Code" });
            DropIndex("dbo.Diseases", new[] { "Disease_Code" });
            DropIndex("dbo.Contact_methods", new[] { "Code" });
            DropIndex("dbo.check_request_change_status", new[] { "check_Request_ID" });
            DropIndex("dbo.check_Request", new[] { "Checktype_ID" });
            DropIndex("dbo.check_Request", new[] { "check_request_change_status_ID" });
            DropIndex("dbo.Budget_class_items", new[] { "Budget_class_ID" });
            DropIndex("dbo.Budget_details", new[] { "Budget_ID" });
            DropIndex("dbo.Budget_details", new[] { "Budget_class_items_ID" });
            DropIndex("dbo.Budgets", new[] { "status_ID" });
            DropIndex("dbo.Budgets", new[] { "organization_unit_ID" });
            DropIndex("dbo.Budgets", new[] { "justification_ID" });
            DropIndex("dbo.Authorities", new[] { "Authority_Type_ID" });
            DropIndex("dbo.Definition_of_EOS_Interview_Questions", new[] { "EOS_Interview_Questions_Groups_ID" });
            DropIndex("dbo.Service_Information", new[] { "Currency_ID" });
            DropIndex("dbo.Religions", new[] { "Code" });
            DropIndex("dbo.Sub_educational_body", new[] { "Name_of_educational_qualification_ID" });
            DropIndex("dbo.Sub_educational_body", new[] { "Main_Educate_bodyid" });
            DropIndex("dbo.Sub_educational_body", new[] { "Code" });
            DropIndex("dbo.Main_Educate_body", new[] { "Code" });
            DropIndex("dbo.Educate_category", new[] { "Code" });
            DropIndex("dbo.Employee_Qualification_Profile", new[] { "Sub_educational_body_ID" });
            DropIndex("dbo.Employee_Qualification_Profile", new[] { "Qualification_Major_ID" });
            DropIndex("dbo.Employee_Qualification_Profile", new[] { "Name_of_educational_qualification_ID" });
            DropIndex("dbo.Employee_Qualification_Profile", new[] { "Main_Educate_body_ID" });
            DropIndex("dbo.Employee_Qualification_Profile", new[] { "GradeEducate_ID" });
            DropIndex("dbo.Employee_Qualification_Profile", new[] { "Educate_Title_ID" });
            DropIndex("dbo.Employee_Qualification_Profile", new[] { "Educate_category_ID" });
            DropIndex("dbo.Slots", new[] { "job_title_cards_ID" });
            DropIndex("dbo.Slots", new[] { "job_level_setup_ID" });
            DropIndex("dbo.Organization_Chart", new[] { "work_location_ID" });
            DropIndex("dbo.Organization_Chart", new[] { "Organization_Chart_ID" });
            DropIndex("dbo.Organization_Chart", new[] { "unit_type_codeID" });
            DropIndex("dbo.Nationalities", new[] { "Code" });
            DropIndex("dbo.Job_title_sub_class", new[] { "job_title_cards_ID" });
            DropIndex("dbo.Job_title_sub_class", new[] { "Job_title_class_ID" });
            DropIndex("dbo.Organization_Unit_Type", new[] { "job_level_setup_ID" });
            DropIndex("dbo.Organization_Unit_Type", new[] { "Organization_Unit_Schema_ID" });
            DropIndex("dbo.Organization_Unit_Type", new[] { "Organization_Unit_Level_ID" });
            DropIndex("dbo.job_level_education", new[] { "job_level_setup_ID" });
            DropIndex("dbo.job_level_education", new[] { "Educate_Title_ID" });
            DropIndex("dbo.job_level_setup", new[] { "Job_level_grade_ID" });
            DropIndex("dbo.job_level_setup", new[] { "Job_level_class_ID" });
            DropIndex("dbo.Skills", new[] { "Skill_group_ID" });
            DropIndex("dbo.skills_job", new[] { "Job_Details_ID" });
            DropIndex("dbo.skills_job", new[] { "skill_ID" });
            DropIndex("dbo.Risks", new[] { "Job_Details_ID" });
            DropIndex("dbo.Risks", new[] { "Risks_Type_ID" });
            DropIndex("dbo.Responsibilities", new[] { "Job_Details_ID" });
            DropIndex("dbo.Requirments", new[] { "Job_Details_ID" });
            DropIndex("dbo.Qualification_Major", new[] { "Name_of_educational_qualificationid" });
            DropIndex("dbo.Qualification_Major", new[] { "Code" });
            DropIndex("dbo.Educate_Title", new[] { "Code" });
            DropIndex("dbo.Name_of_educational_qualification", new[] { "Educate_Titleid" });
            DropIndex("dbo.Name_of_educational_qualification", new[] { "Code" });
            DropIndex("dbo.GradeEducates", new[] { "Code" });
            DropIndex("dbo.qulification_job", new[] { "Job_Details_ID" });
            DropIndex("dbo.qulification_job", new[] { "Qualification_Major_ID" });
            DropIndex("dbo.qulification_job", new[] { "Name_of_educational_qualification_ID" });
            DropIndex("dbo.qulification_job", new[] { "GradeEducate_ID" });
            DropIndex("dbo.job_title_cards", new[] { "Organization_Chart_ID" });
            DropIndex("dbo.job_title_cards", new[] { "Nationality_ID" });
            DropIndex("dbo.job_title_cards", new[] { "job_level_setup_ID" });
            DropIndex("dbo.job_title_cards", new[] { "Job_Details_ID" });
            DropIndex("dbo.Position_Information", new[] { "work_location_ID" });
            DropIndex("dbo.Position_Information", new[] { "job_title_cards_ID" });
            DropIndex("dbo.Position_Information", new[] { "Job_level_grade_ID" });
            DropIndex("dbo.postcodes", new[] { "cities_ID" });
            DropIndex("dbo.postcodes", new[] { "Code" });
            DropIndex("dbo.Employee_Address_Profile", new[] { "the_states_ID" });
            DropIndex("dbo.Employee_Address_Profile", new[] { "Territories_ID" });
            DropIndex("dbo.Employee_Address_Profile", new[] { "postcode_ID" });
            DropIndex("dbo.Employee_Address_Profile", new[] { "Country_ID" });
            DropIndex("dbo.Employee_Address_Profile", new[] { "cities_ID" });
            DropIndex("dbo.Employee_Address_Profile", new[] { "Area_ID" });
            DropIndex("dbo.Territories", new[] { "the_states_ID" });
            DropIndex("dbo.Territories", new[] { "Code" });
            DropIndex("dbo.cities", new[] { "Territories_ID" });
            DropIndex("dbo.cities", new[] { "Code" });
            DropIndex("dbo.Employee_Profile", new[] { "the_states_ID" });
            DropIndex("dbo.Employee_Profile", new[] { "Territories_ID" });
            DropIndex("dbo.Employee_Profile", new[] { "Service_Information_ID" });
            DropIndex("dbo.Employee_Profile", new[] { "Religion_ID" });
            DropIndex("dbo.Employee_Profile", new[] { "Personnel_Information_ID" });
            DropIndex("dbo.Employee_Profile", new[] { "Nationality_ID" });
            DropIndex("dbo.Employee_Profile", new[] { "Employee_Qualification_Profile_ID" });
            DropIndex("dbo.Employee_Profile", new[] { "Employee_Positions_Profile_ID" });
            DropIndex("dbo.Employee_Profile", new[] { "Employee_Address_Profile_ID" });
            DropIndex("dbo.Employee_Profile", new[] { "Country_ID" });
            DropIndex("dbo.Employee_Profile", new[] { "cities_ID" });
            DropIndex("dbo.Employee_Profile", new[] { "Area_ID" });
            DropIndex("dbo.Employee_Profile", new[] { "Ability_ID" });
            DropIndex("dbo.Check_Lists_Items", new[] { "Check_List_Item_Groups_ID" });
            DropIndex("dbo.check_list_EOS", new[] { "item_ID" });
            DropIndex("dbo.check_list_EOS", new[] { "EOS_ID" });
            DropIndex("dbo.EOS_Request", new[] { "status_ID" });
            DropIndex("dbo.EOS_Request", new[] { "EOS_group_ID" });
            DropIndex("dbo.EOS_Request", new[] { "Employee_ID" });
            DropIndex("dbo.EOS_Request", new[] { "Check_List_Item_Groups_ID" });
            DropIndex("dbo.Answer_EOS", new[] { "question_ID" });
            DropIndex("dbo.Answer_EOS", new[] { "EOS_ID" });
            DropIndex("dbo.Allergy_Type", new[] { "Allergy_Code" });
            DropIndex("dbo.Countries", new[] { "Code" });
            DropIndex("dbo.Areas", new[] { "Country_ID" });
            DropIndex("dbo.Areas", new[] { "Code" });
            DropIndex("dbo.the_states", new[] { "Area_ID" });
            DropIndex("dbo.the_states", new[] { "Code" });
            DropIndex("dbo.Air_ports", new[] { "the_states_ID" });
            DropIndex("dbo.Air_ports", new[] { "Code" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TicketPrices");
            DropTable("dbo.Tests");
            DropTable("dbo.Subscription_Syndicate");
            DropTable("dbo.StructureModels");
            DropTable("dbo.Sponsors");
            DropTable("dbo.Special_Allwonce_History");
            DropTable("dbo.Sings_Symptom");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Rejection_Reasons");
            DropTable("dbo.Questions");
            DropTable("dbo.Position_Transaction_Information");
            DropTable("dbo.Military_Service_Status");
            DropTable("dbo.Military_Service_Rank");
            DropTable("dbo.Medicines");
            DropTable("dbo.Medical_Medicine_Classfication");
            DropTable("dbo.Medical_Doctors_Level");
            DropTable("dbo.Medical_Doctors");
            DropTable("dbo.Medical_Service_Group");
            DropTable("dbo.Medical_Service_Classification");
            DropTable("dbo.Medical_Service");
            DropTable("dbo.Medical_Contributions_Allocations_Services");
            DropTable("dbo.Medical_Contributions_Allocations_Entry");
            DropTable("dbo.Files");
            DropTable("dbo.FileDetailsModels");
            DropTable("dbo.External_compaines");
            DropTable("dbo.Experience_group");
            DropTable("dbo.months");
            DropTable("dbo.Exchange_Rate");
            DropTable("dbo.Employee_records");
            DropTable("dbo.Employee_Recodes_Group");
            DropTable("dbo.Employee_Profile_Groups");
            DropTable("dbo.Documents");
            DropTable("dbo.Document_Group");
            DropTable("dbo.Diseases");
            DropTable("dbo.Contract_Type");
            DropTable("dbo.Contact_methods");
            DropTable("dbo.Committe_subjects");
            DropTable("dbo.Checktypes");
            DropTable("dbo.check_request_status");
            DropTable("dbo.check_request_change_status");
            DropTable("dbo.check_Request");
            DropTable("dbo.Check_List_Items");
            DropTable("dbo.justifications");
            DropTable("dbo.Budget_class");
            DropTable("dbo.Budget_class_items");
            DropTable("dbo.Budget_details");
            DropTable("dbo.Budgets");
            DropTable("dbo.Authority_Type");
            DropTable("dbo.Authorities");
            DropTable("dbo.status");
            DropTable("dbo.Definition_of_EOS_Interview_Questions");
            DropTable("dbo.EOS_Interview_Questions_Groups");
            DropTable("dbo.Currencies");
            DropTable("dbo.Service_Information");
            DropTable("dbo.Religions");
            DropTable("dbo.Personnel_Information");
            DropTable("dbo.Sub_educational_body");
            DropTable("dbo.Main_Educate_body");
            DropTable("dbo.Educate_category");
            DropTable("dbo.Employee_Qualification_Profile");
            DropTable("dbo.Slots");
            DropTable("dbo.work_location");
            DropTable("dbo.Organization_Chart");
            DropTable("dbo.Nationalities");
            DropTable("dbo.Job_title_class");
            DropTable("dbo.Job_title_sub_class");
            DropTable("dbo.Organization_Unit_Schema");
            DropTable("dbo.Organization_Unit_Level");
            DropTable("dbo.Organization_Unit_Type");
            DropTable("dbo.job_level_education");
            DropTable("dbo.Job_level_class");
            DropTable("dbo.job_level_setup");
            DropTable("dbo.Skill_group");
            DropTable("dbo.Skills");
            DropTable("dbo.skills_job");
            DropTable("dbo.Risks_Type");
            DropTable("dbo.Risks");
            DropTable("dbo.Responsibilities");
            DropTable("dbo.Requirments");
            DropTable("dbo.Qualification_Major");
            DropTable("dbo.Educate_Title");
            DropTable("dbo.Name_of_educational_qualification");
            DropTable("dbo.GradeEducates");
            DropTable("dbo.qulification_job");
            DropTable("dbo.Job_Details");
            DropTable("dbo.job_title_cards");
            DropTable("dbo.Job_level_grade");
            DropTable("dbo.Position_Information");
            DropTable("dbo.postcodes");
            DropTable("dbo.Employee_Address_Profile");
            DropTable("dbo.Territories");
            DropTable("dbo.cities");
            DropTable("dbo.Employee_Profile");
            DropTable("dbo.Check_List_Item_Groups");
            DropTable("dbo.Check_Lists_Items");
            DropTable("dbo.check_list_EOS");
            DropTable("dbo.EOS_Request");
            DropTable("dbo.Answer_EOS");
            DropTable("dbo.Allowance_years");
            DropTable("dbo.Allergy_Type");
            DropTable("dbo.Countries");
            DropTable("dbo.Areas");
            DropTable("dbo.the_states");
            DropTable("dbo.Air_ports");
            DropTable("dbo.Abilities");
        }
    }
}
