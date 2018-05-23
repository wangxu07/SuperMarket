using SuperMarketWebAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace SuperMarketWebAPI.Mappers
{
    public class PurchaseMapper:EntityTypeConfiguration<Purchase>
    {
        public PurchaseMapper()
        {
            this.ToTable("Purchase");

            this.HasKey(c => c.InProductID);
            this.Property(c => c.InProductID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.InProductID).IsRequired();

            this.Property(c => c.InProductName).IsRequired();
            this.Property(c => c.InProductName).HasMaxLength(25);

            this.Property(c => c.InCount).IsOptional();

            this.Property(c => c.InPrice).IsOptional();

            this.Property(c => c.InDate).IsOptional();

            this.Property(c => c.Suppliersid).IsRequired();

            this.HasRequired(c => c.Supplier).WithMany(c => c.purchases).HasForeignKey(c => c.Suppliersid).WillCascadeOnDelete(true);

        }
    }
}