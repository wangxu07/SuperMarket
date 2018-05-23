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
    public class SuppliersController : ApiController
    {
        private SuperMarketWebAPIContext db = new SuperMarketWebAPIContext();

        // GET: api/Suppliers
        public IQueryable<Supplier> GetSuppliers()
        {
            return db.Suppliers;
        }
        public IHttpActionResult Getsupplier()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Supplier> supplier = null;
            supplier = db.Suppliers.ToList();
            return Ok(supplier);

        }
        // GET: api/Suppliers/5
        [ResponseType(typeof(Supplier))]
        public IHttpActionResult GetSupplier(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }
        public IHttpActionResult GetSupplierByName([FromUri]string name)
        {

            //Supplier supplier = new Supplier();
            //supplier = db.Suppliers.Where(c =>c.SupplierName==name).FirstOrDefault();
            var supplier = (from a in db.Suppliers
                            where a.SupplierName==name
                            select new
                            {
                                SupplierID=a.SupplierID,
                                SupplierName=a.SupplierName,
                                SupplierAddress = a.SupplierAddress,
                                SuplierEmail = a.SuplierEmail,
                                SupplierPhone = a.SupplierPhone
                            });

            return Ok(supplier);
        }
        // PUT: api/Suppliers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSupplier(int id, Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != supplier.SupplierID)
            {
                return BadRequest();
            }

            db.Entry(supplier).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(id))
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
        public string PutSupplierByPara([FromBody]Newtonsoft.Json.Linq.JObject data)
        {
            string value = string.Empty;
            int id = int.Parse(data["SupplierID"].ToString());
            Supplier supplier = new Supplier();
            supplier = db.Suppliers.Where(c => c.SupplierID == id).FirstOrDefault();

            try
            {
                if (supplier.SupplierName != data["SupplierName"].ToString())
                {
                    supplier.SupplierName = data["SupplierName"].ToString();
                }
                if (supplier.SupplierAddress != data["SupplierAddress"].ToString())
                {
                    supplier.SupplierAddress = data["SupplierAddress"].ToString();
                }
                if (supplier.SuplierEmail != data["SuplierEmail"].ToString())
                {
                    supplier.SuplierEmail = data["SuplierEmail"].ToString();
                }
                if (supplier.SupplierPhone != data["SupplierPhone"].ToString())
                {
                    supplier.SupplierPhone = data["SupplierPhone"].ToString();
                }
                db.Entry(supplier).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                value = "OK";
            }
            catch
            {
                value = "false";
            }
            return value;
        }
        // POST: api/Suppliers
        [ResponseType(typeof(Supplier))]
        public IHttpActionResult PostSupplier(Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Suppliers.Add(supplier);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = supplier.SupplierID }, supplier);
        }
        public string PostAddSupplier([FromBody]Newtonsoft.Json.Linq.JObject data)
        {
            string value = "OK";
            Supplier supplier = new Supplier();

            supplier.SupplierName = data["SupplierName"].ToString();
            supplier.SupplierAddress = data["SupplierAddress"].ToString();
            supplier.SuplierEmail = data["SuplierEmail"].ToString();
            supplier.SupplierPhone = data["SupplierPhone"].ToString();
            if (supplier != null)
            {
                try
                {
                    db.Suppliers.Add(supplier);
                    db.SaveChanges();
                }
                catch
                {
                    value = "false";
                }
            }

            return value;
        }
        // DELETE: api/Suppliers/5
        [ResponseType(typeof(Supplier))]
        public IHttpActionResult DeleteSupplier(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }

            db.Suppliers.Remove(supplier);
            db.SaveChanges();

            return Ok(supplier);
        }
        public string DeleteSupplierByID([FromUri]int SupplierID)
        {
            string value = string.Empty;
            Supplier supplier = db.Suppliers.Find(SupplierID);
            if (supplier != null)
            {
                try
                {
                    db.Suppliers.Remove(supplier);
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

        private bool SupplierExists(int id)
        {
            return db.Suppliers.Count(e => e.SupplierID == id) > 0;
        }
    }
}