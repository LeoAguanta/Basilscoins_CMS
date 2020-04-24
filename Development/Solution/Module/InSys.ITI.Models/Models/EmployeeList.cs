using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [NotMapped]
    public class EmployeeList
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? ID_Designation { get; set; }
        public string Designation { get; set; }
        public int ID_Company { get; set; }
    }
}
