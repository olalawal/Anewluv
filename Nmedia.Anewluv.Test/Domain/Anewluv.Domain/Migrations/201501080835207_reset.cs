namespace Anewluv.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reset : DbMigration
    {
        public override void Up()
        {
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
                        profileactivitygeodata_id = c.Int(),
                        activitytype_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_activitytype", t => t.activitytype_id)
                .ForeignKey("dbo.profiles", t => t.profile_id, cascadeDelete: true)
                .Index(t => t.profile_id)
                .Index(t => t.activitytype_id);
            
            CreateTable(
                "dbo.profileactivitygeodatas",
                c => new
                    {
                        id = c.Int(nullable: false),
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
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.profileactivities", t => t.id)
                .Index(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.profileactivitygeodatas", "id", "dbo.profileactivities");
            DropForeignKey("dbo.profileactivities", "profile_id", "dbo.profiles");
            DropForeignKey("dbo.profileactivities", "activitytype_id", "dbo.lu_activitytype");
            DropIndex("dbo.profileactivitygeodatas", new[] { "id" });
            DropIndex("dbo.profileactivities", new[] { "activitytype_id" });
            DropIndex("dbo.profileactivities", new[] { "profile_id" });
            DropTable("dbo.profileactivitygeodatas");
            DropTable("dbo.profileactivities");
        }
    }
}
