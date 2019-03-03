namespace Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedUserToPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "ApplicationUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Posts", "ApplicationUser_Id");
            AddForeignKey("dbo.Posts", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Posts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Posts", "ApplicationUser_Id");
            DropColumn("dbo.Posts", "ApplicationUserId");
        }
    }
}
