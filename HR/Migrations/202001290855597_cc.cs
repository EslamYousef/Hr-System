namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cc : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Document_Group", new[] { "Code" });
            DropIndex("dbo.Documents", new[] { "Code" });
            AlterColumn("dbo.Document_Group", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.Documents", "Code", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Documents", "Code", c => c.Double(nullable: false));
            AlterColumn("dbo.Document_Group", "Code", c => c.Double(nullable: false));
            CreateIndex("dbo.Documents", "Code", unique: true);
            CreateIndex("dbo.Document_Group", "Code", unique: true);
        }
    }
}
