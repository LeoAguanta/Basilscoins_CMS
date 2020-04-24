using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tMyModule")]
    public partial class tMyModule
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ID_Gender { get; set; }
    }
    public class vMyModule
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ID_Gender { get; set; }
        public string Gender { get; set; }
    }
}
