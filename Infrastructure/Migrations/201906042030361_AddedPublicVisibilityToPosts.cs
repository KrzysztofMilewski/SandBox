namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPublicVisibilityToPosts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "PubliclyVisible", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "PubliclyVisible");
        }
    }
}
