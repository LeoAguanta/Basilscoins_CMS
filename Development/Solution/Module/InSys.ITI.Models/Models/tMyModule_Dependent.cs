using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tMyModule_Dependent")]
    public partial class tMyModule_Dependent
    {
        public int ID { get; set; }
        public int ID_MyModule { get; set; }
        public string DependentName { get; set; }
    }
}
