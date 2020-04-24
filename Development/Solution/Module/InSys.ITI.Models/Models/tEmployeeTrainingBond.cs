using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tEmployeeTrainingBond")]
    public class tEmployeeTrainingBond
    {
        public int ID { get; set; }
        public int ID_Employee { get; set; }
        public int? ID_Training { get; set; }
        public string Notes { get; set; }
        public int? NumberOfMonths { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }
        public bool IsCancelled { get; set; }
    }

}
