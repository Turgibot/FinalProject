namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSelectorModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SelectorModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SelectedIndex = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SelectorModels");
        }
    }
}
