using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tPolicy_Designation")]
    public class tPolicy_Designation
    {
        public int ID { get; set; }
        public int ID_Policy { get; set; }
        public int ID_Designation { get; set; }
        public string Value { get; set; }
    }
    public class vPolicy_Designation
    {
        public int ID { get; set; }
        public int ID_Policy { get; set; }
        public int ID_Designation { get; set; }
        public string Value { get; set; }
        //view
        public string Policy { get; set; }
        public string Designation { get; set; }
    }
}
