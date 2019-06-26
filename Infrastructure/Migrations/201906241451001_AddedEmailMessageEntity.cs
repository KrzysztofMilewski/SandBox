namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmailMessageEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderId = c.String(nullable: false, maxLength: 128),
                        ReceiverId = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false),
                        Message = c.String(nullable: false),
                        DateSent = c.DateTime(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        RequestDeliveryNote = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ReceiverId)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmailMessages", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EmailMessages", "ReceiverId", "dbo.AspNetUsers");
            DropIndex("dbo.EmailMessages", new[] { "ReceiverId" });
            DropIndex("dbo.EmailMessages", new[] { "SenderId" });
            DropTable("dbo.EmailMessages");
        }
    }
}
