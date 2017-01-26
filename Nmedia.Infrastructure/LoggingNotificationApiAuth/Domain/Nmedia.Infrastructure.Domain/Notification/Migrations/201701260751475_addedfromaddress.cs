namespace Nmedia.Infrastructure.Domain.Notification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedfromaddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.lu_application", "fromemailaddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.lu_application", "fromemailaddress");
        }
    }
}
