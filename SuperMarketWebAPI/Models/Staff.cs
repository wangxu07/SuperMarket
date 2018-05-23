using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMarketWebAPI.Models
{
    public class Staff
    {
        public int StaffID { get; set; }
        public string StaffName { get; set; }
        public string StaffAge { get; set; }
        public int StaffSex { get; set; }
        public string StaffAddress { get; set; }
        public string StaffEmail { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public DateTime? StaffBirthday { get; set; }
        public int Salary { get; set; }
        public int Level { get; set; }
        public string StaffUserName { get; set; }
        public string StaffPassWord { get; set; }
    }
}