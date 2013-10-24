namespace Cards.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailToAccounts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "AssignedTo_ID", c => c.Int());
            AddForeignKey("dbo.Cards", "AssignedTo_ID", "dbo.Accounts", "ID");
            CreateIndex("dbo.Cards", "AssignedTo_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Cards", new[] { "AssignedTo_ID" });
            DropForeignKey("dbo.Cards", "AssignedTo_ID", "dbo.Accounts");
            DropColumn("dbo.Cards", "AssignedTo_ID");
        }
    }
}
