namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetDefaultAlert2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MainViewerAlertsModels", "SettingsViewModels_Id", "dbo.SettingsViewModels");
            DropIndex("dbo.MainViewerAlertsModels", new[] { "SettingsViewModels_Id" });
            DropColumn("dbo.MainViewerAlertsModels", "SettingsViewModels_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MainViewerAlertsModels", "SettingsViewModels_Id", c => c.Long());
            CreateIndex("dbo.MainViewerAlertsModels", "SettingsViewModels_Id");
            AddForeignKey("dbo.MainViewerAlertsModels", "SettingsViewModels_Id", "dbo.SettingsViewModels", "Id");
        }
    }
}
