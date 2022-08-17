namespace Article.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 4000),
                        Body = c.String(maxLength: 4000),
                        AdditionDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        State = c.Int(nullable: false),
                        RejectedReason = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Articles_KeyWords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleId = c.Int(nullable: false),
                        KeyWordsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.Keywords", t => t.KeyWordsId, cascadeDelete: true)
                .Index(t => t.ArticleId)
                .Index(t => t.KeyWordsId);
            
            CreateTable(
                "dbo.Keywords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleId = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Body = c.String(maxLength: 4000),
                        AdditionDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ArticleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(maxLength: 4000),
                        SecurityStamp = c.String(maxLength: 4000),
                        PhoneNumber = c.String(maxLength: 4000),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 256),
                        CreationDate = c.DateTime(nullable: false),
                        Location = c.String(maxLength: 256),
                        IsActivated = c.Boolean(nullable: false),
                        FirebaseToken = c.String(maxLength: 256),
                        Facebook = c.String(maxLength: 500),
                        ProfileImage = c.String(maxLength: 800),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.UsersArticles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleId = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Role = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ArticleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Claim",
                c => new
                    {
                        ClaimId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        ClaimType = c.String(maxLength: 4000),
                        ClaimValue = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.ClaimId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ExternalLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleId = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Stars = c.Int(nullable: false),
                        AdditionDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ArticleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Readers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleId = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ArticleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 2),
                        ArabicName = c.String(nullable: false, maxLength: 100),
                        EnglishName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        RoleId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.Readers", "UserId", "dbo.User");
            DropForeignKey("dbo.Readers", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Ratings", "UserId", "dbo.User");
            DropForeignKey("dbo.Ratings", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.ExternalLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.Claim", "UserId", "dbo.User");
            DropForeignKey("dbo.UsersArticles", "UserId", "dbo.User");
            DropForeignKey("dbo.UsersArticles", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Comments", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Articles", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Articles_KeyWords", "KeyWordsId", "dbo.Keywords");
            DropForeignKey("dbo.Articles_KeyWords", "ArticleId", "dbo.Articles");
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.Readers", new[] { "UserId" });
            DropIndex("dbo.Readers", new[] { "ArticleId" });
            DropIndex("dbo.Ratings", new[] { "UserId" });
            DropIndex("dbo.Ratings", new[] { "ArticleId" });
            DropIndex("dbo.ExternalLogin", new[] { "UserId" });
            DropIndex("dbo.Claim", new[] { "UserId" });
            DropIndex("dbo.UsersArticles", new[] { "UserId" });
            DropIndex("dbo.UsersArticles", new[] { "ArticleId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "ArticleId" });
            DropIndex("dbo.Articles_KeyWords", new[] { "KeyWordsId" });
            DropIndex("dbo.Articles_KeyWords", new[] { "ArticleId" });
            DropIndex("dbo.Articles", new[] { "CategoryId" });
            DropTable("dbo.UserRole");
            DropTable("dbo.Language");
            DropTable("dbo.Role");
            DropTable("dbo.Readers");
            DropTable("dbo.Ratings");
            DropTable("dbo.ExternalLogin");
            DropTable("dbo.Claim");
            DropTable("dbo.UsersArticles");
            DropTable("dbo.User");
            DropTable("dbo.Comments");
            DropTable("dbo.Category");
            DropTable("dbo.Keywords");
            DropTable("dbo.Articles_KeyWords");
            DropTable("dbo.Articles");
        }
    }
}
