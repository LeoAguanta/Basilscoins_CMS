using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tPolicy_Department")]
    public class tPolicy_Department
    {
        public int ID { get; set; }
        public int ID_Policy { get; set; }
        public int ID_Department { get; set; }
        public string Value { get; set; }
    }
    public class vPolicy_Department
    {
        public int ID { get; set; }
        public int ID_Policy { get; set; }
        public int ID_Department { get; set; }
        public string Value { get; set; }
        //view
        public string Policy { get; set; }
        public string Department { get; set; }
    }
}
