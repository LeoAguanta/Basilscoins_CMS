using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tPolicy_JobClass")]
    public class tPolicy_JobClass
    {
        public int ID { get; set; }
        public int ID_Policy { get; set; }
        public int ID_JobClass { get; set; }
        public string Value { get; set; }
    }
    public class vPolicy_JobClass
    {
        public int ID { get; set; }
        public int ID_Policy { get; set; }
        public int ID_JobClass { get; set; }
        public string Value { get; set; }
        //view
        public string Policy { get; set; }
        public string JobClass { get; set; }
    }
}
