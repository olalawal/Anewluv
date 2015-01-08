namespace Anewluv.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.profileactivities", "profileactivitygeodata_id");
            RenameColumn(table: "dbo.profileactivities", name: "profileactivitygeodata_id1", newName: "profileactivitygeodata_id");
            RenameIndex(table: "dbo.profileactivities", name: "IX_profileactivitygeodata_id1", newName: "IX_profileactivitygeodata_id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.profileactivities", name: "IX_profileactivitygeodata_id", newName: "IX_profileactivitygeodata_id1");
            RenameColumn(table: "dbo.profileactivities", name: "profileactivitygeodata_id", newName: "profileactivitygeodata_id1");
            AddColumn("dbo.profileactivities", "profileactivitygeodata_id", c => c.Int());
        }
    }
}
