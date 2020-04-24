using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tTestmodule_Dependent")]
    public partial class tTestModule_Dependent
    {
        public int ID { get; set; }
        public int ID_TestModule { get; set; }
        public string DependentName { get; set; }

    }
}
