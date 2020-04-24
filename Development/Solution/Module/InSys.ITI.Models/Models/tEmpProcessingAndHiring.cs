using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tEmpProcessingAndHiring")]
    public class tEmpProcessingAndHiring
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    [NotMapped]
    public class vEmpProcessingAndHiring
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
