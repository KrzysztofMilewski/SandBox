namespace SandBox.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSubscriptionModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        PublisherId = c.String(nullable: false, maxLength: 128),
                        SubscriberId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.PublisherId, t.SubscriberId })
                .ForeignKey("dbo.AspNetUsers", t => t.PublisherId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.SubscriberId)
                .Index(t => t.PublisherId)
                .Index(t => t.SubscriberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscriptions", "SubscriberId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Subscriptions", "PublisherId", "dbo.AspNetUsers");
            DropIndex("dbo.Subscriptions", new[] { "SubscriberId" });
            DropIndex("dbo.Subscriptions", new[] { "PublisherId" });
            DropTable("dbo.Subscriptions");
        }
    }
}
