namespace Anewluv.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.profileactivities", "activitytype_id", "dbo.lu_activitytype");
            DropForeignKey("dbo.profileactivities", "profile_id", "dbo.profiles");
            DropForeignKey("dbo.profileactivities", "profileactivitygeodata_id", "dbo.profileactivitygeodatas");
            DropIndex("dbo.profileactivities", new[] { "profile_id" });
            DropIndex("dbo.profileactivities", new[] { "profileactivitygeodata_id" });
            DropIndex("dbo.profileactivities", new[] { "activitytype_id" });
            DropTable("dbo.profileactivities");
            DropTable("dbo.profileactivitygeodatas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.profileactivitygeodatas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        city = c.String(),
                        regionname = c.String(),
                        continent = c.String(),
                        countryId = c.Int(),
                        countrycode = c.String(),
                        countryname = c.String(),
                        creationdate = c.DateTime(),
                        lattitude = c.Double(),
                        longitude = c.Double(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.profileactivities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        creationdate = c.DateTime(),
                        ipaddress = c.String(),
                        profile_id = c.Int(nullable: false),
                        sessionid = c.String(),
                        apikey = c.Guid(nullable: false),
                        useragent = c.String(),
                        routeurl = c.String(),
                        actionname = c.String(),
                        profileactivitygeodata_id = c.Int(nullable: false),
                        activitytype_id = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateIndex("dbo.profileactivities", "activitytype_id");
            CreateIndex("dbo.profileactivities", "profileactivitygeodata_id");
            CreateIndex("dbo.profileactivities", "profile_id");
            AddForeignKey("dbo.profileactivities", "profileactivitygeodata_id", "dbo.profileactivitygeodatas", "id", cascadeDelete: true);
            AddForeignKey("dbo.profileactivities", "profile_id", "dbo.profiles", "id", cascadeDelete: true);
            AddForeignKey("dbo.profileactivities", "activitytype_id", "dbo.lu_activitytype", "id");
        }
    }
}
