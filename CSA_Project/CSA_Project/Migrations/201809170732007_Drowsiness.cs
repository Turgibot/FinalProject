namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Drowsiness : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DetectDrowsinesses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsAwake = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DetectDrowsinesses");
        }
    }
}
