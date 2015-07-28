namespace Anewluv.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newfeildstophotoandprofile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.profiles", "isuseradmin", c => c.Boolean());
            AddColumn("dbo.photos", "landingpagevisiblity", c => c.Boolean());
            AddColumn("dbo.photos", "adminmodificationdate", c => c.DateTime());
            AddColumn("dbo.photos", "adminmodiferprofile_id", c => c.Int());
            AddColumn("dbo.photos", "modificationdate", c => c.DateTime());
            CreateIndex("dbo.photos", "adminmodiferprofile_id");
            AddForeignKey("dbo.photos", "adminmodiferprofile_id", "dbo.profiles", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.photos", "adminmodiferprofile_id", "dbo.profiles");
            DropIndex("dbo.photos", new[] { "adminmodiferprofile_id" });
            DropColumn("dbo.photos", "modificationdate");
            DropColumn("dbo.photos", "adminmodiferprofile_id");
            DropColumn("dbo.photos", "adminmodificationdate");
            DropColumn("dbo.photos", "landingpagevisiblity");
            DropColumn("dbo.profiles", "isuseradmin");
        }
    }
}
