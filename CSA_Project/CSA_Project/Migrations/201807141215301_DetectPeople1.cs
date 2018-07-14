namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DetectPeople1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DetectPeoples",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ValuesString = c.String(),
                        NumberOfPeople = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DetectPeoples");
        }
    }
}
