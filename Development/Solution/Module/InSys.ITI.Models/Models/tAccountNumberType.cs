using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tAccountNumberType")]
    public class tAccountNumberType
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public DateTime DateTimeCreated { get; set; }
    }
}
