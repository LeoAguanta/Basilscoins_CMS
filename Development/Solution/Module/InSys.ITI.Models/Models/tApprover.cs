using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tApprover")]
    public class tApprover
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ID_Company { get; set; }
        public bool? IsActive { get; set; }
    }
    [NotMapped]
    public class vApprover
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ID_Company { get; set; }
        public bool? IsActive { get; set; }
        public string Company { get; set; }
    }
}
