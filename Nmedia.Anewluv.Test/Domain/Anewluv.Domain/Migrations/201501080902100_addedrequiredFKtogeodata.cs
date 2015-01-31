namespace Anewluv.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedrequiredFKtogeodata : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.profileactivitygeodatas", "id", "dbo.profileactivities");
            DropIndex("dbo.profileactivitygeodatas", new[] { "id" });
            DropPrimaryKey("dbo.profileactivitygeodatas");
            AddColumn("dbo.profileactivities", "profileactivitygeodata_id1", c => c.Int());
            AddColumn("dbo.profileactivitygeodatas", "activity_id", c => c.Int(nullable: false));
            AlterColumn("dbo.profileactivitygeodatas", "id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.profileactivitygeodatas", "id");
            CreateIndex("dbo.profileactivities", "profileactivitygeodata_id1");
            CreateIndex("dbo.profileactivitygeodatas", "activity_id");
            AddForeignKey("dbo.profileactivities", "profileactivitygeodata_id1", "dbo.profileactivitygeodatas", "id");
            AddForeignKey("dbo.profileactivitygeodatas", "activity_id", "dbo.profileactivities", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.profileactivitygeodatas", "activity_id", "dbo.profileactivities");
            DropForeignKey("dbo.profileactivities", "profileactivitygeodata_id1", "dbo.profileactivitygeodatas");
            DropIndex("dbo.profileactivitygeodatas", new[] { "activity_id" });
            DropIndex("dbo.profileactivities", new[] { "profileactivitygeodata_id1" });
            DropPrimaryKey("dbo.profileactivitygeodatas");
            AlterColumn("dbo.profileactivitygeodatas", "id", c => c.Int(nullable: false));
            DropColumn("dbo.profileactivitygeodatas", "activity_id");
            DropColumn("dbo.profileactivities", "profileactivitygeodata_id1");
            AddPrimaryKey("dbo.profileactivitygeodatas", "id");
            CreateIndex("dbo.profileactivitygeodatas", "id");
            AddForeignKey("dbo.profileactivitygeodatas", "id", "dbo.profileactivities", "id");
        }
    }
}
