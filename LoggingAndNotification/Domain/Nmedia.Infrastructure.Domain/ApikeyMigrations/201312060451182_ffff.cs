namespace Nmedia.Infrastructure.Domain.ApikeyMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ffff : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.api_apicall", newName: "api_apicalls");
            RenameTable(name: "dbo.api_apikey", newName: "api_apikeys");
            RenameTable(name: "dbo.api_user", newName: "api_users");
            RenameTable(name: "dbo.userapikey", newName: "api_userapikey");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.api_userapikey", newName: "userapikey");
            RenameTable(name: "dbo.api_users", newName: "api_user");
            RenameTable(name: "dbo.api_apikeys", newName: "api_apikey");
            RenameTable(name: "dbo.api_apicalls", newName: "api_apicall");
        }
    }
}
