namespace SandBox.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSubscription : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubscriberId = c.String(maxLength: 128),
                        PublisherId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.PublisherId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.SubscriberId)
                .Index(t => t.SubscriberId)
                .Index(t => t.PublisherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscriptions", "SubscriberId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Subscriptions", "PublisherId", "dbo.AspNetUsers");
            DropIndex("dbo.Subscriptions", new[] { "PublisherId" });
            DropIndex("dbo.Subscriptions", new[] { "SubscriberId" });
            DropTable("dbo.Subscriptions");
        }
    }
}
