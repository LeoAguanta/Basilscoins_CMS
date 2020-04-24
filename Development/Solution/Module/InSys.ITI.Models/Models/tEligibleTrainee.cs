using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tEligibleTrainee")]
    public class tEligibleTrainee
    {
        public int ID { get; set; }
        public int ID_Training { get; set; }
        public int ID_Type { get; set; }
        public int ID_Value { get; set; }
    }

    [NotMapped]
    public class vEligibleTrainee
    {
        public int ID { get; set; }
        public int ID_Training { get; set; }
        public int ID_Type { get; set; }
        public int ID_Value { get; set; }
        public string Name {get; set;}
    }
}
