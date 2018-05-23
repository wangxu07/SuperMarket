using SuperMarketWebAPI.Mappers;
using SuperMarketWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SuperMarketWebAPI
{
    public class SuperMarketWebAPIContext:DbContext
    {
        public SuperMarketWebAPIContext()
            : base("SuperMarketWebAPIContext")
        {

        }
        public IDbSet<Staff> Staffs { get; set; }
        public IDbSet<Supplier> Suppliers { get; set; }
        public IDbSet<Stock> Stocks { get; set; }
        public IDbSet<Purchase> Purchases { get; set; }
       
        public IDbSet<SulProduct> SulProducts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new StaffMapper());
            //modelBuilder.Entity<Staff>();
            modelBuilder.Configurations.Add(new SupplierMapper());
            //modelBuilder.Entity<Supplier>();
            modelBuilder.Configurations.Add(new StockMapper());
            //modelBuilder.Entity<Stock>();
            modelBuilder.Configurations.Add(new PurchaseMapper());
           // modelBuilder.Entity<Purchase>();
            

            // modelBuilder.Entity<Shipment>();
            modelBuilder.Configurations.Add(new SulProductMapper());


        }
    }
}