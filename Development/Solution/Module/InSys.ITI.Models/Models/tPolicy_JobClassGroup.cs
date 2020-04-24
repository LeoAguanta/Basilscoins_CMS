using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tPolicy_JobClassGroup")]
    public class tPolicy_JobClassGroup
    {
        public int ID { get; set; }
        public int ID_Policy { get; set; }
        public int ID_JobClassGroup { get; set; }
        public string Value { get; set; }
    }
    public class vPolicy_JobClassGroup
    {
        public int ID { get; set; }
        public int ID_Policy { get; set; }
        public int ID_JobClassGroup { get; set; }
        public string Value { get; set; }
        //view
        public string Policy { get; set; }
        public string JobClassGroup { get; set; }
    }
}
