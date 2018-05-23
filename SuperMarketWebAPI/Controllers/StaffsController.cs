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
using Newtonsoft.Json;

namespace SuperMarketWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]   
    public class StaffsController : ApiController
    {
        private SuperMarketWebAPIContext db = new SuperMarketWebAPIContext();

        // GET: api/Staffs
        public IQueryable<Staff> GetStaffs()
        {
            return db.Staffs;
        }
        [HttpGet]
        public Object GetStaffsByPage([FromUri]int page,int PageSize=5)
        {

            List<Staff> sf= db.Staffs.OrderBy(c=>c.StaffID).ToList();
            var StaffCount = sf.Count();
            var TotalPages =(int)Math.Ceiling((double)StaffCount / PageSize);

            var result = sf.Skip(PageSize * (page - 1)).Take(PageSize).ToList();
            return new {
                userinfo=result,
                TotalPages=TotalPages,
                TotalCount=StaffCount
            };
        }
        public IHttpActionResult GetStaffsByName([FromUri]string name)
        {
            // data.ToString();
            //string name = data["name"].ToString();
            //name.ToString();
            List<Staff> staff = null;
               staff=db.Staffs.Where(u => u.StaffName == name).ToList();       
                if (staff == null)
                {
                    return NotFound();
                }
            return Ok(staff);
        }

        // GET: api/Staffs/5
        [ResponseType(typeof(Staff))]
        public IHttpActionResult GetStaff(int id)
        {
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return NotFound();
            }

            return Ok(staff);
        }
        [HttpGet]
        public IHttpActionResult GetStaffByID([FromUri] int StaffID)
        {
            Staff st = new Staff();
            st = db.Staffs.Where(c => c.StaffID == StaffID).FirstOrDefault();

            return Ok(st);
        }
        // PUT: api/Staffs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStaff(int id, Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != staff.StaffID)
            {
                return BadRequest();
            }

            db.Entry(staff).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(id))
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
        public string PutStaffBypara([FromBody]Newtonsoft.Json.Linq.JObject data)
        {
            string value = "OK";
            int id = int.Parse(data["StaffID"].ToString());
          //  string s = data["CheckOutTime"].ToString();
            Staff staffedit = null;
            staffedit = db.Set<Staff>().Where(u => u.StaffID == id).FirstOrDefault();
            try
            {
                if (staffedit.StaffName != data["StaffName"].ToString())
                {
                    staffedit.StaffName = data["StaffName"].ToString();
                }
                if (staffedit.StaffAge != data["StaffAge"].ToString())
                {
                    staffedit.StaffAge = data["StaffAge"].ToString();
                }
                if (staffedit.StaffSex != int.Parse(data["StaffSex"].ToString()))
                {
                    staffedit.StaffSex = int.Parse(data["StaffSex"].ToString());
                }
                if (staffedit.StaffAddress != data["StaffAddress"].ToString())
                {
                    staffedit.StaffAddress = data["StaffAddress"].ToString();
                }
                if (staffedit.StaffEmail != data["StaffEmail"].ToString())
                {
                    staffedit.StaffEmail = data["StaffEmail"].ToString();
                }
                //if (staffedit.CheckInTime != DateTime.Parse(data["CheckInTime"].ToString()))
                //{
                //    staffedit.CheckInTime = DateTime.Parse(data["CheckInTime"].ToString());
                //}
                if (data["CheckOutTime"].ToString() != "")
                {
                    if (staffedit.CheckOutTime != DateTime.Parse(data["CheckOutTime"].ToString()))
                    {
                        staffedit.CheckOutTime = DateTime.Parse(data["CheckOutTime"].ToString());
                    }
                }
                else
                {
                    staffedit.CheckOutTime = null;
                }
               
                if (staffedit.StaffBirthday != DateTime.Parse(data["StaffBirthday"].ToString()))
                {
                    staffedit.StaffBirthday = DateTime.Parse(data["StaffBirthday"].ToString());
                }
                if (staffedit.Salary != int.Parse(data["Salary"].ToString()))
                {
                    staffedit.Salary = int.Parse(data["Salary"].ToString());
                }
                if (staffedit.Level != int.Parse(data["Level"].ToString()))
                {
                    staffedit.Level = int.Parse(data["Level"].ToString());
                }
                if (staffedit.StaffUserName != data["StaffUserName"].ToString())
                {
                    staffedit.StaffUserName = data["StaffUserName"].ToString();
                }
                if (staffedit.StaffPassWord != data["StaffPassWord"].ToString())
                {
                    staffedit.StaffPassWord = data["StaffPassWord"].ToString();
                }

                db.Entry(staffedit).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                value="false";
            }
            return value;
        }
        public string PutPeopleBypara([FromBody]Newtonsoft.Json.Linq.JObject data)
        {
            string value = string.Empty;

            int id = int.Parse(data["StaffID"].ToString());
            Staff staffedit = null;
            staffedit = db.Set<Staff>().Where(u => u.StaffID == id).FirstOrDefault();
            try
            {
                if (staffedit.StaffAddress != data["StaffAddress"].ToString())
                {
                    staffedit.StaffAddress = data["StaffAddress"].ToString();
                }
                if (staffedit.StaffEmail != data["StaffEmail"].ToString())
                {
                    staffedit.StaffEmail = data["StaffEmail"].ToString();
                }
                if (staffedit.StaffPassWord != data["StaffPassWord"].ToString())
                {
                    staffedit.StaffPassWord = data["StaffPassWord"].ToString();
                }

                db.Entry(staffedit).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                value = "OK";
            }
            catch
            {
                value = "false";
            }

            return value;
        }
        // POST: api/Staffs
        [ResponseType(typeof(Staff))]
        public IHttpActionResult PostStaff(Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Staffs.Add(staff);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = staff.StaffID }, staff);
        }
        [ResponseType(typeof(void))]
        public string PostLogin([FromBody]Newtonsoft.Json.Linq.JObject data)
        {
            string IsRight = string.Empty;
            var result = (from a in db.Staffs
                          select new
                          {
                              StaffUserName = a.StaffUserName,
                              StaffPassWord = a.StaffPassWord,
                              StaffName = a.StaffName,
                              Level = a.Level,
                              StaffID = a.StaffID
                          });
            //result.ToList();
            //data.ToString();
            string un = data["username"].ToString();
            string pwd = data["pwd"].ToString();
            foreach (var temp in result.ToList())
            {
                if (temp.StaffUserName == un && temp.StaffPassWord == pwd)
                {
                    IsRight = "OK"+","+temp.StaffName+","+temp.Level+ "," + temp.StaffID;
                    break;
                    
                }
                else
                {
                    IsRight = "False";
                }
            }
            return IsRight;
           
        }
        public string PostAddStaff([FromBody]Newtonsoft.Json.Linq.JObject data)
        {
            string IsSuccessful = "OK";

            Staff staff = new Staff();
            staff.StaffName = data["StaffName"].ToString();
            staff.StaffAge = data["StaffAge"].ToString();
            staff.StaffSex = int.Parse(data["StaffSex"].ToString());
            staff.StaffAddress = data["StaffAddress"].ToString();
            staff.StaffEmail = data["StaffEmail"].ToString();
            staff.CheckInTime = DateTime.Parse(data["CheckInTime"].ToString());
            //staff.CheckOutTime = DateTime.Parse(data["CheckOutTime"].ToString());
            
            staff.StaffBirthday = DateTime.Parse(data["StaffBirthday"].ToString());
            staff.Salary = int.Parse(data["Salary"].ToString());
            staff.Level = int.Parse(data["Level"].ToString());
            staff.StaffUserName = data["StaffUserName"].ToString();
            staff.StaffPassWord = data["StaffPassWord"].ToString();
            if (staff != null)
            {
                try
                {
                    db.Staffs.Add(staff);
                    db.SaveChanges();
                }
                catch
                {
                    IsSuccessful = "error";
                }
            }

            return IsSuccessful;
        }
        public string DeleteStaffByID([FromUri]int StaffID)
        {
            string value = string.Empty;

            Staff staff = db.Staffs.Find(StaffID);
            if (staff != null)
            {
                try
                {
                    db.Staffs.Remove(staff);
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
                value = "other";
            }
            return value;
        }
        // DELETE: api/Staffs/5
        [ResponseType(typeof(Staff))]
        public IHttpActionResult DeleteStaff(int id)
        {
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return NotFound();
            }

            db.Staffs.Remove(staff);
            db.SaveChanges();

            return Ok(staff);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StaffExists(int id)
        {
            return db.Staffs.Count(e => e.StaffID == id) > 0;
        }
    }
}