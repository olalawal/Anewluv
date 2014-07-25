namespace Nmedia.Infrastructure.Domain.Notification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.addresses",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        addresstype_id = c.Int(nullable: false),
                        emailaddress = c.String(),
                        username = c.String(),
                        otheridentifer = c.String(),
                        active = c.Boolean(nullable: false),
                        creationdate = c.DateTime(),
                        removaldate = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_addresstype", t => t.addresstype_id)
                .Index(t => t.addresstype_id);
            
            CreateTable(
                "dbo.lu_addresstype",
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
                "dbo.messages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        messagetype_id = c.Int(nullable: false),
                        template_id = c.Int(),
                        systemaddress_id = c.Int(nullable: false),
                        sendingapplication = c.String(),
                        content = c.String(),
                        body = c.String(),
                        subject = c.String(),
                        creationdate = c.DateTime(),
                        sendattempts = c.Int(),
                        sent = c.Boolean(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_messagetype", t => t.messagetype_id)
                .ForeignKey("dbo.systemaddresses", t => t.systemaddress_id)
                .ForeignKey("dbo.lu_template", t => t.template_id)
                .Index(t => t.messagetype_id)
                .Index(t => t.template_id)
                .Index(t => t.systemaddress_id);
            
            CreateTable(
                "dbo.lu_messagetype",
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
                "dbo.systemaddresses",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        systemaddresstype_id = c.Int(nullable: false),
                        emailaddress = c.String(),
                        hostip = c.String(),
                        hostname = c.String(),
                        createdby = c.String(),
                        credentialusername = c.String(),
                        credentialpassword = c.String(),
                        active = c.Boolean(),
                        creationdate = c.DateTime(),
                        removaldate = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_systemaddresstype", t => t.systemaddresstype_id)
                .Index(t => t.systemaddresstype_id);
            
            CreateTable(
                "dbo.lu_systemaddresstype",
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
                "dbo.lu_template",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        defaultlocation = c.String(),
                        creationdate = c.DateTime(),
                        removaldate = c.DateTime(),
                        active = c.Boolean(nullable: false),
                        filename_id = c.Int(nullable: false),
                        body_id = c.Int(nullable: false),
                        subject_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_templatebody", t => t.body_id)
                .ForeignKey("dbo.lu_templatefilename", t => t.filename_id)
                .ForeignKey("dbo.lu_templatesubject", t => t.subject_id)
                .Index(t => t.filename_id)
                .Index(t => t.body_id)
                .Index(t => t.subject_id);
            
            CreateTable(
                "dbo.lu_templatebody",
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
                "dbo.lu_templatefilename",
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
                "dbo.lu_templatesubject",
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
                "dbo.lu_news",
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
                "dbo.messagedetails",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        messagetype_id = c.Int(nullable: false),
                        templatebody_id = c.Int(nullable: false),
                        templatesubject_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_messagetype", t => t.messagetype_id)
                .ForeignKey("dbo.lu_templatebody", t => t.templatebody_id)
                .ForeignKey("dbo.lu_templatesubject", t => t.templatesubject_id)
                .Index(t => t.messagetype_id)
                .Index(t => t.templatebody_id)
                .Index(t => t.templatesubject_id);
            
            CreateTable(
                "dbo.notification_addressmessages",
                c => new
                    {
                        recipient_id = c.Int(nullable: false),
                        message_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.recipient_id, t.message_id })
                .ForeignKey("dbo.addresses", t => t.recipient_id, cascadeDelete: true)
                .ForeignKey("dbo.messages", t => t.message_id, cascadeDelete: true)
                .Index(t => t.recipient_id)
                .Index(t => t.message_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.messagedetails", "templatesubject_id", "dbo.lu_templatesubject");
            DropForeignKey("dbo.messagedetails", "templatebody_id", "dbo.lu_templatebody");
            DropForeignKey("dbo.messagedetails", "messagetype_id", "dbo.lu_messagetype");
            DropForeignKey("dbo.notification_addressmessages", "message_id", "dbo.messages");
            DropForeignKey("dbo.notification_addressmessages", "recipient_id", "dbo.addresses");
            DropForeignKey("dbo.messages", "template_id", "dbo.lu_template");
            DropForeignKey("dbo.lu_template", "subject_id", "dbo.lu_templatesubject");
            DropForeignKey("dbo.lu_template", "filename_id", "dbo.lu_templatefilename");
            DropForeignKey("dbo.lu_template", "body_id", "dbo.lu_templatebody");
            DropForeignKey("dbo.messages", "systemaddress_id", "dbo.systemaddresses");
            DropForeignKey("dbo.systemaddresses", "systemaddresstype_id", "dbo.lu_systemaddresstype");
            DropForeignKey("dbo.messages", "messagetype_id", "dbo.lu_messagetype");
            DropForeignKey("dbo.addresses", "addresstype_id", "dbo.lu_addresstype");
            DropIndex("dbo.notification_addressmessages", new[] { "message_id" });
            DropIndex("dbo.notification_addressmessages", new[] { "recipient_id" });
            DropIndex("dbo.messagedetails", new[] { "templatesubject_id" });
            DropIndex("dbo.messagedetails", new[] { "templatebody_id" });
            DropIndex("dbo.messagedetails", new[] { "messagetype_id" });
            DropIndex("dbo.lu_template", new[] { "subject_id" });
            DropIndex("dbo.lu_template", new[] { "body_id" });
            DropIndex("dbo.lu_template", new[] { "filename_id" });
            DropIndex("dbo.systemaddresses", new[] { "systemaddresstype_id" });
            DropIndex("dbo.messages", new[] { "systemaddress_id" });
            DropIndex("dbo.messages", new[] { "template_id" });
            DropIndex("dbo.messages", new[] { "messagetype_id" });
            DropIndex("dbo.addresses", new[] { "addresstype_id" });
            DropTable("dbo.notification_addressmessages");
            DropTable("dbo.messagedetails");
            DropTable("dbo.lu_news");
            DropTable("dbo.lu_templatesubject");
            DropTable("dbo.lu_templatefilename");
            DropTable("dbo.lu_templatebody");
            DropTable("dbo.lu_template");
            DropTable("dbo.lu_systemaddresstype");
            DropTable("dbo.systemaddresses");
            DropTable("dbo.lu_messagetype");
            DropTable("dbo.messages");
            DropTable("dbo.lu_addresstype");
            DropTable("dbo.addresses");
        }
    }
}
