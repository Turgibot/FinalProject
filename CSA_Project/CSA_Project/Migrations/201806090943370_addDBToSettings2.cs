namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDBToSettings2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SettingsViewModels", "NN_Model", c => c.String());
            AddColumn("dbo.SettingsViewModels", "NN_Weights", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SettingsViewModels", "NN_Weights");
            DropColumn("dbo.SettingsViewModels", "NN_Model");
        }
    }
}
