using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tFilingModules")]
    public class tFilingModules
    {
        public int ID { get; set; }
        public int ID_Menus { get; set; }
        public bool? IsActive { get; set; }
    }
    [NotMapped]
    public class vFilingModules
    {
        public int ID { get; set; }
        public int ID_Menus { get; set; }
        public bool? IsActive { get; set; }
        public string Name { get; set; }
    }
}
