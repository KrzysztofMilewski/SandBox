namespace Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SomethingBroken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subscriptions", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Subscriptions", "ApplicationUser_Id");
            AddForeignKey("dbo.Subscriptions", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Subscriptions", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Subscriptions", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Subscriptions", "ApplicationUser_Id");
        }
    }
}
