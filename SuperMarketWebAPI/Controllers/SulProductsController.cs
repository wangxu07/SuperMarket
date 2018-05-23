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
    public class SulProductsController : ApiController
    {
        private SuperMarketWebAPIContext db = new SuperMarketWebAPIContext();

        // GET: api/SulProducts
        public IQueryable<SulProduct> GetSulProducts()
        {
            return db.SulProducts;
        }
        public IHttpActionResult GetSproduct()
        {
            //string value = "OK";
            db.Configuration.ProxyCreationEnabled = false;
            List<SulProduct> sulproduct = null;
            sulproduct = db.SulProducts.ToList();
            

            return Ok(sulproduct);
        }
        [HttpGet]
        public Object NewGetSproduct()
        {
            var sulproduct = (from a in db.SulProducts
                              join c in db.Suppliers on a.Supplierid equals c.SupplierID
                              select new
                              {
                                  SulProductID=a.SulProductID,
                                  SulProductName = a.SulProductName,
                                  Remarks = a.Remarks,
                                  SupplierName = c.SupplierName,
                                  Supplierid=a.Supplierid
                              });
            return sulproduct.ToList();
        }
        public IHttpActionResult GetSulProductsByName([FromUri]string name)
        {
            string value = "false";
           // List<SulProduct> sulproduct = null;
            Supplier supplier = null;
            supplier = db.Suppliers.Where(c => c.SupplierName == name).FirstOrDefault();
            if (supplier != null)
            {

                //sulproduct = db.SulProducts.Where(c => c.Supplierid == supplier.SupplierID).Select()
              var  sulproduct = (from a in db.SulProducts
                             join b in db.Suppliers on a.Supplierid equals b.SupplierID
                             where b.SupplierID==supplier.SupplierID
                             select new
                             {
                                 SulProductID=a.SulProductID,
                                 SulProductName=a.SulProductName,
                                 Supplierid=a.Supplierid,
                                 Remarks=a.Remarks

                             });
                if (sulproduct == null)
                {
                    return NotFound();
                }
                return Ok(sulproduct.ToList());
            }
            return Ok(value);
            
            
        }
        // GET: api/SulProducts/5
        [ResponseType(typeof(SulProduct))]
        public IHttpActionResult GetSulProduct(int id)
        {
            SulProduct sulProduct = db.SulProducts.Find(id);
            if (sulProduct == null)
            {
                return NotFound();
            }

            return Ok(sulProduct);
        }

        public string PutSulProductByPara([FromBody]Newtonsoft.Json.Linq.JObject data)
        {
            string value = "OK";
            int id = int.Parse(data["SulProductID"].ToString());

            SulProduct sulproduct = null;
            sulproduct = db.SulProducts.Where(c => c.SulProductID == id).FirstOrDefault();
            try
            {
                if (sulproduct.SulProductName != data["SulProductName"].ToString())
                {
                    sulproduct.SulProductName = data["SulProductName"].ToString();
                }
                if (sulproduct.Remarks != data["Remarks"].ToString())
                {
                    sulproduct.Remarks = data["Remarks"].ToString();
                }
                if (sulproduct.Supplierid != int.Parse(data["Supplierid"].ToString()))
                {
                    sulproduct.Supplierid = int.Parse(data["Supplierid"].ToString());
                }

                db.Entry(sulproduct).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                value = "error";
            }
            return value;
        }

        // PUT: api/SulProducts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSulProduct(int id, SulProduct sulProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sulProduct.SulProductID)
            {
                return BadRequest();
            }

            db.Entry(sulProduct).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SulProductExists(id))
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

        // POST: api/SulProducts
        [ResponseType(typeof(SulProduct))]
        public IHttpActionResult PostSulProduct(SulProduct sulProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SulProducts.Add(sulProduct);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sulProduct.SulProductID }, sulProduct);
        }
        public string PostAddSulProduct([FromBody]Newtonsoft.Json.Linq.JObject data)
        {
            string value = "OK";
            
            SulProduct sulproduct = new SulProduct();
           
           
            sulproduct.SulProductName = data["SulProductName"].ToString();
            sulproduct.Remarks = data["Remarks"].ToString();
            
            sulproduct.Supplierid = int.Parse(data["Supplierid"].ToString());
            Supplier supplier = db.Suppliers.Where(c => c.SupplierID == sulproduct.Supplierid).FirstOrDefault();
            sulproduct.Supplier = supplier;
                      
                    if (sulproduct != null)
                    {
                        try
                        {
                            db.SulProducts.Add(sulproduct);
                            db.SaveChanges();
                        }
                        catch
                        {
                            value = "false";
                        }
                    }                      
            return value;
        }

        // DELETE: api/SulProducts/5
        [ResponseType(typeof(SulProduct))]
        public IHttpActionResult DeleteSulProduct(int id)
        {
            SulProduct sulProduct = db.SulProducts.Find(id);
            if (sulProduct == null)
            {
                return NotFound();
            }

            db.SulProducts.Remove(sulProduct);
            db.SaveChanges();

            return Ok(sulProduct);
        }
        public string DeleteSulProductByID([FromUri]int SulProductID)
        {
            string value = string.Empty;
            SulProduct sulproduct = db.SulProducts.Find(SulProductID);
            if (sulproduct != null)
            {
                try
                {
                    db.SulProducts.Remove(sulproduct);
                    db.SaveChanges();
                    value = "OK";
                }
                catch
                {
                    value = "false";
                }
            }

            return value;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SulProductExists(int id)
        {
            return db.SulProducts.Count(e => e.SulProductID == id) > 0;
        }
    }
}