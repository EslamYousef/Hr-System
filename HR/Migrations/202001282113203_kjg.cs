namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kjg : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.the_states", new[] { "Code" });
            DropIndex("dbo.Areas", new[] { "Code" });
            DropIndex("dbo.Countries", new[] { "Code" });
            DropIndex("dbo.cities", new[] { "Code" });
            DropIndex("dbo.Territories", new[] { "Code" });
            DropIndex("dbo.postcodes", new[] { "Code" });
            DropIndex("dbo.Educate_Title", new[] { "Code" });
            DropIndex("dbo.Name_of_educational_qualification", new[] { "Code" });
            DropIndex("dbo.GradeEducates", new[] { "Code" });
            DropIndex("dbo.Educate_category", new[] { "Code" });
            DropIndex("dbo.Main_Educate_body", new[] { "Code" });
            DropIndex("dbo.Sub_educational_body", new[] { "Code" });
            AlterColumn("dbo.the_states", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.Areas", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.Countries", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.cities", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.Territories", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.postcodes", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.Educate_Title", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.Name_of_educational_qualification", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.GradeEducates", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.Educate_category", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.Main_Educate_body", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.Sub_educational_body", "Code", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sub_educational_body", "Code", c => c.Double(nullable: false));
            AlterColumn("dbo.Main_Educate_body", "Code", c => c.Double(nullable: false));
            AlterColumn("dbo.Educate_category", "Code", c => c.Double(nullable: false));
            AlterColumn("dbo.GradeEducates", "Code", c => c.Double(nullable: false));
            AlterColumn("dbo.Name_of_educational_qualification", "Code", c => c.Double(nullable: false));
            AlterColumn("dbo.Educate_Title", "Code", c => c.Double(nullable: false));
            AlterColumn("dbo.postcodes", "Code", c => c.Double(nullable: false));
            AlterColumn("dbo.Territories", "Code", c => c.Double(nullable: false));
            AlterColumn("dbo.cities", "Code", c => c.Double(nullable: false));
            AlterColumn("dbo.Countries", "Code", c => c.Double(nullable: false));
            AlterColumn("dbo.Areas", "Code", c => c.Double(nullable: false));
            AlterColumn("dbo.the_states", "Code", c => c.Double(nullable: false));
            CreateIndex("dbo.Sub_educational_body", "Code", unique: true);
            CreateIndex("dbo.Main_Educate_body", "Code", unique: true);
            CreateIndex("dbo.Educate_category", "Code", unique: true);
            CreateIndex("dbo.GradeEducates", "Code", unique: true);
            CreateIndex("dbo.Name_of_educational_qualification", "Code", unique: true);
            CreateIndex("dbo.Educate_Title", "Code", unique: true);
            CreateIndex("dbo.postcodes", "Code", unique: true);
            CreateIndex("dbo.Territories", "Code", unique: true);
            CreateIndex("dbo.cities", "Code", unique: true);
            CreateIndex("dbo.Countries", "Code", unique: true);
            CreateIndex("dbo.Areas", "Code", unique: true);
            CreateIndex("dbo.the_states", "Code", unique: true);
        }
    }
}
