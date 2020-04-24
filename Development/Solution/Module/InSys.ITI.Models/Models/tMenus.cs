using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tMenus")]
    public class tMenus
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? ID_Menus { get; set; }
        public int? SequenceNumber { get; set; }
        public int ID_MenusType { get; set; } = 1;
        public bool IsVisible { get; set; } = true;
        public bool IsSystem { get; set; } = false;
        public bool IsFilingModule { get; set; } = false;
        public string AssemblyName { get; set; }
        public string ClassName { get; set; }
        public int ID_Company { get; set; }
    }
}
