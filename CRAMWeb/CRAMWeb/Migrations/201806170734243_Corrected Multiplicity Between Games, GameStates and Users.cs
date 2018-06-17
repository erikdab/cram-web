namespace CRAMWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectedMultiplicityBetweenGamesGameStatesandUsers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("KacperWeissErikBurnell.Raids", "AtackingPlayer_Id", "KacperWeissErikBurnell.Users");
            DropForeignKey("KacperWeissErikBurnell.Raids", "DefendingPlayer_Id", "KacperWeissErikBurnell.Users");
            DropForeignKey("KacperWeissErikBurnell.Users", "Game_Id", "KacperWeissErikBurnell.Games");
            DropIndex("KacperWeissErikBurnell.Raids", new[] { "AtackingPlayer_Id" });
            DropIndex("KacperWeissErikBurnell.Raids", new[] { "DefendingPlayer_Id" });
            DropIndex("KacperWeissErikBurnell.Users", new[] { "Game_Id" });
            RenameColumn(table: "KacperWeissErikBurnell.Raids", name: "GameState_Id", newName: "DefendingGameState_Id");
            RenameColumn(table: "KacperWeissErikBurnell.Raids", name: "GameState_Id1", newName: "AttackingGameState_Id");
            RenameIndex(table: "KacperWeissErikBurnell.Raids", name: "IX_GameState_Id1", newName: "IX_AttackingGameState_Id");
            RenameIndex(table: "KacperWeissErikBurnell.Raids", name: "IX_GameState_Id", newName: "IX_DefendingGameState_Id");
            CreateTable(
                "KacperWeissErikBurnell.ApplicationUserGames",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Game_Id })
                .ForeignKey("KacperWeissErikBurnell.Users", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("KacperWeissErikBurnell.Games", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Game_Id);
            
            AddColumn("KacperWeissErikBurnell.GameStates", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("KacperWeissErikBurnell.GameStates", "User_Id");
            AddForeignKey("KacperWeissErikBurnell.GameStates", "User_Id", "KacperWeissErikBurnell.Users", "Id");
            DropColumn("KacperWeissErikBurnell.Raids", "AtackingPlayer_Id");
            DropColumn("KacperWeissErikBurnell.Raids", "DefendingPlayer_Id");
            DropColumn("KacperWeissErikBurnell.Users", "Game_Id");
        }
        
        public override void Down()
        {
            AddColumn("KacperWeissErikBurnell.Users", "Game_Id", c => c.Int());
            AddColumn("KacperWeissErikBurnell.Raids", "DefendingPlayer_Id", c => c.String(maxLength: 128));
            AddColumn("KacperWeissErikBurnell.Raids", "AtackingPlayer_Id", c => c.String(maxLength: 128));
            DropForeignKey("KacperWeissErikBurnell.GameStates", "User_Id", "KacperWeissErikBurnell.Users");
            DropForeignKey("KacperWeissErikBurnell.ApplicationUserGames", "Game_Id", "KacperWeissErikBurnell.Games");
            DropForeignKey("KacperWeissErikBurnell.ApplicationUserGames", "ApplicationUser_Id", "KacperWeissErikBurnell.Users");
            DropIndex("KacperWeissErikBurnell.ApplicationUserGames", new[] { "Game_Id" });
            DropIndex("KacperWeissErikBurnell.ApplicationUserGames", new[] { "ApplicationUser_Id" });
            DropIndex("KacperWeissErikBurnell.GameStates", new[] { "User_Id" });
            DropColumn("KacperWeissErikBurnell.GameStates", "User_Id");
            DropTable("KacperWeissErikBurnell.ApplicationUserGames");
            RenameIndex(table: "KacperWeissErikBurnell.Raids", name: "IX_DefendingGameState_Id", newName: "IX_GameState_Id");
            RenameIndex(table: "KacperWeissErikBurnell.Raids", name: "IX_AttackingGameState_Id", newName: "IX_GameState_Id1");
            RenameColumn(table: "KacperWeissErikBurnell.Raids", name: "AttackingGameState_Id", newName: "GameState_Id1");
            RenameColumn(table: "KacperWeissErikBurnell.Raids", name: "DefendingGameState_Id", newName: "GameState_Id");
            CreateIndex("KacperWeissErikBurnell.Users", "Game_Id");
            CreateIndex("KacperWeissErikBurnell.Raids", "DefendingPlayer_Id");
            CreateIndex("KacperWeissErikBurnell.Raids", "AtackingPlayer_Id");
            AddForeignKey("KacperWeissErikBurnell.Users", "Game_Id", "KacperWeissErikBurnell.Games", "Id");
            AddForeignKey("KacperWeissErikBurnell.Raids", "DefendingPlayer_Id", "KacperWeissErikBurnell.Users", "Id");
            AddForeignKey("KacperWeissErikBurnell.Raids", "AtackingPlayer_Id", "KacperWeissErikBurnell.Users", "Id");
        }
    }
}
