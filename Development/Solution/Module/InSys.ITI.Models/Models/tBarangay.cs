using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tBarangay")]
    public class tBarangay
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ID_City { get; set; }
        public bool? IsActive { get; set; }
    }
}
