namespace Anewluv.Domain.Migrations.AnewluvMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.mailboxfoldertypes", "defaultfolder_id", "dbo.lu_defaultmailboxfolder");
            DropForeignKey("dbo.mailboxfolders", "foldertype_id", "dbo.mailboxfoldertypes");
            DropForeignKey("dbo.profiles", "status_id", "dbo.lu_profilestatus");
            DropIndex("dbo.profiles", new[] { "status_id" });
            DropIndex("dbo.mailboxfolders", new[] { "foldertype_id" });
            DropIndex("dbo.mailboxfoldertypes", new[] { "defaultfolder_id" });
            AddColumn("dbo.profiles", "passwordchangeattempts", c => c.Int());
            AddColumn("dbo.mailboxfolders", "displayname", c => c.String());
            AddColumn("dbo.mailboxfolders", "creationdate", c => c.DateTime());
            AddColumn("dbo.mailboxfolders", "deleteddate", c => c.DateTime());
            AddColumn("dbo.mailboxfolders", "maxsizeinbytes", c => c.Int());
            AddColumn("dbo.mailboxfolders", "defaultfolder_id", c => c.Int());
            AddColumn("dbo.mailboxmessagefolders", "deleted", c => c.Boolean());
            AddColumn("dbo.mailboxmessagefolders", "moved", c => c.Boolean());
            AddColumn("dbo.mailboxmessagefolders", "movedate", c => c.DateTime());
            AddColumn("dbo.mailboxmessagefolders", "draft", c => c.Boolean());
            AddColumn("dbo.mailboxmessagefolders", "flagged", c => c.Boolean(nullable: false));
            AddColumn("dbo.mailboxmessagefolders", "read", c => c.Boolean());
            AddColumn("dbo.mailboxmessagefolders", "replied", c => c.Boolean());
            AddColumn("dbo.mailboxmessages", "sizeinbtyes", c => c.Int());
            AlterColumn("dbo.profiles", "status_id", c => c.Int(nullable: false));
            CreateIndex("dbo.profiles", "status_id");
            CreateIndex("dbo.mailboxfolders", "defaultfolder_id");
            AddForeignKey("dbo.mailboxfolders", "defaultfolder_id", "dbo.lu_defaultmailboxfolder", "id");
            AddForeignKey("dbo.profiles", "status_id", "dbo.lu_profilestatus", "id", cascadeDelete: true);
            DropColumn("dbo.mailboxfolders", "foldertype_id");
            DropColumn("dbo.mailboxmessages", "uniqueid");
            DropTable("dbo.mailboxfoldertypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.mailboxfoldertypes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        active = c.Boolean(),
                        creationdate = c.DateTime(),
                        deleteddate = c.DateTime(),
                        maxsize = c.Int(),
                        defaultfolder_id = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.mailboxmessages", "uniqueid", c => c.Int());
            AddColumn("dbo.mailboxfolders", "foldertype_id", c => c.Int());
            DropForeignKey("dbo.profiles", "status_id", "dbo.lu_profilestatus");
            DropForeignKey("dbo.mailboxfolders", "defaultfolder_id", "dbo.lu_defaultmailboxfolder");
            DropIndex("dbo.mailboxfolders", new[] { "defaultfolder_id" });
            DropIndex("dbo.profiles", new[] { "status_id" });
            AlterColumn("dbo.profiles", "status_id", c => c.Int());
            DropColumn("dbo.mailboxmessages", "sizeinbtyes");
            DropColumn("dbo.mailboxmessagefolders", "replied");
            DropColumn("dbo.mailboxmessagefolders", "read");
            DropColumn("dbo.mailboxmessagefolders", "flagged");
            DropColumn("dbo.mailboxmessagefolders", "draft");
            DropColumn("dbo.mailboxmessagefolders", "movedate");
            DropColumn("dbo.mailboxmessagefolders", "moved");
            DropColumn("dbo.mailboxmessagefolders", "deleted");
            DropColumn("dbo.mailboxfolders", "defaultfolder_id");
            DropColumn("dbo.mailboxfolders", "maxsizeinbytes");
            DropColumn("dbo.mailboxfolders", "deleteddate");
            DropColumn("dbo.mailboxfolders", "creationdate");
            DropColumn("dbo.mailboxfolders", "displayname");
            DropColumn("dbo.profiles", "passwordchangeattempts");
            CreateIndex("dbo.mailboxfoldertypes", "defaultfolder_id");
            CreateIndex("dbo.mailboxfolders", "foldertype_id");
            CreateIndex("dbo.profiles", "status_id");
            AddForeignKey("dbo.profiles", "status_id", "dbo.lu_profilestatus", "id");
            AddForeignKey("dbo.mailboxfolders", "foldertype_id", "dbo.mailboxfoldertypes", "id");
            AddForeignKey("dbo.mailboxfoldertypes", "defaultfolder_id", "dbo.lu_defaultmailboxfolder", "id");
        }
    }
}
