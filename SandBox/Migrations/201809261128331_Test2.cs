namespace SandBox.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subscriptions", "PublisherId", "dbo.AspNetUsers");
            AddForeignKey("dbo.Subscriptions", "PublisherId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscriptions", "PublisherId", "dbo.AspNetUsers");
            AddForeignKey("dbo.Subscriptions", "PublisherId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
