using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tMovementType")]
    public class tMovementType
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
