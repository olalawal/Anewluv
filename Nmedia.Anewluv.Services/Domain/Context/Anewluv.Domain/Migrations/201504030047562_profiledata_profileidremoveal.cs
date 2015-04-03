namespace Anewluv.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class profiledata_profileidremoveal : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.profiledatas", "profilemetadata_profile_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.profiledatas", "profilemetadata_profile_id", c => c.Int());
        }
    }
}
