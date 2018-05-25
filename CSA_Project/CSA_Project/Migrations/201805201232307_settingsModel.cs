namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class settingsModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SettingsViewModels",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        MaxPeopleAllowed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SettingsViewModels");
        }
    }
}
