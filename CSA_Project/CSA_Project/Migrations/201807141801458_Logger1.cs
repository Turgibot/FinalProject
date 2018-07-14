namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Logger1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LoggerModels", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.LoggerModels", new[] { "User_Id" });
            AddColumn("dbo.LoggerModels", "DateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.LoggerModels", "Email", c => c.String());
            DropColumn("dbo.LoggerModels", "time");
            DropColumn("dbo.LoggerModels", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LoggerModels", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.LoggerModels", "time", c => c.DateTime(nullable: false));
            DropColumn("dbo.LoggerModels", "Email");
            DropColumn("dbo.LoggerModels", "DateTime");
            CreateIndex("dbo.LoggerModels", "User_Id");
            AddForeignKey("dbo.LoggerModels", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
