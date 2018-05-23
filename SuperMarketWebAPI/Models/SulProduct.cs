using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMarketWebAPI.Models
{
    public class SulProduct
    {
        public int SulProductID { get; set; }
        public string SulProductName { get; set; }
        public string Remarks { get; set; }
        public int Supplierid { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}