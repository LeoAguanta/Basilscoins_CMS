using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tPersonaLicensesAndCertificates")]
    public partial class tPersonaLicensesAndCertificates
    {
        public int ID { get; set; }
        public int ID_Persona { get; set; }
        public string Name { get; set; }
        public string LicenseNo { get; set; }
        public string Description { get; set; }
        public DateTime? ValidityDate { get; set; }
        public DateTime DatetimeCreated { get; set; }
        public int? ID_UserCreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
