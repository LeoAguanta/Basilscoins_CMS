using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tPolicy_Branch")]
    public class tPolicy_Branch
    {
        public int ID { get; set; }
        public int ID_Policy { get; set; }
        public int ID_Branch { get; set; }
        public string Value { get; set; }
    }
    public class vPolicy_Branch
    {
        public int ID { get; set; }
        public int ID_Policy { get; set; }
        public int ID_Branch { get; set; }
        public string Value { get; set; }
        //view
        public string Policy { get; set; }
        public string Branch { get; set; }
    }
}
