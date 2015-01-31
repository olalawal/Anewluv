namespace Anewluv.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationFromJohn : DbMigration
    {
        public override void Up()
        {
          
            AddColumn("dbo.profiles", "apikey", c => c.Guid(nullable: false));
            AddColumn("dbo.profileactivities", "apikey", c => c.Guid(nullable: false));
           
        }
        
        public override void Down()
        {
        
            DropColumn("dbo.profileactivities", "apikey");
            DropColumn("dbo.profiles", "apikey");
           
        }
    }
}
