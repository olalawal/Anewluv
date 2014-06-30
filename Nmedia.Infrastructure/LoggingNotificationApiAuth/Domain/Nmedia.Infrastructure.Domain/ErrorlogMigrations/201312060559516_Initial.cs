namespace Nmedia.Infrastructure.Domain.ErrorlogMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.errorlog_logs",
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
                .ForeignKey("dbo.errorlog_lu_application", t => t.application_id)
                .ForeignKey("dbo.errorlog_lu_enviroment", t => t.enviroment_id)
                .ForeignKey("dbo.errorlog_lu_logseverity", t => t.logseverity_id)
                .ForeignKey("dbo.errorlog_lu_logseverityinternal", t => t.logseverityinternal_id)
                .Index(t => t.application_id)
                .Index(t => t.enviroment_id)
                .Index(t => t.logseverity_id)
                .Index(t => t.logseverityinternal_id);
            
            CreateTable(
                "dbo.errorlog_lu_application",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.errorlog_lu_enviroment",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.errorlog_lu_logseverity",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.errorlog_lu_logseverityinternal",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.errorlog_logs", "logseverityinternal_id", "dbo.errorlog_lu_logseverityinternal");
            DropForeignKey("dbo.errorlog_logs", "logseverity_id", "dbo.errorlog_lu_logseverity");
            DropForeignKey("dbo.errorlog_logs", "enviroment_id", "dbo.errorlog_lu_enviroment");
            DropForeignKey("dbo.errorlog_logs", "application_id", "dbo.errorlog_lu_application");
            DropIndex("dbo.errorlog_logs", new[] { "logseverityinternal_id" });
            DropIndex("dbo.errorlog_logs", new[] { "logseverity_id" });
            DropIndex("dbo.errorlog_logs", new[] { "enviroment_id" });
            DropIndex("dbo.errorlog_logs", new[] { "application_id" });
            DropTable("dbo.errorlog_lu_logseverityinternal");
            DropTable("dbo.errorlog_lu_logseverity");
            DropTable("dbo.errorlog_lu_enviroment");
            DropTable("dbo.errorlog_lu_application");
            DropTable("dbo.errorlog_logs");
        }
    }
}
