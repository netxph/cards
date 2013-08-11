namespace Cards.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateActivity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CardID = c.Int(nullable: false),
                        CurrentAreaID = c.Int(nullable: false),
                        ChangeType = c.Int(nullable: false),
                        Comment = c.String(),
                        StampDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Activities");
        }
    }
}
