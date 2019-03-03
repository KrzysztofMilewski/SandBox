namespace Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedPost : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false, maxLength: 40),
                    Contents = c.String(nullable: false),
                    DatePublished = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.Posts");
        }
    }
}
