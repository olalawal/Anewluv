namespace NotificationModel.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        MessageTypeLookupId = c.Int(nullable: false),
                        MessageTemplateLookupId = c.Int(nullable: false),
                        MessageSystemAddressID = c.Int(nullable: false),
                        SendingApplication = c.String(),
                        Body = c.String(),
                        Subject = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        Sent = c.Boolean(),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("MessageTypeLookups", t => t.MessageTypeLookupId, cascadeDelete: true)
                .ForeignKey("MessageTemplateLookups", t => t.MessageTemplateLookupId, cascadeDelete: true)
                .ForeignKey("MessageSystemAddresses", t => t.MessageSystemAddressID, cascadeDelete: true)
                .Index(t => t.MessageTypeLookupId)
                .Index(t => t.MessageTemplateLookupId)
                .Index(t => t.MessageSystemAddressID);
            
            CreateTable(
                "MessageTypeLookups",
                c => new
                    {
                        MessageTypeLookupID = c.Int(nullable: false),
                        Description = c.String(),
                        Active = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        RemovalDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.MessageTypeLookupID);
            
            CreateTable(
                "MessageTemplateLookups",
                c => new
                    {
                        MessageTemplateLookupId = c.Int(nullable: false),
                        Description = c.String(),
                        PhysicalLocation = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        RemovalDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RazorTemplateBody = c.String(),
                        StringTemplateSubject = c.String(),
                        StringTemplateBody = c.String(),
                    })
                .PrimaryKey(t => t.MessageTemplateLookupId);
            
            CreateTable(
                "MessageSystemAddresses",
                c => new
                    {
                        MessageSystemAddressID = c.Int(nullable: false, identity: true),
                        MessageSystemAddressTypeLookupID = c.Int(nullable: false),
                        EmailAddress = c.String(),
                        HostIp = c.String(),
                        HostName = c.String(),
                        CreatedBy = c.String(),
                        CredentialUserName = c.String(),
                        CredentialPassword = c.String(),
                        Active = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        RemovalDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.MessageSystemAddressID)
                .ForeignKey("MessageSystemAddressTypeLookups", t => t.MessageSystemAddressTypeLookupID, cascadeDelete: true)
                .Index(t => t.MessageSystemAddressTypeLookupID);
            
            CreateTable(
                "MessageSystemAddressTypeLookups",
                c => new
                    {
                        MessageSystemAddressTypeLookupID = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.MessageSystemAddressTypeLookupID);
            
            CreateTable(
                "MessageAddresses",
                c => new
                    {
                        MessageAddressId = c.Int(nullable: false, identity: true),
                        MessageAddressTypeLookupID = c.Int(nullable: false),
                        EmailAddress = c.String(),
                        Username = c.String(),
                        OtherIdentifer = c.String(),
                        Active = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        RemovalDate = c.DateTime(),
                        Message_MessageId = c.Int(),
                    })
                .PrimaryKey(t => t.MessageAddressId)
                .ForeignKey("MessageAddressTypeLookups", t => t.MessageAddressTypeLookupID, cascadeDelete: true)
                .ForeignKey("Messages", t => t.Message_MessageId)
                .Index(t => t.MessageAddressTypeLookupID)
                .Index(t => t.Message_MessageId);
            
            CreateTable(
                "MessageAddressTypeLookups",
                c => new
                    {
                        MessageAddressTypeLookupID = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.MessageAddressTypeLookupID);
            
        }
        
        public override void Down()
        {
            DropIndex("MessageAddresses", new[] { "Message_MessageId" });
            DropIndex("MessageAddresses", new[] { "MessageAddressTypeLookupID" });
            DropIndex("MessageSystemAddresses", new[] { "MessageSystemAddressTypeLookupID" });
            DropIndex("Messages", new[] { "MessageSystemAddressID" });
            DropIndex("Messages", new[] { "MessageTemplateLookupId" });
            DropIndex("Messages", new[] { "MessageTypeLookupId" });
            DropForeignKey("MessageAddresses", "Message_MessageId", "Messages");
            DropForeignKey("MessageAddresses", "MessageAddressTypeLookupID", "MessageAddressTypeLookups");
            DropForeignKey("MessageSystemAddresses", "MessageSystemAddressTypeLookupID", "MessageSystemAddressTypeLookups");
            DropForeignKey("Messages", "MessageSystemAddressID", "MessageSystemAddresses");
            DropForeignKey("Messages", "MessageTemplateLookupId", "MessageTemplateLookups");
            DropForeignKey("Messages", "MessageTypeLookupId", "MessageTypeLookups");
            DropTable("MessageAddressTypeLookups");
            DropTable("MessageAddresses");
            DropTable("MessageSystemAddressTypeLookups");
            DropTable("MessageSystemAddresses");
            DropTable("MessageTemplateLookups");
            DropTable("MessageTypeLookups");
            DropTable("Messages");
        }
    }
}
