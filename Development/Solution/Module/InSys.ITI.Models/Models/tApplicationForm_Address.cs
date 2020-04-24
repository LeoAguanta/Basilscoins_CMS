using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tApplicationForm_Address")]
    public class tApplicationForm_Address
    {
        public int ID { get; set; }
        public int ID_ApplicationForm { get; set; }
        public string HouseNo { get; set; }
        public string StreetName { get; set; }
        public int? ID_City { get; set; }
        public int? ID_Province { get; set; }
        public string PhoneNo { get; set; }
        public int? ID_Barangay { get; set; }
        public int? ID_UserCreatedBy { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DatetimeCreated { get; set; }
    }
    [NotMapped]
    public class vApplicationForm_Address
    {
        public int ID { get; set; }
        public int ID_ApplicationForm { get; set; }
        public string HouseNo { get; set; }
        public string StreetName { get; set; }
        public int? ID_City { get; set; }
        public int? ID_Province { get; set; }
        public string PhoneNo { get; set; }
        public int? ID_Barangay { get; set; }
        public int? ID_UserCreatedBy { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DatetimeCreated { get; set; }
        //View
        public string City { get; set; }
        public string Province { get; set; }
        public string Barangay { get; set; }
        public string ZipCode { get; set; }
        public string AddressProperty { get; set; }
        public bool IsPresentAddress { get; set; }
    }
}
