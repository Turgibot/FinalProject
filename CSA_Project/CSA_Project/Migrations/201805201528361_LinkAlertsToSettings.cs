namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkAlertsToSettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MainViewerAlertsModels", "SettingsViewModels_Id", c => c.Long());
            CreateIndex("dbo.MainViewerAlertsModels", "SettingsViewModels_Id");
            AddForeignKey("dbo.MainViewerAlertsModels", "SettingsViewModels_Id", "dbo.SettingsViewModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MainViewerAlertsModels", "SettingsViewModels_Id", "dbo.SettingsViewModels");
            DropIndex("dbo.MainViewerAlertsModels", new[] { "SettingsViewModels_Id" });
            DropColumn("dbo.MainViewerAlertsModels", "SettingsViewModels_Id");
        }
    }
}
