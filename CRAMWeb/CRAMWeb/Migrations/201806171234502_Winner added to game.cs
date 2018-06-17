namespace CRAMWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Winneraddedtogame : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("KacperWeissErikBurnell.ApplicationUserGames", "ApplicationUser_Id", "KacperWeissErikBurnell.Users");
            DropForeignKey("KacperWeissErikBurnell.ApplicationUserGames", "Game_Id", "KacperWeissErikBurnell.Games");
            DropIndex("KacperWeissErikBurnell.ApplicationUserGames", new[] { "ApplicationUser_Id" });
            DropIndex("KacperWeissErikBurnell.ApplicationUserGames", new[] { "Game_Id" });
            AddColumn("KacperWeissErikBurnell.Games", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("KacperWeissErikBurnell.Games", "Winner_Id", c => c.String(maxLength: 128));
            AddColumn("KacperWeissErikBurnell.Users", "Game_Id", c => c.Int());
            CreateIndex("KacperWeissErikBurnell.Games", "ApplicationUser_Id");
            CreateIndex("KacperWeissErikBurnell.Games", "Winner_Id");
            CreateIndex("KacperWeissErikBurnell.Users", "Game_Id");
            AddForeignKey("KacperWeissErikBurnell.Games", "ApplicationUser_Id", "KacperWeissErikBurnell.Users", "Id");
            AddForeignKey("KacperWeissErikBurnell.Users", "Game_Id", "KacperWeissErikBurnell.Games", "Id");
            AddForeignKey("KacperWeissErikBurnell.Games", "Winner_Id", "KacperWeissErikBurnell.Users", "Id");
            DropTable("KacperWeissErikBurnell.ApplicationUserGames");
        }
        
        public override void Down()
        {
            CreateTable(
                "KacperWeissErikBurnell.ApplicationUserGames",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Game_Id });
            
            DropForeignKey("KacperWeissErikBurnell.Games", "Winner_Id", "KacperWeissErikBurnell.Users");
            DropForeignKey("KacperWeissErikBurnell.Users", "Game_Id", "KacperWeissErikBurnell.Games");
            DropForeignKey("KacperWeissErikBurnell.Games", "ApplicationUser_Id", "KacperWeissErikBurnell.Users");
            DropIndex("KacperWeissErikBurnell.Users", new[] { "Game_Id" });
            DropIndex("KacperWeissErikBurnell.Games", new[] { "Winner_Id" });
            DropIndex("KacperWeissErikBurnell.Games", new[] { "ApplicationUser_Id" });
            DropColumn("KacperWeissErikBurnell.Users", "Game_Id");
            DropColumn("KacperWeissErikBurnell.Games", "Winner_Id");
            DropColumn("KacperWeissErikBurnell.Games", "ApplicationUser_Id");
            CreateIndex("KacperWeissErikBurnell.ApplicationUserGames", "Game_Id");
            CreateIndex("KacperWeissErikBurnell.ApplicationUserGames", "ApplicationUser_Id");
            AddForeignKey("KacperWeissErikBurnell.ApplicationUserGames", "Game_Id", "KacperWeissErikBurnell.Games", "Id", cascadeDelete: true);
            AddForeignKey("KacperWeissErikBurnell.ApplicationUserGames", "ApplicationUser_Id", "KacperWeissErikBurnell.Users", "Id", cascadeDelete: true);
        }
    }
}
