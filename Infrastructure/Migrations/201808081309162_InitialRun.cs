namespace Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialRun : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Nickname", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "BirthDate");
            DropColumn("dbo.AspNetUsers", "Nickname");
        }
    }
}
