using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{

    [Table("tTestModule")]
    public partial class tTestModule
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ID_Gender { get; set; }

    }

    [Table("vTestModule")]
    public class vTestModule
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ID_Gender { get; set; }
        public string Gender { get; set; }


    }
}
