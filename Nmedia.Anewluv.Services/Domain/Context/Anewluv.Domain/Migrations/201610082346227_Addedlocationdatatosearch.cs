namespace Anewluv.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedlocationdatatosearch : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.searchsettings", "selectedcountryname", c => c.String());
            AddColumn("dbo.searchsettings", "selectedcountryid", c => c.Int());
            AddColumn("dbo.searchsettings", "selectedpostalcode", c => c.String());
            AddColumn("dbo.searchsettings", "selectedpostalcodestatus", c => c.Boolean());
            AddColumn("dbo.searchsettings", "selectedcity", c => c.String());
            AddColumn("dbo.searchsettings", "selectedstateprovince", c => c.String());
            AddColumn("dbo.searchsettings", "selectedlongitude", c => c.Double());
            AddColumn("dbo.searchsettings", "selectedlatitude", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.searchsettings", "selectedlatitude");
            DropColumn("dbo.searchsettings", "selectedlongitude");
            DropColumn("dbo.searchsettings", "selectedstateprovince");
            DropColumn("dbo.searchsettings", "selectedcity");
            DropColumn("dbo.searchsettings", "selectedpostalcodestatus");
            DropColumn("dbo.searchsettings", "selectedpostalcode");
            DropColumn("dbo.searchsettings", "selectedcountryid");
            DropColumn("dbo.searchsettings", "selectedcountryname");
        }
    }
}
