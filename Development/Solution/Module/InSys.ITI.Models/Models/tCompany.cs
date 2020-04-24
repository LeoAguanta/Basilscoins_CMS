using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tCompany")]
    public class tCompany
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string TIN { get; set; }
        public string SSSNo { get; set; }
        public string PhilHealthNo { get; set; }
        public string HDMFNo { get; set; }
        public string TelNo { get; set; }
        public string President { get; set; }
        public string VicePresident { get; set; }
        public string Owner { get; set; }
        public string VatRegNo { get; set; }
        public string ImageFile { get; set; }
        public bool? IsActive { get; set; }
    }
    [NotMapped]
    public class vCompany
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string TIN { get; set; }
        public string SSSNo { get; set; }
        public string PhilHealthNo { get; set; }
        public string HDMFNo { get; set; }
        public string TelNo { get; set; }
        public string President { get; set; }
        public string VicePresident { get; set; }
        public string Owner { get; set; }
        public string VatRegNo { get; set; }
        public string ImageFile { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public bool? IsActive { get; set; }
    }
}
