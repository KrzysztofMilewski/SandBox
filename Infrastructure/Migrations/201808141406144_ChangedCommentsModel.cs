namespace Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangedCommentsModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.Posts");
            DropIndex("dbo.Posts", new[] { "ApplicationUserId" });
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            RenameColumn(table: "dbo.Comments", name: "Post_Id", newName: "PostId");
            AlterColumn("dbo.Posts", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Comments", "PostId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "PostId");
            CreateIndex("dbo.Posts", "ApplicationUserId");
            AddForeignKey("dbo.Posts", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "ApplicationUserId" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            AlterColumn("dbo.Comments", "PostId", c => c.Int());
            AlterColumn("dbo.Posts", "ApplicationUserId", c => c.String(maxLength: 128));
            RenameColumn(table: "dbo.Comments", name: "PostId", newName: "Post_Id");
            CreateIndex("dbo.Comments", "Post_Id");
            CreateIndex("dbo.Posts", "ApplicationUserId");
            AddForeignKey("dbo.Comments", "Post_Id", "dbo.Posts", "Id");
            AddForeignKey("dbo.Posts", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
