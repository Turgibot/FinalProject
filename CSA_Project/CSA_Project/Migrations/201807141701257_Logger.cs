namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Logger : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoggerModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        time = c.DateTime(nullable: false),
                        Message = c.String(),
                        Code = c.Int(nullable: false),
                        Key = c.String(),
                        Value = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoggerModels", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.LoggerModels", new[] { "User_Id" });
            DropTable("dbo.LoggerModels");
        }
    }
}
