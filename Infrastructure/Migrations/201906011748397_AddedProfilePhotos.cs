namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProfilePhotos : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationUserApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserApplicationUsers", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserApplicationUsers", new[] { "ApplicationUser_Id1" });
            AddColumn("dbo.AspNetUsers", "ImageData", c => c.Binary());
            AddColumn("dbo.AspNetUsers", "ImageMimeType", c => c.String());
            DropTable("dbo.ApplicationUserApplicationUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApplicationUserApplicationUsers",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id1 = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.ApplicationUser_Id1 });
            
            DropColumn("dbo.AspNetUsers", "ImageMimeType");
            DropColumn("dbo.AspNetUsers", "ImageData");
            CreateIndex("dbo.ApplicationUserApplicationUsers", "ApplicationUser_Id1");
            CreateIndex("dbo.ApplicationUserApplicationUsers", "ApplicationUser_Id");
            AddForeignKey("dbo.ApplicationUserApplicationUsers", "ApplicationUser_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ApplicationUserApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
