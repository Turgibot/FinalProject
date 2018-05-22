namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetDefaultAlert1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MainViewerViewModels", "Alert_Id", "dbo.MainViewerAlertsModels");
            DropIndex("dbo.MainViewerViewModels", new[] { "Alert_Id" });
            DropColumn("dbo.MainViewerViewModels", "Alert_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MainViewerViewModels", "Alert_Id", c => c.Long());
            CreateIndex("dbo.MainViewerViewModels", "Alert_Id");
            AddForeignKey("dbo.MainViewerViewModels", "Alert_Id", "dbo.MainViewerAlertsModels", "Id");
        }
    }
}
