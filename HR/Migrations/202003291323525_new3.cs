namespace HR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Organization_Unit_Type", "Organization_Unit_Level_ID", "dbo.Organization_Unit_Level");
            DropForeignKey("dbo.Organization_Unit_Type", "Organization_Unit_Schema_ID", "dbo.Organization_Unit_Schema");
            DropIndex("dbo.Organization_Unit_Type", new[] { "unitLevelcode" });
            DropIndex("dbo.Organization_Unit_Type", new[] { "Organization_Unit_Level_ID" });
            DropIndex("dbo.Organization_Unit_Type", new[] { "Organization_Unit_Schema_ID" });
            RenameColumn(table: "dbo.Organization_Unit_Type", name: "Organization_Unit_Level_ID", newName: "Organization_Unit_LevelID");
            RenameColumn(table: "dbo.Organization_Unit_Type", name: "Organization_Unit_Schema_ID", newName: "Organization_Unit_SchemaID");
            AlterColumn("dbo.Organization_Unit_Type", "Organization_Unit_LevelID", c => c.Int(nullable: false));
            AlterColumn("dbo.Organization_Unit_Type", "Organization_Unit_SchemaID", c => c.Int(nullable: false));
            CreateIndex("dbo.Organization_Unit_Type", "Organization_Unit_LevelID");
            CreateIndex("dbo.Organization_Unit_Type", "Organization_Unit_SchemaID");
            AddForeignKey("dbo.Organization_Unit_Type", "Organization_Unit_LevelID", "dbo.Organization_Unit_Level", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Organization_Unit_Type", "Organization_Unit_SchemaID", "dbo.Organization_Unit_Schema", "ID", cascadeDelete: true);
            DropColumn("dbo.Organization_Unit_Type", "unitLevelcode");
            DropColumn("dbo.Organization_Unit_Type", "unitschemacode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Organization_Unit_Type", "unitschemacode", c => c.String());
            AddColumn("dbo.Organization_Unit_Type", "unitLevelcode", c => c.String(maxLength: 50));
            DropForeignKey("dbo.Organization_Unit_Type", "Organization_Unit_SchemaID", "dbo.Organization_Unit_Schema");
            DropForeignKey("dbo.Organization_Unit_Type", "Organization_Unit_LevelID", "dbo.Organization_Unit_Level");
            DropIndex("dbo.Organization_Unit_Type", new[] { "Organization_Unit_SchemaID" });
            DropIndex("dbo.Organization_Unit_Type", new[] { "Organization_Unit_LevelID" });
            AlterColumn("dbo.Organization_Unit_Type", "Organization_Unit_SchemaID", c => c.Int());
            AlterColumn("dbo.Organization_Unit_Type", "Organization_Unit_LevelID", c => c.Int());
            RenameColumn(table: "dbo.Organization_Unit_Type", name: "Organization_Unit_SchemaID", newName: "Organization_Unit_Schema_ID");
            RenameColumn(table: "dbo.Organization_Unit_Type", name: "Organization_Unit_LevelID", newName: "Organization_Unit_Level_ID");
            CreateIndex("dbo.Organization_Unit_Type", "Organization_Unit_Schema_ID");
            CreateIndex("dbo.Organization_Unit_Type", "Organization_Unit_Level_ID");
            CreateIndex("dbo.Organization_Unit_Type", "unitLevelcode", unique: true);
            AddForeignKey("dbo.Organization_Unit_Type", "Organization_Unit_Schema_ID", "dbo.Organization_Unit_Schema", "ID");
            AddForeignKey("dbo.Organization_Unit_Type", "Organization_Unit_Level_ID", "dbo.Organization_Unit_Level", "ID");
        }
    }
}
