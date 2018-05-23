namespace SuperMarketWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20180308alter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stock", "OutCount", c => c.Int());
            AddColumn("dbo.Stock", "OutPrice", c => c.Int());
            AddColumn("dbo.Stock", "OutDate", c => c.DateTime());
            DropTable("dbo.Sell");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Sell",
                c => new
                    {
                        OutProductID = c.Int(nullable: false, identity: true),
                        OutProductName = c.String(nullable: false, maxLength: 25),
                        OutCount = c.Int(),
                        OutPrice = c.Int(),
                        OutDate = c.DateTime(),
                        Supplierid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OutProductID);
            
            DropColumn("dbo.Stock", "OutDate");
            DropColumn("dbo.Stock", "OutPrice");
            DropColumn("dbo.Stock", "OutCount");
        }
    }
}
