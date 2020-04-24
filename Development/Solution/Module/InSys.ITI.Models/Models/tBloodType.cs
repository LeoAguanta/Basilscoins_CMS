using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tBloodType")]
    public class tBloodType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
    [NotMapped]
    public class vBloodType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}
