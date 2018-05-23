using SuperMarketWebAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace SuperMarketWebAPI.Mappers
{
    public class StaffMapper:EntityTypeConfiguration<Staff>
    {
        public StaffMapper()
        {
            this.ToTable("Staff");

            this.HasKey(c => c.StaffID);   //主键
            this.Property(c => c.StaffID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);  //主键自增长
            this.Property(c => c.StaffID).IsRequired();

            this.Property(c => c.StaffName).IsRequired();     //字段非空
            this.Property(c => c.StaffName).HasMaxLength(15);  //设定字段最大长度

            this.Property(c => c.StaffAge).IsOptional();
            this.Property(c => c.StaffAge).HasMaxLength(10);

            this.Property(c => c.StaffBirthday).IsOptional();
            this.Property(c => c.StaffEmail).IsOptional();     //字段可以为空
            this.Property(c => c.StaffAddress).IsOptional();

            this.Property(c => c.CheckInTime).IsRequired();
            this.Property(c => c.CheckOutTime).IsOptional();
            this.Property(c => c.Salary).IsOptional();

            this.Property(c => c.Level).IsRequired();

            this.Property(c => c.StaffUserName).IsRequired();
            this.Property(c => c.StaffPassWord).IsRequired();
            
        }
    }
}