namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDBToSettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SettingsViewModels", "DB_Name", c => c.String());
            AddColumn("dbo.SettingsViewModels", "ConnectionString", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SettingsViewModels", "ConnectionString");
            DropColumn("dbo.SettingsViewModels", "DB_Name");
        }
    }
}
