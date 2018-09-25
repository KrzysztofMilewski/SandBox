namespace SandBox.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Comments", new[] { "CommentingUserId" });
            AlterColumn("dbo.Comments", "CommentingUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Comments", "CommentingUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "CommentingUserId" });
            AlterColumn("dbo.Comments", "CommentingUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Comments", "CommentingUserId");
        }
    }
}
