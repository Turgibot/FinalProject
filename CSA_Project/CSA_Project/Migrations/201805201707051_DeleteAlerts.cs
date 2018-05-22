namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteAlerts : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.MainViewerAlertsModels");
        }
        
        public override void Down()
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
            
        }
    }
}
