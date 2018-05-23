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
    public class PurchasesController : ApiController
    {
        private SuperMarketWebAPIContext db = new SuperMarketWebAPIContext();

        // GET: api/Purchases
        public IQueryable<Purchase> GetPurchases()
        {
            return db.Purchases;
        }
        [HttpGet]
        public IHttpActionResult NewGetPurchase()
        {
            var result = (from a in db.Purchases
                          select new
                          {
                              InProductName = a.InProductName,
                              InCount = a.InCount,
                              InPrice = a.InPrice,
                              InDate = a.InDate,
                              Suppliersid = a.Suppliersid
                          });
            return Ok(result.ToList());
          
        }
        public IHttpActionResult GetPurchasesByName([FromUri] string InProductName)
        {
            List<Purchase> purchase = null;
            purchase = db.Purchases.Where(u => u.InProductName == InProductName).ToList();
            if (purchase == null)
            {
                return NotFound();
            }
            return Ok(purchase);
        }
        public IHttpActionResult GetPurchasesByDate([FromUri] string InDate)
        {
            //string In_Date=InDate.Substring(0, 10);
            
            List<Purchase> purchase = new List<Purchase>();
            List<Purchase> purchase1 = null;
            
            // purchase = db.Purchases.Where(u => u.InDate == InDate).ToList();
            purchase1 = db.Purchases.ToList();
            foreach (Purchase temp in purchase1)
            {
                //string str4=temp.InDate.ToShortDateString();
                //string str2=temp.InDate.ToLongDateString();
                //string str1=  temp.InDate.ToString("yyyy-MM-dd HH:mm:ss");
                if (InDate.Length == 10)
                {
                    string str = temp.InDate.ToString("yyyy-MM-dd");
                    if (str == InDate)
                    {
                        purchase.Add(temp);
                    }
                }
                else if (InDate.Length == 7)
                {
                    string str = temp.InDate.ToString("yyyy-MM");
                    if (str == InDate)
                    {
                        purchase.Add(temp);
                    }
                }
                else if (InDate.Length == 4)
                {
                    string str = temp.InDate.ToString("yyyy");
                    if (str == InDate)
                    {
                        purchase.Add(temp);
                    }
                }
                else
                {
                    return NotFound();
                }
               

            }
            if (purchase == null)
            {
                return NotFound();
            }
            return Ok(purchase);
        }
        // GET: api/Purchases/5
        [ResponseType(typeof(Purchase))]
        public IHttpActionResult GetPurchase(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return NotFound();
            }

            return Ok(purchase);
        }

        // PUT: api/Purchases/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchase(int id, Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchase.InProductID)
            {
                return BadRequest();
            }

            db.Entry(purchase).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseExists(id))
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

        // POST: api/Purchases
        [ResponseType(typeof(Purchase))]
        public IHttpActionResult PostPurchase(Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Purchases.Add(purchase);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchase.InProductID }, purchase);
        }
        public string PostAddPurchase([FromBody]Newtonsoft.Json.Linq.JObject data)
        {
            string value = "OK";
            //string str = data.ToString();
            Purchase plist = new Purchase();
            plist.InProductName = data["InProductName"].ToString();
            plist.InCount = int.Parse(data["InCount"].ToString());
            plist.InPrice = int.Parse(data["InPrice"].ToString());
            plist.InDate = DateTime.Parse(data["InDate"].ToString());
            string s_name = data["SupplierName"].ToString();
            Supplier supplier = db.Suppliers.Where(c => c.SupplierName == s_name).FirstOrDefault();
            plist.Suppliersid = supplier.SupplierID;
            try
            {
                db.Purchases.Add(plist);
                db.SaveChanges();
            }
            catch
            {
                value = "false";
            }
            return value;
        }

        // DELETE: api/Purchases/5
        [ResponseType(typeof(Purchase))]
        public IHttpActionResult DeletePurchase(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return NotFound();
            }

            db.Purchases.Remove(purchase);
            db.SaveChanges();

            return Ok(purchase);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseExists(int id)
        {
            return db.Purchases.Count(e => e.InProductID == id) > 0;
        }
    }
}