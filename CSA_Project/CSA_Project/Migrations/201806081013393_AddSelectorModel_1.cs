namespace CSA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSelectorModel_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SelectorModels", "SelectedValue", c => c.String());
            DropColumn("dbo.SelectorModels", "SelectedIndex");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SelectorModels", "SelectedIndex", c => c.Int(nullable: false));
            DropColumn("dbo.SelectorModels", "SelectedValue");
        }
    }
}
