using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tPayrollItemSetupOption")]
    public class tPayrollItemSetupOption
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? SeqNo { get; set; }
        public int? ID_Company { get; set; }
    }
}
