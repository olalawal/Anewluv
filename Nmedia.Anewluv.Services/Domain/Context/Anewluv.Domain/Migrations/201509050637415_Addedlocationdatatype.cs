namespace Anewluv.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Spatial;
    
    public partial class Addedlocationdatatype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.profiledatas", "location", c => c.Geography());
        }
        
        public override void Down()
        {
            DropColumn("dbo.profiledatas", "location");
        }
    }
}
