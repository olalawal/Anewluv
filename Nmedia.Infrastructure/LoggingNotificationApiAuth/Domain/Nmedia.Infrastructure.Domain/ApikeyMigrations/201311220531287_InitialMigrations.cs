namespace Nmedia.Infrastructure.Domain.ApikeyMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.api_apicall",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        timestamp = c.DateTime(),
                        ipaddress = c.String(),
                        destinationurl = c.String(),
                        apikey_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.api_apikey", t => t.apikey_id, cascadeDelete: true)
                .Index(t => t.apikey_id);
            
            CreateTable(
                "dbo.api_apikey",
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
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.api_lu_accesslevel", t => t.accesslevel_id, cascadeDelete: true)
                .ForeignKey("dbo.api_lu_application", t => t.application_id, cascadeDelete: true)
                .Index(t => t.accesslevel_id)
                .Index(t => t.application_id);
            
            CreateTable(
                "dbo.api_lu_accesslevel",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.api_lu_application",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.api_user",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        timestamp = c.DateTime(),
                        username = c.String(),
                        email = c.String(),
                        active = c.Boolean(nullable: false),
                        registeringapplication = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.userapikey",
                c => new
                    {
                        apikey_id = c.Int(nullable: false),
                        user_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.apikey_id, t.user_id })
                .ForeignKey("dbo.api_apikey", t => t.apikey_id, cascadeDelete: true)
                .ForeignKey("dbo.api_user", t => t.user_id, cascadeDelete: true)
                .Index(t => t.apikey_id)
                .Index(t => t.user_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.api_apicall", "apikey_id", "dbo.api_apikey");
            DropForeignKey("dbo.userapikey", "user_id", "dbo.api_user");
            DropForeignKey("dbo.userapikey", "apikey_id", "dbo.api_apikey");
            DropForeignKey("dbo.api_apikey", "application_id", "dbo.api_lu_application");
            DropForeignKey("dbo.api_apikey", "accesslevel_id", "dbo.api_lu_accesslevel");
            DropIndex("dbo.api_apicall", new[] { "apikey_id" });
            DropIndex("dbo.userapikey", new[] { "user_id" });
            DropIndex("dbo.userapikey", new[] { "apikey_id" });
            DropIndex("dbo.api_apikey", new[] { "application_id" });
            DropIndex("dbo.api_apikey", new[] { "accesslevel_id" });
            DropTable("dbo.userapikey");
            DropTable("dbo.api_user");
            DropTable("dbo.api_lu_application");
            DropTable("dbo.api_lu_accesslevel");
            DropTable("dbo.api_apikey");
            DropTable("dbo.api_apicall");
        }
    }
}
