using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMarketWebAPI.Models
{
    public class Stock
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public int? OutCount { get; set; }
        public int? OutPrice { get; set; }
        public DateTime? OutDate{ get; set; }
        public DateTime? Date { get; set; }
        public int IsSelling { get; set; }
        public int Supplierid { get; set; }
    }
}