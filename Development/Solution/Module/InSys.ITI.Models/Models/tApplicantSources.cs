using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tApplicantSources")]
    public class tApplicantSources
    {
        public int ID { get; set; }
        public int? ID_Company { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsSystemDefined { get; set; }
        public bool? IsActive { get; set; }
    }
}
