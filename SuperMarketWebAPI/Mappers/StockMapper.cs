using SuperMarketWebAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace SuperMarketWebAPI.Mappers
{
    public class StockMapper:EntityTypeConfiguration<Stock>
    {
        public StockMapper()
        {
            this.ToTable("Stock");

            this.HasKey(c => c.ProductID);
            this.Property(c => c.ProductID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.ProductID).IsRequired();

            this.Property(c => c.ProductName).IsRequired();
            this.Property(c => c.ProductName).HasMaxLength(25);

            this.Property(c => c.Count).IsOptional();

            this.Property(c => c.OutCount).IsOptional();

            this.Property(c => c.OutPrice).IsOptional();

            this.Property(c => c.OutDate).IsOptional();

            this.Property(c => c.Date).IsOptional();

            this.Property(c => c.IsSelling).IsOptional();

            this.Property(c => c.Supplierid).IsRequired();


        }
    }
}