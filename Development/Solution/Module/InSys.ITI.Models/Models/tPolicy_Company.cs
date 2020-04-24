using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tPolicy_Company")]
    public class tPolicy_Company
    {
        public int ID { get; set; }
        public int ID_Policy { get; set; }
        public int ID_Company { get; set; }
        public string Value { get; set; }
    }
    public class vPolicy_Company
    {
        public int ID { get; set; }
        public int ID_Policy { get; set; }
        public int ID_Company { get; set; }
        public string Value { get; set; }
        //view
        public string Policy { get; set; }
        public string Company { get; set; }
    }
}
