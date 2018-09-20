namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDetectPanic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DetectPanics",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ValuesString = c.String(),
                        IsPistol = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DetectPanics");
        }
    }
}
