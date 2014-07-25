namespace Nmedia.Infrastructure.Domain.Logging.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.logs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        timestamp = c.DateTime(),
                        loggeduser = c.String(),
                        ipaddress = c.String(),
                        assemblyname = c.String(),
                        classname = c.String(),
                        methodname = c.String(),
                        parentmethodname = c.String(),
                        profileid = c.String(),
                        errorpage = c.String(),
                        type = c.String(),
                        linenumbers = c.Int(nullable: false),
                        stacktrace = c.String(),
                        message = c.String(),
                        sessionid = c.String(),
                        querystring = c.String(),
                        request = c.String(),
                        application_id = c.Int(nullable: false),
                        enviroment_id = c.Int(nullable: false),
                        logseverity_id = c.Int(nullable: false),
                        logseverityinternal_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_logapplication", t => t.application_id)
                .ForeignKey("dbo.lu_logenviroment", t => t.enviroment_id)
                .ForeignKey("dbo.lu_logseverity", t => t.logseverity_id)
                .ForeignKey("dbo.lu_logseverityinternal", t => t.logseverityinternal_id)
                .Index(t => t.application_id)
                .Index(t => t.enviroment_id)
                .Index(t => t.logseverity_id)
                .Index(t => t.logseverityinternal_id);
            
            CreateTable(
                "dbo.lu_logapplication",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_logenviroment",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_logseverity",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_logseverityinternal",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.logs", "logseverityinternal_id", "dbo.lu_logseverityinternal");
            DropForeignKey("dbo.logs", "logseverity_id", "dbo.lu_logseverity");
            DropForeignKey("dbo.logs", "enviroment_id", "dbo.lu_logenviroment");
            DropForeignKey("dbo.logs", "application_id", "dbo.lu_logapplication");
            DropIndex("dbo.logs", new[] { "logseverityinternal_id" });
            DropIndex("dbo.logs", new[] { "logseverity_id" });
            DropIndex("dbo.logs", new[] { "enviroment_id" });
            DropIndex("dbo.logs", new[] { "application_id" });
            DropTable("dbo.lu_logseverityinternal");
            DropTable("dbo.lu_logseverity");
            DropTable("dbo.lu_logenviroment");
            DropTable("dbo.lu_logapplication");
            DropTable("dbo.logs");
        }
    }
}
