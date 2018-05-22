namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MainViewer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MainViewerAlertsModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AlertType = c.String(),
                        Message = c.String(),
                        Code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MainViewerViewModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        InferenceResult = c.Int(nullable: false),
                        MaxPeopleAllowed = c.Int(nullable: false),
                        Alert_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MainViewerAlertsModels", t => t.Alert_Id)
                .Index(t => t.Alert_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MainViewerViewModels", "Alert_Id", "dbo.MainViewerAlertsModels");
            DropIndex("dbo.MainViewerViewModels", new[] { "Alert_Id" });
            DropTable("dbo.MainViewerViewModels");
            DropTable("dbo.MainViewerAlertsModels");
        }
    }
}
