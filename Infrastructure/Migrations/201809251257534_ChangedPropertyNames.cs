namespace Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangedPropertyNames : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Comments", name: "ApplicationUserId", newName: "CommentingUserId");
            RenameColumn(table: "dbo.Posts", name: "ApplicationUserId", newName: "PublisherId");
            RenameIndex(table: "dbo.Comments", name: "IX_ApplicationUserId", newName: "IX_CommentingUserId");
            RenameIndex(table: "dbo.Posts", name: "IX_ApplicationUserId", newName: "IX_PublisherId");
        }

        public override void Down()
        {
            RenameIndex(table: "dbo.Posts", name: "IX_PublisherId", newName: "IX_ApplicationUserId");
            RenameIndex(table: "dbo.Comments", name: "IX_CommentingUserId", newName: "IX_ApplicationUserId");
            RenameColumn(table: "dbo.Posts", name: "PublisherId", newName: "ApplicationUserId");
            RenameColumn(table: "dbo.Comments", name: "CommentingUserId", newName: "ApplicationUserId");
        }
    }
}
