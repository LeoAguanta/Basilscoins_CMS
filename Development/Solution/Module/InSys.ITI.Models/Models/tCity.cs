using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tCity")]
    public class tCity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ID_Province { get; set; }
        public bool? IsActive { get; set; }
    }
    [NotMapped]
    public class vCity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Province { get; set; }
        public bool? IsActive { get; set; }
    }
}
