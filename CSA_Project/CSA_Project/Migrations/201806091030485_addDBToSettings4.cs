namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDBToSettings4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SettingsViewModels", "Python27", c => c.String());
            AddColumn("dbo.SettingsViewModels", "Python34", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SettingsViewModels", "Python34");
            DropColumn("dbo.SettingsViewModels", "Python27");
        }
    }
}
