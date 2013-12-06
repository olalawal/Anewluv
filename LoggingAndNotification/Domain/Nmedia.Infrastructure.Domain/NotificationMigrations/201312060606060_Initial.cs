namespace Nmedia.Infrastructure.Domain.NotificationMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.notification_address",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        emailaddress = c.String(),
                        username = c.String(),
                        otheridentifer = c.String(),
                        active = c.Boolean(nullable: false),
                        creationdate = c.DateTime(),
                        removaldate = c.DateTime(),
                        addresstype_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.notification_lu_addresstype", t => t.addresstype_id, cascadeDelete: true)
                .Index(t => t.addresstype_id);
            
            CreateTable(
                "dbo.notification_lu_addresstype",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        active = c.Boolean(),
                        creationdate = c.DateTime(),
                        removaldate = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.notification_message",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        sendingapplication = c.String(),
                        content = c.String(),
                        body = c.String(),
                        subject = c.String(),
                        creationdate = c.DateTime(),
                        sendattempts = c.Int(),
                        sent = c.Boolean(),
                        messagetype_id = c.Int(nullable: false),
                        systemaddress_id = c.Int(nullable: false),
                        template_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.notification_lu_messagetype", t => t.messagetype_id, cascadeDelete: true)
                .ForeignKey("dbo.notification_systemaddress", t => t.systemaddress_id, cascadeDelete: true)
                .ForeignKey("dbo.notification_lu_template", t => t.template_id, cascadeDelete: true)
                .Index(t => t.messagetype_id)
                .Index(t => t.systemaddress_id)
                .Index(t => t.template_id);
            
            CreateTable(
                "dbo.notification_lu_messagetype",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        active = c.Boolean(),
                        creationdate = c.DateTime(),
                        removaldate = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.notification_systemaddress",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        emailaddress = c.String(),
                        hostip = c.String(),
                        hostname = c.String(),
                        createdby = c.String(),
                        credentialusername = c.String(),
                        credentialpassword = c.String(),
                        active = c.Boolean(),
                        creationdate = c.DateTime(),
                        removaldate = c.DateTime(),
                        systemaddresstype_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.notification_lu_systemaddresstype", t => t.systemaddresstype_id, cascadeDelete: true)
                .Index(t => t.systemaddresstype_id);
            
            CreateTable(
                "dbo.notification_lu_systemaddresstype",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        active = c.Boolean(),
                        creationdate = c.DateTime(),
                        removaldate = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.notification_lu_template",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        physicallocation = c.String(),
                        filename = c.String(),
                        creationdate = c.DateTime(),
                        removaldate = c.DateTime(),
                        active = c.Boolean(nullable: false),
                        razortemplatebody = c.String(),
                        bodystring_id = c.Int(),
                        subjectstring_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.notification_lu_templatebody", t => t.bodystring_id)
                .ForeignKey("dbo.notification_lu_templatesubject", t => t.subjectstring_id)
                .Index(t => t.bodystring_id)
                .Index(t => t.subjectstring_id);
            
            CreateTable(
                "dbo.notification_lu_templatebody",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        active = c.Boolean(),
                        creationdate = c.DateTime(),
                        removaldate = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.notification_lu_templatesubject",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        active = c.Boolean(),
                        creationdate = c.DateTime(),
                        removaldate = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.notification_lu_news",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        creationdate = c.DateTime(),
                        active = c.Boolean(),
                        removaldate = c.DateTime(),
                        curentmessagenews = c.Boolean(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.addressmessages",
                c => new
                    {
                        recipient_id = c.Int(nullable: false),
                        message_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.recipient_id, t.message_id })
                .ForeignKey("dbo.notification_address", t => t.recipient_id, cascadeDelete: true)
                .ForeignKey("dbo.notification_message", t => t.message_id, cascadeDelete: true)
                .Index(t => t.recipient_id)
                .Index(t => t.message_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.addressmessages", "message_id", "dbo.notification_message");
            DropForeignKey("dbo.addressmessages", "recipient_id", "dbo.notification_address");
            DropForeignKey("dbo.notification_message", "template_id", "dbo.notification_lu_template");
            DropForeignKey("dbo.notification_lu_template", "subjectstring_id", "dbo.notification_lu_templatesubject");
            DropForeignKey("dbo.notification_lu_template", "bodystring_id", "dbo.notification_lu_templatebody");
            DropForeignKey("dbo.notification_message", "systemaddress_id", "dbo.notification_systemaddress");
            DropForeignKey("dbo.notification_systemaddress", "systemaddresstype_id", "dbo.notification_lu_systemaddresstype");
            DropForeignKey("dbo.notification_message", "messagetype_id", "dbo.notification_lu_messagetype");
            DropForeignKey("dbo.notification_address", "addresstype_id", "dbo.notification_lu_addresstype");
            DropIndex("dbo.addressmessages", new[] { "message_id" });
            DropIndex("dbo.addressmessages", new[] { "recipient_id" });
            DropIndex("dbo.notification_message", new[] { "template_id" });
            DropIndex("dbo.notification_lu_template", new[] { "subjectstring_id" });
            DropIndex("dbo.notification_lu_template", new[] { "bodystring_id" });
            DropIndex("dbo.notification_message", new[] { "systemaddress_id" });
            DropIndex("dbo.notification_systemaddress", new[] { "systemaddresstype_id" });
            DropIndex("dbo.notification_message", new[] { "messagetype_id" });
            DropIndex("dbo.notification_address", new[] { "addresstype_id" });
            DropTable("dbo.addressmessages");
            DropTable("dbo.notification_lu_news");
            DropTable("dbo.notification_lu_templatesubject");
            DropTable("dbo.notification_lu_templatebody");
            DropTable("dbo.notification_lu_template");
            DropTable("dbo.notification_lu_systemaddresstype");
            DropTable("dbo.notification_systemaddress");
            DropTable("dbo.notification_lu_messagetype");
            DropTable("dbo.notification_message");
            DropTable("dbo.notification_lu_addresstype");
            DropTable("dbo.notification_address");
        }
    }
}
