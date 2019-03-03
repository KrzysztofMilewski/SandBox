namespace Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Dunno : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.Posts", "ApplicationUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Posts", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Posts", "ApplicationUser_Id");
            AddForeignKey("dbo.Posts", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Posts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.Posts", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Posts", "ApplicationUserId");
            CreateIndex("dbo.Posts", "ApplicationUser_Id");
            AddForeignKey("dbo.Posts", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
