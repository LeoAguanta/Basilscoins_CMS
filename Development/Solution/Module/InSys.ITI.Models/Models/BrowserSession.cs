using System;
using System.Collections.Generic;
using System.Text;

namespace InSys.ITI.Models.Models
{
    public class BrowserSession
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Designation { get; set; }
        public int? ID_Company { get; set; }
        public string Company { get; set; }
        public int ID_Roles { get; set; }
        public string Roles { get; set; }
        public int ID_User { get; set; }
        public int? ID_Employee { get; set; }
        public string WebNotificationServer { get; set; }
        public string SessionID { get; set; }
    }

    public class Profile
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string LoginName { get; set; }
        public string Image { get; set; }
        public string Designation { get; set; }
        public string CompanyEmail { get; set; }
        public string EmployeeStatus { get; set; }
        public string AccessNo { get; set; }
        public DateTime? DateHired { get; set; }
        public string SSSNo { get; set; }
        public string HDMFNo { get; set; }
        public string TINNo { get; set; }
        public string PhilHealthNo { get; set; }
        public int ID_User { get; set; }

    }
}
