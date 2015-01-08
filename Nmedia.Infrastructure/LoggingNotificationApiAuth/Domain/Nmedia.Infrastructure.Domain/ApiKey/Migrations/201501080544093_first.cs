namespace Nmedia.Infrastructure.Domain.Apikey.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.apicalls",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        timestamp = c.DateTime(),
                        ipaddress = c.String(),
                        destinationurl = c.String(),
                        apikey_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.apikeys", t => t.apikey_id, cascadeDelete: true)
                .Index(t => t.apikey_id);
            
            CreateTable(
                "dbo.apikeys",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        timestamp = c.DateTime(),
                        externalapplicationname = c.String(),
                        key = c.Guid(nullable: false),
                        active = c.Boolean(),
                        lastaccesstime = c.DateTime(),
                        accesslevel_id = c.Int(nullable: false),
                        application_id = c.Int(nullable: false),
                        user_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_accesslevel", t => t.accesslevel_id, cascadeDelete: true)
                .ForeignKey("dbo.lu_application", t => t.application_id, cascadeDelete: true)
                .ForeignKey("dbo.users", t => t.user_id)
                .Index(t => t.accesslevel_id)
                .Index(t => t.application_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.lu_accesslevel",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_application",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        timestamp = c.DateTime(),
                        username = c.String(),
                        useridentifier = c.Int(nullable: false),
                        email = c.String(),
                        active = c.Boolean(nullable: false),
                        registeringapplication = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.apicalls", "apikey_id", "dbo.apikeys");
            DropForeignKey("dbo.apikeys", "user_id", "dbo.users");
            DropForeignKey("dbo.apikeys", "application_id", "dbo.lu_application");
            DropForeignKey("dbo.apikeys", "accesslevel_id", "dbo.lu_accesslevel");
            DropIndex("dbo.apikeys", new[] { "user_id" });
            DropIndex("dbo.apikeys", new[] { "application_id" });
            DropIndex("dbo.apikeys", new[] { "accesslevel_id" });
            DropIndex("dbo.apicalls", new[] { "apikey_id" });
            DropTable("dbo.users");
            DropTable("dbo.lu_application");
            DropTable("dbo.lu_accesslevel");
            DropTable("dbo.apikeys");
            DropTable("dbo.apicalls");
        }
    }
}
