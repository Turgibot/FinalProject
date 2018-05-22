namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSettingsIDType : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.SettingsViewModels");
            AlterColumn("dbo.SettingsViewModels", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.SettingsViewModels", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.SettingsViewModels");
            AlterColumn("dbo.SettingsViewModels", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.SettingsViewModels", "Id");
        }
    }
}
