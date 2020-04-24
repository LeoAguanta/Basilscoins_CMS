using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tPolicy_Section")]
    public class tPolicy_Section
    {
        public int ID { get; set; }
        public int ID_Policy { get; set; }
        public int ID_Section { get; set; }
        public string Value { get; set; }
    }
    public class vPolicy_Section
    {
        public int ID { get; set; }
        public int ID_Policy { get; set; }
        public int ID_Section { get; set; }
        public string Value { get; set; }
        //view
        public string Policy { get; set; }
        public string Section { get; set; }
    }
}
