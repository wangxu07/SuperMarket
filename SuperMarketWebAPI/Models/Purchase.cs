using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMarketWebAPI.Models
{
    public class Purchase
    {
        public int InProductID { get; set; }
        public string InProductName { get; set; }
        public int InCount { get; set; }
        public int InPrice { get; set; }
        public DateTime InDate { get; set; }
        public int Suppliersid { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}