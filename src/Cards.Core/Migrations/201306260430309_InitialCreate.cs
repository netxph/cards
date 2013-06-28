namespace Cards.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        AreaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Areas", t => t.AreaID, cascadeDelete: true)
                .Index(t => t.AreaID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Cards", new[] { "AreaID" });
            DropForeignKey("dbo.Cards", "AreaID", "dbo.Areas");
            DropTable("dbo.Cards");
            DropTable("dbo.Areas");
        }
    }
}
