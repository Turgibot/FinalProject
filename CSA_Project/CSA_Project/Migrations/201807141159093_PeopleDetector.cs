namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PeopleDetector : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.PersonDetectors");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PersonDetectors",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ValuesString = c.String(),
                        NumberOfPeople = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
