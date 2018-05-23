namespace SuperMarketWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20180304_type_alter : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Supplier", "SupplierPhone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Supplier", "SupplierPhone", c => c.Int());
        }
    }
}
