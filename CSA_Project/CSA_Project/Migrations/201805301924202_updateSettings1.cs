namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateSettings1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AlertsModels", "SettingsViewModels_Id", "dbo.SettingsViewModels");
            DropIndex("dbo.AlertsModels", new[] { "SettingsViewModels_Id" });
            DropColumn("dbo.AlertsModels", "SettingsViewModels_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AlertsModels", "SettingsViewModels_Id", c => c.Long());
            CreateIndex("dbo.AlertsModels", "SettingsViewModels_Id");
            AddForeignKey("dbo.AlertsModels", "SettingsViewModels_Id", "dbo.SettingsViewModels", "Id");
        }
    }
}
