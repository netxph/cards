namespace Cards.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnIsActiveToCardAndArea : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Areas", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cards", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cards", "IsActive");
            DropColumn("dbo.Areas", "IsActive");
        }
    }
}
