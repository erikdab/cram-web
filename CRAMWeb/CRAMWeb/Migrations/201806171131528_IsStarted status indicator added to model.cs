namespace CRAMWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsStartedstatusindicatoraddedtomodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("KacperWeissErikBurnell.Games", "IsStarted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("KacperWeissErikBurnell.Games", "IsStarted");
        }
    }
}
