using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tProvince")]
    public class tProvince
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}
