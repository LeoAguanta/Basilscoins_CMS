using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tSignatories")]
    public class tSignatories
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public int ID_Company { get; set; }
    }
}
