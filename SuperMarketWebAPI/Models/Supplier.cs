using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMarketWebAPI.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SuplierEmail { get; set; }
        public string SupplierPhone { get; set; }
        public virtual ICollection<SulProduct> sproducts { get; set; }
        public virtual ICollection<Purchase> purchases { get; set; }

        
    }
}