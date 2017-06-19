namespace TestTask.Shortener.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0001_Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OriginalLink = c.String(nullable: false),
                        ClickCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserLinks",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        LinkId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        ClickCount = c.Int(nullable: false),
                        ShortLink = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => new { t.UserId, t.LinkId })
                .ForeignKey("dbo.Links", t => t.LinkId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.LinkId)
                .Index(t => t.ShortLink, unique: true);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShortenerUserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ShortenerUserId, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserLinks", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLinks", "LinkId", "dbo.Links");
            DropIndex("dbo.Users", new[] { "ShortenerUserId" });
            DropIndex("dbo.UserLinks", new[] { "ShortLink" });
            DropIndex("dbo.UserLinks", new[] { "LinkId" });
            DropIndex("dbo.UserLinks", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserLinks");
            DropTable("dbo.Links");
        }
    }
}
