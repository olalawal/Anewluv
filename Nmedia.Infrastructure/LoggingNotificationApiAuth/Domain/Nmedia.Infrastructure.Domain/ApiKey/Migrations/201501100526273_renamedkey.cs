namespace Nmedia.Infrastructure.Domain.Apikey.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renamedkey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.apikeys", "keyvalue", c => c.Guid(nullable: false));
            DropColumn("dbo.apikeys", "key");
        }
        
        public override void Down()
        {
            AddColumn("dbo.apikeys", "key", c => c.Guid(nullable: false));
            DropColumn("dbo.apikeys", "keyvalue");
        }
    }
}
