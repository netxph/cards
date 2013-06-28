namespace Cards.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB_Dates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Areas", "CreatedDateUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.Areas", "ModifiedDateUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.Cards", "CreatedDateUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.Cards", "ModifiedDateUtc", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cards", "ModifiedDateUtc");
            DropColumn("dbo.Cards", "CreatedDateUtc");
            DropColumn("dbo.Areas", "ModifiedDateUtc");
            DropColumn("dbo.Areas", "CreatedDateUtc");
        }
    }
}
