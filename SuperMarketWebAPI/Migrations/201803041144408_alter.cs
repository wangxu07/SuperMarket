namespace SuperMarketWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Purchase",
                c => new
                    {
                        InProductID = c.Int(nullable: false, identity: true),
                        InProductName = c.String(nullable: false, maxLength: 25),
                        InCount = c.Int(),
                        InPrice = c.Int(),
                        InDate = c.DateTime(),
                        Suppliersid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InProductID)
                .ForeignKey("dbo.Supplier", t => t.Suppliersid, cascadeDelete: true)
                .Index(t => t.Suppliersid);
            
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        SupplierID = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(nullable: false, maxLength: 25),
                        SupplierAddress = c.String(),
                        SuplierEmail = c.String(),
                        SupplierPhone = c.Int(),
                    })
                .PrimaryKey(t => t.SupplierID);
            
            CreateTable(
                "dbo.SProduct",
                c => new
                    {
                        SulProductID = c.Int(nullable: false, identity: true),
                        SulProductName = c.String(nullable: false, maxLength: 25),
                        Remarks = c.String(),
                        Supplierid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SulProductID)
                .ForeignKey("dbo.Supplier", t => t.Supplierid, cascadeDelete: true)
                .Index(t => t.Supplierid);
            
            CreateTable(
                "dbo.Sell",
                c => new
                    {
                        OutProductID = c.Int(nullable: false, identity: true),
                        OutProductName = c.String(nullable: false, maxLength: 25),
                        OutCount = c.Int(),
                        OutPrice = c.Int(),
                        OutDate = c.DateTime(),
                        IsSelling = c.Int(),
                        Supplierid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OutProductID);
            
            CreateTable(
                "dbo.Staff",
                c => new
                    {
                        StaffID = c.Int(nullable: false, identity: true),
                        StaffName = c.String(nullable: false, maxLength: 15),
                        StaffAge = c.String(maxLength: 10),
                        StaffSex = c.Int(nullable: false),
                        StaffAddress = c.String(),
                        StaffEmail = c.String(),
                        CheckInTime = c.DateTime(nullable: false),
                        CheckOutTime = c.DateTime(),
                        StaffBirthday = c.DateTime(),
                        Salary = c.Int(),
                        Level = c.Int(nullable: false),
                        StaffUserName = c.String(nullable: false),
                        StaffPassWord = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StaffID);
            
            CreateTable(
                "dbo.Stock",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 25),
                        Count = c.Int(),
                        Date = c.DateTime(),
                        IsSelling = c.Int(),
                        Supplierid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchase", "Suppliersid", "dbo.Supplier");
            DropForeignKey("dbo.SProduct", "Supplierid", "dbo.Supplier");
            DropIndex("dbo.SProduct", new[] { "Supplierid" });
            DropIndex("dbo.Purchase", new[] { "Suppliersid" });
            DropTable("dbo.Stock");
            DropTable("dbo.Staff");
            DropTable("dbo.Sell");
            DropTable("dbo.SProduct");
            DropTable("dbo.Supplier");
            DropTable("dbo.Purchase");
        }
    }
}
