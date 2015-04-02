namespace Anewluv.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class profilemetatdataclean : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.profilemetadatas", "profile_id", "dbo.profiledatas");
        }
        
        public override void Down()
        {
            AddForeignKey("dbo.profilemetadatas", "profile_id", "dbo.profiledatas", "profile_id");
        }
    }
}
