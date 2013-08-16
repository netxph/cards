namespace Cards.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableLabel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Labels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Color = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Labels");
        }
    }
}
