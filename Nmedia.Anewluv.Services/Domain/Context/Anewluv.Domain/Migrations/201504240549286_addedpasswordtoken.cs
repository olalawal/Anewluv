namespace Anewluv.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedpasswordtoken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.profiles", "passwordresettoken", c => c.String());
            AddColumn("dbo.profiles", "passwordresetwindow", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.profiles", "passwordresetwindow");
            DropColumn("dbo.profiles", "passwordresettoken");
        }
    }
}
