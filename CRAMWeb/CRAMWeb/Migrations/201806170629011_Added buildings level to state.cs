namespace CRAMWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedbuildingsleveltostate : DbMigration
    {
        public override void Up()
        {
            AddColumn("KacperWeissErikBurnell.GameStates", "CastleLevel", c => c.Int(nullable: false));
            AddColumn("KacperWeissErikBurnell.GameStates", "FarmsLevel", c => c.Int(nullable: false));
            AddColumn("KacperWeissErikBurnell.GameStates", "LumberjackLevel", c => c.Int(nullable: false));
            AddColumn("KacperWeissErikBurnell.GameStates", "HousingLevel", c => c.Int(nullable: false));
            AddColumn("KacperWeissErikBurnell.GameStates", "MinesLevel", c => c.Int(nullable: false));
            AddColumn("KacperWeissErikBurnell.Users", "Game_Id", c => c.Int());
            CreateIndex("KacperWeissErikBurnell.Users", "Game_Id");
            AddForeignKey("KacperWeissErikBurnell.Users", "Game_Id", "KacperWeissErikBurnell.Games", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("KacperWeissErikBurnell.Users", "Game_Id", "KacperWeissErikBurnell.Games");
            DropIndex("KacperWeissErikBurnell.Users", new[] { "Game_Id" });
            DropColumn("KacperWeissErikBurnell.Users", "Game_Id");
            DropColumn("KacperWeissErikBurnell.GameStates", "MinesLevel");
            DropColumn("KacperWeissErikBurnell.GameStates", "HousingLevel");
            DropColumn("KacperWeissErikBurnell.GameStates", "LumberjackLevel");
            DropColumn("KacperWeissErikBurnell.GameStates", "FarmsLevel");
            DropColumn("KacperWeissErikBurnell.GameStates", "CastleLevel");
        }
    }
}
