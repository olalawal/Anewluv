namespace Nmedia.Infrastructure.Domain.Notification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moregeneric : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.lu_template", "application_id", "dbo.lu_application");
            DropForeignKey("dbo.lu_templatebody", "application_id", "dbo.lu_application");
            DropForeignKey("dbo.lu_templatefilename", "application_id1", "dbo.lu_application");
            DropForeignKey("dbo.lu_templatesubject", "application_id", "dbo.lu_application");
            DropIndex("dbo.lu_template", new[] { "application_id" });
            DropIndex("dbo.lu_templatebody", new[] { "application_id" });
            DropIndex("dbo.lu_templatefilename", new[] { "application_id1" });
            DropIndex("dbo.lu_templatesubject", new[] { "application_id" });
            AddColumn("dbo.lu_application", "activationURL", c => c.String());
            AddColumn("dbo.lu_application", "recoveryURL", c => c.String());
            AddColumn("dbo.lu_application", "contactusURL", c => c.String());
            AddColumn("dbo.lu_application", "websiteURL", c => c.String());
            AddColumn("dbo.lu_application", "loginURL", c => c.String());
            DropColumn("dbo.lu_template", "application_id");
            DropColumn("dbo.lu_templatebody", "application_id");
            DropColumn("dbo.lu_templatefilename", "application_id");
            DropColumn("dbo.lu_templatefilename", "application_id1");
            DropColumn("dbo.lu_templatesubject", "application_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.lu_templatesubject", "application_id", c => c.Int());
            AddColumn("dbo.lu_templatefilename", "application_id1", c => c.Int());
            AddColumn("dbo.lu_templatefilename", "application_id", c => c.Int());
            AddColumn("dbo.lu_templatebody", "application_id", c => c.Int());
            AddColumn("dbo.lu_template", "application_id", c => c.Int());
            DropColumn("dbo.lu_application", "loginURL");
            DropColumn("dbo.lu_application", "websiteURL");
            DropColumn("dbo.lu_application", "contactusURL");
            DropColumn("dbo.lu_application", "recoveryURL");
            DropColumn("dbo.lu_application", "activationURL");
            CreateIndex("dbo.lu_templatesubject", "application_id");
            CreateIndex("dbo.lu_templatefilename", "application_id1");
            CreateIndex("dbo.lu_templatebody", "application_id");
            CreateIndex("dbo.lu_template", "application_id");
            AddForeignKey("dbo.lu_templatesubject", "application_id", "dbo.lu_application", "id");
            AddForeignKey("dbo.lu_templatefilename", "application_id1", "dbo.lu_application", "id");
            AddForeignKey("dbo.lu_templatebody", "application_id", "dbo.lu_application", "id");
            AddForeignKey("dbo.lu_template", "application_id", "dbo.lu_application", "id");
        }
    }
}
