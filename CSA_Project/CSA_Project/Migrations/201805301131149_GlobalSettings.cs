namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GlobalSettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SettingsViewModels", "EuclidIP", c => c.String());
            AddColumn("dbo.SettingsViewModels", "EuclidMAC", c => c.String());
            AddColumn("dbo.SettingsViewModels", "EuclidPort", c => c.String());
            AddColumn("dbo.SettingsViewModels", "Topic", c => c.String());
            AddColumn("dbo.SettingsViewModels", "ServerIP", c => c.String());
            AddColumn("dbo.SettingsViewModels", "ServerMAC", c => c.String());
            AddColumn("dbo.SettingsViewModels", "ServerPort", c => c.String());
            AddColumn("dbo.SettingsViewModels", "RecordingPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SettingsViewModels", "RecordingPath");
            DropColumn("dbo.SettingsViewModels", "ServerPort");
            DropColumn("dbo.SettingsViewModels", "ServerMAC");
            DropColumn("dbo.SettingsViewModels", "ServerIP");
            DropColumn("dbo.SettingsViewModels", "Topic");
            DropColumn("dbo.SettingsViewModels", "EuclidPort");
            DropColumn("dbo.SettingsViewModels", "EuclidMAC");
            DropColumn("dbo.SettingsViewModels", "EuclidIP");
        }
    }
}
