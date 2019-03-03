namespace Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedEditInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "LastTimeEdited", c => c.DateTime());
            AddColumn("dbo.Posts", "NumberOfEdits", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Posts", "NumberOfEdits");
            DropColumn("dbo.Posts", "LastTimeEdited");
        }
    }
}
