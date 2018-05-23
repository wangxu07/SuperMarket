using SuperMarketWebAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace SuperMarketWebAPI.Mappers
{
    public class SupplierMapper:EntityTypeConfiguration<Supplier>
    {
        public SupplierMapper()
        {
            this.ToTable("Supplier");

            this.HasKey(c => c.SupplierID);
            this.Property(c => c.SupplierID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.SupplierID).IsRequired();

            this.Property(c => c.SupplierName).IsRequired();
            this.Property(c => c.SupplierName).HasMaxLength(25);


            this.Property(c => c.SupplierAddress).IsOptional();

            this.Property(c => c.SuplierEmail).IsOptional();

            this.Property(c => c.SupplierPhone).IsOptional();

        }
    }
}