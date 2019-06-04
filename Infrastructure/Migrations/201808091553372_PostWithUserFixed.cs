namespace Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PostWithUserFixed : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Posts", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Posts", "ApplicationUserId");
            RenameColumn(table: "dbo.Posts", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            AlterColumn("dbo.Posts", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Posts", "ApplicationUserId");
        }

        public override void Down()
        {
            DropIndex("dbo.Posts", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Posts", "ApplicationUserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Posts", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.Posts", "ApplicationUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "ApplicationUser_Id");
        }
    }
}
