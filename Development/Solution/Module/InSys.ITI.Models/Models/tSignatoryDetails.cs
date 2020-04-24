using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tSignatoryDetails")]
    public class tSignatoryDetails
    {
        public int ID { get; set; }
        public int ID_Signatories { get; set; }
        public int? ID_Employee { get; set; }
        public string Type { get; set; }
    }

    [NotMapped]
    public class vSignatoryDetails
    {
        public int ID { get; set; }
        public int ID_Signatories { get; set; }
        public int? ID_Employee { get; set; }
        public string Employee { get; set; }
        public string Designation { get; set; }
        public string Type { get; set; }
    }
}
