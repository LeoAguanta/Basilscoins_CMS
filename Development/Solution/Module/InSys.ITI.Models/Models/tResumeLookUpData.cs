using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [NotMapped]
    public class tResumeLookUpData
    {
        public int ID { get; set; }
        //public string Code { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}
