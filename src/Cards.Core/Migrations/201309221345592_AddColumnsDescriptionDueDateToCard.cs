namespace Cards.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnsDescriptionDueDateToCard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "Description", c => c.String());
            AddColumn("dbo.Cards", "DueDateUtc", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cards", "DueDateUtc");
            DropColumn("dbo.Cards", "Description");
        }
    }
}
