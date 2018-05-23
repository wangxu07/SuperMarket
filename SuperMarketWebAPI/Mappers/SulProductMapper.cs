using SuperMarketWebAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace SuperMarketWebAPI.Mappers
{
    public class SulProductMapper: EntityTypeConfiguration<SulProduct>
    {
        public SulProductMapper()
        {
            this.ToTable("SProduct");

            this.HasKey(c => c.SulProductID);
            this.Property(c => c.SulProductID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            this.Property(c => c.SulProductID).IsRequired();

            this.Property(c => c.SulProductName).IsRequired();
            this.Property(c => c.SulProductName).HasMaxLength(25);

            this.Property(c => c.Supplierid).IsRequired();

            this.Property(c => c.Remarks).IsOptional();

            this.HasRequired(c => c.Supplier).WithMany(c => c.sproducts).HasForeignKey(c => c.Supplierid).WillCascadeOnDelete(true);

        }
    }
}