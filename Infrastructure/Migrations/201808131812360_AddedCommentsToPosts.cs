namespace Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedCommentsToPosts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ApplicationUserId = c.String(nullable: false, maxLength: 128),
                    Contents = c.String(nullable: false, maxLength: 255),
                    DateAdded = c.DateTime(nullable: false),
                    Post_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.Post_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Comments", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            DropIndex("dbo.Comments", new[] { "ApplicationUserId" });
            DropTable("dbo.Comments");
        }
    }
}
