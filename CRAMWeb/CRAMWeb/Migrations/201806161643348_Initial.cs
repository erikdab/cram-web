namespace CRAMWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "KacperWeissErikBurnell.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameName = c.String(maxLength: 32),
                        MaxPlayers = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "KacperWeissErikBurnell.GameStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Food = c.Int(nullable: false),
                        Wood = c.Int(nullable: false),
                        Stone = c.Int(nullable: false),
                        Gold = c.Int(nullable: false),
                        Soldiers = c.Int(nullable: false),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("KacperWeissErikBurnell.Games", t => t.Game_Id)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "KacperWeissErikBurnell.Raids",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Soldiers = c.Int(nullable: false),
                        Food = c.Int(nullable: false),
                        Wood = c.Int(nullable: false),
                        Stone = c.Int(nullable: false),
                        Gold = c.Int(nullable: false),
                        AtackingPlayer_Id = c.String(maxLength: 128),
                        DefendingPlayer_Id = c.String(maxLength: 128),
                        GameState_Id = c.Int(),
                        GameState_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("KacperWeissErikBurnell.Users", t => t.AtackingPlayer_Id)
                .ForeignKey("KacperWeissErikBurnell.Users", t => t.DefendingPlayer_Id)
                .ForeignKey("KacperWeissErikBurnell.GameStates", t => t.GameState_Id)
                .ForeignKey("KacperWeissErikBurnell.GameStates", t => t.GameState_Id1)
                .Index(t => t.AtackingPlayer_Id)
                .Index(t => t.DefendingPlayer_Id)
                .Index(t => t.GameState_Id)
                .Index(t => t.GameState_Id1);
            
            CreateTable(
                "KacperWeissErikBurnell.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "KacperWeissErikBurnell.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("KacperWeissErikBurnell.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "KacperWeissErikBurnell.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("KacperWeissErikBurnell.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "KacperWeissErikBurnell.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("KacperWeissErikBurnell.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("KacperWeissErikBurnell.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "KacperWeissErikBurnell.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("KacperWeissErikBurnell.UserRoles", "RoleId", "KacperWeissErikBurnell.Roles");
            DropForeignKey("KacperWeissErikBurnell.GameStates", "Game_Id", "KacperWeissErikBurnell.Games");
            DropForeignKey("KacperWeissErikBurnell.Raids", "GameState_Id1", "KacperWeissErikBurnell.GameStates");
            DropForeignKey("KacperWeissErikBurnell.Raids", "GameState_Id", "KacperWeissErikBurnell.GameStates");
            DropForeignKey("KacperWeissErikBurnell.Raids", "DefendingPlayer_Id", "KacperWeissErikBurnell.Users");
            DropForeignKey("KacperWeissErikBurnell.Raids", "AtackingPlayer_Id", "KacperWeissErikBurnell.Users");
            DropForeignKey("KacperWeissErikBurnell.UserRoles", "UserId", "KacperWeissErikBurnell.Users");
            DropForeignKey("KacperWeissErikBurnell.UserLogins", "UserId", "KacperWeissErikBurnell.Users");
            DropForeignKey("KacperWeissErikBurnell.UserClaims", "UserId", "KacperWeissErikBurnell.Users");
            DropIndex("KacperWeissErikBurnell.Roles", "RoleNameIndex");
            DropIndex("KacperWeissErikBurnell.UserRoles", new[] { "RoleId" });
            DropIndex("KacperWeissErikBurnell.UserRoles", new[] { "UserId" });
            DropIndex("KacperWeissErikBurnell.UserLogins", new[] { "UserId" });
            DropIndex("KacperWeissErikBurnell.UserClaims", new[] { "UserId" });
            DropIndex("KacperWeissErikBurnell.Users", "UserNameIndex");
            DropIndex("KacperWeissErikBurnell.Raids", new[] { "GameState_Id1" });
            DropIndex("KacperWeissErikBurnell.Raids", new[] { "GameState_Id" });
            DropIndex("KacperWeissErikBurnell.Raids", new[] { "DefendingPlayer_Id" });
            DropIndex("KacperWeissErikBurnell.Raids", new[] { "AtackingPlayer_Id" });
            DropIndex("KacperWeissErikBurnell.GameStates", new[] { "Game_Id" });
            DropTable("KacperWeissErikBurnell.Roles");
            DropTable("KacperWeissErikBurnell.UserRoles");
            DropTable("KacperWeissErikBurnell.UserLogins");
            DropTable("KacperWeissErikBurnell.UserClaims");
            DropTable("KacperWeissErikBurnell.Users");
            DropTable("KacperWeissErikBurnell.Raids");
            DropTable("KacperWeissErikBurnell.GameStates");
            DropTable("KacperWeissErikBurnell.Games");
        }
    }
}
