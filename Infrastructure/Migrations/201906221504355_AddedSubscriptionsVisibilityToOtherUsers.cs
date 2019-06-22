namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSubscriptionsVisibilityToOtherUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SubscriptionsVisibility", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SubscriptionsVisibility");
        }
    }
}
