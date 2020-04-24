using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [NotMapped]
    public class tEndorseLookUpData
    {
        public int ID { get; set; }
        //public string Code { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public int ID_Designation { get; set; }
    }

    [NotMapped]
    public class tKioskMRFLData
    {
        public int ID { get; set; }
        //public string Code { get; set; }
        public int ID_Designation { get; set; }
        public bool? IsActive { get; set; }
        public int? ID_Mrf { get; set; }
        public string MRFNumber { get; set; }
    }
}