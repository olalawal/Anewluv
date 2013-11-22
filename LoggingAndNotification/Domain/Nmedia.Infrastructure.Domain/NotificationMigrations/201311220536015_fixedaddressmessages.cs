namespace Nmedia.Infrastructure.Domain.NotificationMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedaddressmessages : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.addressmessages", newName: "notification_addressmessages");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.notification_addressmessages", newName: "addressmessages");
        }
    }
}
