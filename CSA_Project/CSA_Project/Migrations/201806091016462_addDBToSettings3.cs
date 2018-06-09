namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDBToSettings3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SettingsViewModels", "Streamer", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SettingsViewModels", "Streamer");
        }
    }
}
