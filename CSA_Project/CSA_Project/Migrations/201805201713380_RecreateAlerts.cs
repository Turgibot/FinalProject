namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecreateAlerts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlertsModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AlertType = c.String(nullable: false),
                        Message = c.String(nullable: false),
                        Code = c.Int(nullable: false),
                        SettingsViewModels_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SettingsViewModels", t => t.SettingsViewModels_Id)
                .Index(t => t.SettingsViewModels_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlertsModels", "SettingsViewModels_Id", "dbo.SettingsViewModels");
            DropIndex("dbo.AlertsModels", new[] { "SettingsViewModels_Id" });
            DropTable("dbo.AlertsModels");
        }
    }
}
