namespace Anewluv.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedProfileActivityProfileIdvalue : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.profileactivities", "profile_id", "dbo.profiles");
            DropIndex("dbo.profileactivities", new[] { "profile_id" });
            AlterColumn("dbo.profileactivities", "profile_id", c => c.Int());
            AlterColumn("dbo.profileactivities", "apikey", c => c.Guid());
            CreateIndex("dbo.profileactivities", "profile_id");
            AddForeignKey("dbo.profileactivities", "profile_id", "dbo.profiles", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.profileactivities", "profile_id", "dbo.profiles");
            DropIndex("dbo.profileactivities", new[] { "profile_id" });
            AlterColumn("dbo.profileactivities", "apikey", c => c.Guid(nullable: false));
            AlterColumn("dbo.profileactivities", "profile_id", c => c.Int(nullable: false));
            CreateIndex("dbo.profileactivities", "profile_id");
            AddForeignKey("dbo.profileactivities", "profile_id", "dbo.profiles", "id", cascadeDelete: true);
        }
    }
}
