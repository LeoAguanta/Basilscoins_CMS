using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tJobClass")]
    public class tJobClass
    {
        public int ID { get; set; }
        public bool? IsActive { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
    }
    [NotMapped]
    public class vJobClass
    {
        public int ID { get; set; }
        public bool? IsActive { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
    }
}
