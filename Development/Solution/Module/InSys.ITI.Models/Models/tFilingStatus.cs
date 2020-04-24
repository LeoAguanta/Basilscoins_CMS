using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tFilingStatus")]
    public class tFilingStatus
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int IsActive { get; set; }
    }
}
