namespace SuperMarketWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20180306alter : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Sell", "IsSelling");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sell", "IsSelling", c => c.Int());
        }
    }
}
