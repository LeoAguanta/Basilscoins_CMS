using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tPolicy")]
    public class tPolicy
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int ID_Company { get; set; }
    }
    public class vPolicy
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int ID_Company { get; set; }
        //view
        public string Company { get; set; }
    }
}
