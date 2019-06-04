namespace Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DeletedSubs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subscriptions", "PublisherId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Subscriptions", "SubscriberId", "dbo.AspNetUsers");
            DropIndex("dbo.Subscriptions", new[] { "SubscriberId" });
            DropIndex("dbo.Subscriptions", new[] { "PublisherId" });
            DropTable("dbo.Subscriptions");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.Subscriptions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    SubscriberId = c.String(maxLength: 128),
                    PublisherId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateIndex("dbo.Subscriptions", "PublisherId");
            CreateIndex("dbo.Subscriptions", "SubscriberId");
            AddForeignKey("dbo.Subscriptions", "SubscriberId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Subscriptions", "PublisherId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
