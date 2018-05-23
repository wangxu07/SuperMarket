using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SuperMarketWebAPI;
using SuperMarketWebAPI.Models;
using System.Web.Http.Cors;

namespace SuperMarketWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StocksController : ApiController
    {
        private SuperMarketWebAPIContext db = new SuperMarketWebAPIContext();

        // GET: api/Stocks
        public IQueryable<Stock> GetStocks()
        {
            return db.Stocks;
        }
        [HttpGet]
        public object NewGetStock()
        {
            List<Stock> st = new List<Stock>();
            st = db.Stocks.ToList();
            return st;
        }
       
        // GET: api/Stocks/5
        [ResponseType(typeof(Stock))]
        public IHttpActionResult GetStock(int id)
        {
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }
        public object GetByName([FromUri] string name)
        {
            List<Stock> st = db.Stocks.Where(c => c.ProductName == name).ToList();

            return st;
        }

        // PUT: api/Stocks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStock(int id, Stock stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stock.ProductID)
            {
                return BadRequest();
            }

            db.Entry(stock).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        public string PutOnSell([FromBody]Newtonsoft.Json.Linq.JObject data)
        {
            string value = string.Empty;
            string p_name = data["ProductName"].ToString();
            int id = int.Parse(data["Supplierid"].ToString());
            Stock stock = new Stock();
            stock = db.Stocks.Where(c => c.ProductName == p_name && c.Supplierid == id).FirstOrDefault();
            try
            {
                stock.Count = stock.Count + Convert.ToInt32(stock.OutCount);
                stock.OutCount = 0;
                stock.IsSelling = int.Parse(data["IsSelling"].ToString());
                stock.OutPrice = 0;
                stock.OutDate = null;
                db.Entry(stock).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                value = "OK";
            }
            catch
            {
                value = "false";
            }
           
            return value;
        }
        public string PutStocksell([FromBody]Newtonsoft.Json.Linq.JObject data)
        {
            string value = string.Empty;

            string p_name = data["ProductName"].ToString();
             int id = int.Parse(data["Supplierid"].ToString());
            Stock stock = new Stock();
            stock = db.Stocks.Where(c => c.ProductName == p_name && c.Supplierid == id).FirstOrDefault();

            try
            {
                stock.Count = stock.Count - int.Parse(data["OutCount"].ToString());
                stock.OutCount = Convert.ToInt32(stock.OutCount) + int.Parse(data["OutCount"].ToString());
                //if (Convert.ToInt32(stock.OutCount) != int.Parse(data["OutCount"].ToString()))
                //{
                //    if (Convert.ToInt32(stock.OutCount) > int.Parse(data["OutCount"].ToString()))
                //    {                       
                //        stock.Count = stock.Count +(Convert.ToInt32(stock.OutCount)- int.Parse(data["OutCount"].ToString()));
                //    }
                //    else 
                //    {


                //        stock.Count = stock.Count - (int.Parse(data["OutCount"].ToString()) - Convert.ToInt32(stock.OutCount));
                //    }
                //    stock.OutCount = int.Parse(data["OutCount"].ToString());
                //}
                //stock.OutCount = int.Parse(data["OutCount"].ToString());

                stock.OutDate = DateTime.Parse(data["OutDate"].ToString());
                stock.OutPrice = int.Parse(data["OutPrice"].ToString());
                stock.IsSelling = int.Parse(data["IsSelling"].ToString());

                db.Entry(stock).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                value = "OK";
            }
            catch
            {
                value = "false";
            }
          

            return value;
        }
        public string PutOrPostStock([FromBody]Newtonsoft.Json.Linq.JObject data)
        {
            string value = string.Empty;
            string  p_name = data["ProductName"].ToString();
            string s_name = data["SupplierName"].ToString();
            Supplier supplier = new Supplier();
            supplier = db.Suppliers.Where(c =>c.SupplierName==s_name).FirstOrDefault();
            int id = supplier.SupplierID;
            Stock stock = new Stock();
            stock = db.Stocks.Where(c => c.ProductName == p_name && c.Supplierid == id).FirstOrDefault();
            Stock stock1 = new Stock();

            if (stock != null)
            {
                try
                {
                    int s_count = int.Parse(data["Count"].ToString());
                    stock.Count = stock.Count + s_count;
                    stock.Date = DateTime.Parse(data["Date"].ToString());

                    db.Entry(stock).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    value = "OK";
                }
                catch
                {
                    value = "false";
                }

            }
            else
            {
                try
                {
                    stock1.ProductName = data["ProductName"].ToString();
                    stock1.Count = int.Parse(data["Count"].ToString());
                    stock1.IsSelling = 0;
                    stock1.Date =DateTime.Parse(data["Date"].ToString());
                    stock1.Supplierid = id;

                    db.Stocks.Add(stock1);
                    db.SaveChanges();
                    value = "OK";
                }
                catch
                {
                    value = "flase";
                }
            }
            return value;
        }
        // POST: api/Stocks
        [ResponseType(typeof(Stock))]
        public IHttpActionResult PostStock(Stock stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stocks.Add(stock);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = stock.ProductID }, stock);
        }

        // DELETE: api/Stocks/5
        [ResponseType(typeof(Stock))]
        public IHttpActionResult DeleteStock(int id)
        {
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }

            db.Stocks.Remove(stock);
            db.SaveChanges();

            return Ok(stock);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StockExists(int id)
        {
            return db.Stocks.Count(e => e.ProductID == id) > 0;
        }
    }
}