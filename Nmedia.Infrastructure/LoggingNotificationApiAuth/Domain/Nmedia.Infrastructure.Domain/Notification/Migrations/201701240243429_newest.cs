namespace Nmedia.Infrastructure.Domain.Notification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.lu_application",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        active = c.Boolean(),
                        creationdate = c.DateTime(),
                        removaldate = c.DateTime(),
                        logourl = c.String(),
                        emaildeliverystring = c.String(),
                        photourl = c.String(),
                        bottombulleturl = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.addresses", "application_id", c => c.Int());
            AddColumn("dbo.lu_template", "application_id", c => c.Int());
            AddColumn("dbo.lu_templatebody", "application_id", c => c.Int());
            AddColumn("dbo.lu_templatefilename", "application_id", c => c.Int());
            AddColumn("dbo.lu_templatefilename", "application_id1", c => c.Int());
            AddColumn("dbo.lu_templatesubject", "application_id", c => c.Int());
            CreateIndex("dbo.addresses", "application_id");
            CreateIndex("dbo.lu_template", "application_id");
            CreateIndex("dbo.lu_templatebody", "application_id");
            CreateIndex("dbo.lu_templatefilename", "application_id1");
            CreateIndex("dbo.lu_templatesubject", "application_id");
            AddForeignKey("dbo.addresses", "application_id", "dbo.lu_application", "id");
            AddForeignKey("dbo.lu_template", "application_id", "dbo.lu_application", "id");
            AddForeignKey("dbo.lu_templatebody", "application_id", "dbo.lu_application", "id");
            AddForeignKey("dbo.lu_templatefilename", "application_id1", "dbo.lu_application", "id");
            AddForeignKey("dbo.lu_templatesubject", "application_id", "dbo.lu_application", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.lu_templatesubject", "application_id", "dbo.lu_application");
            DropForeignKey("dbo.lu_templatefilename", "application_id1", "dbo.lu_application");
            DropForeignKey("dbo.lu_templatebody", "application_id", "dbo.lu_application");
            DropForeignKey("dbo.lu_template", "application_id", "dbo.lu_application");
            DropForeignKey("dbo.addresses", "application_id", "dbo.lu_application");
            DropIndex("dbo.lu_templatesubject", new[] { "application_id" });
            DropIndex("dbo.lu_templatefilename", new[] { "application_id1" });
            DropIndex("dbo.lu_templatebody", new[] { "application_id" });
            DropIndex("dbo.lu_template", new[] { "application_id" });
            DropIndex("dbo.addresses", new[] { "application_id" });
            DropColumn("dbo.lu_templatesubject", "application_id");
            DropColumn("dbo.lu_templatefilename", "application_id1");
            DropColumn("dbo.lu_templatefilename", "application_id");
            DropColumn("dbo.lu_templatebody", "application_id");
            DropColumn("dbo.lu_template", "application_id");
            DropColumn("dbo.addresses", "application_id");
            DropTable("dbo.lu_application");
        }
    }
}
