using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tPolicy_Employee")]
    public class tPolicy_Employee
    {
        public int ID { get; set; }
        public int ID_Policy { get; set; }
        public int ID_Employee { get; set; }
        public string Value { get; set; }
    }
    public class vPolicy_Employee
    {
        public int ID { get; set; }
        public int ID_Policy { get; set; }
        public int ID_Employee { get; set; }
        public string Value { get; set; }
        //view
        public string Policy { get; set; }
        public string Employee { get; set; }
    }
}
