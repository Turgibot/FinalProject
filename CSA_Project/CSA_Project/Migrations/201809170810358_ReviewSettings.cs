namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewSettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SettingsViewModels", "CameraTopic", c => c.String());
            AddColumn("dbo.SettingsViewModels", "PeopleTopic", c => c.String());
            AddColumn("dbo.SettingsViewModels", "DrowsinessTopic", c => c.String());
            AddColumn("dbo.SettingsViewModels", "PanicTopic", c => c.String());
            DropColumn("dbo.SettingsViewModels", "Topic");
            DropColumn("dbo.SettingsViewModels", "RecordingPath");
            DropColumn("dbo.SettingsViewModels", "Streamer");
            DropColumn("dbo.SettingsViewModels", "Python27");
            DropColumn("dbo.SettingsViewModels", "Python34");
            DropColumn("dbo.SettingsViewModels", "NN_Model");
            DropColumn("dbo.SettingsViewModels", "NN_Weights");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SettingsViewModels", "NN_Weights", c => c.String());
            AddColumn("dbo.SettingsViewModels", "NN_Model", c => c.String());
            AddColumn("dbo.SettingsViewModels", "Python34", c => c.String());
            AddColumn("dbo.SettingsViewModels", "Python27", c => c.String());
            AddColumn("dbo.SettingsViewModels", "Streamer", c => c.String());
            AddColumn("dbo.SettingsViewModels", "RecordingPath", c => c.String());
            AddColumn("dbo.SettingsViewModels", "Topic", c => c.String());
            DropColumn("dbo.SettingsViewModels", "PanicTopic");
            DropColumn("dbo.SettingsViewModels", "DrowsinessTopic");
            DropColumn("dbo.SettingsViewModels", "PeopleTopic");
            DropColumn("dbo.SettingsViewModels", "CameraTopic");
        }
    }
}
