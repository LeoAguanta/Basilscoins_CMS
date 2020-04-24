using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tManPowerPlans")]
    public class tManPowerPlans
    {
        public int ID { get; set; }
        public string RefNum { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Remarks { get; set; }
        public string FileName { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public int? ID_CreatedBy { get; set; }
        public int? ID_ModifiedBy { get; set; }
        public int? ID_Company { get; set; }
    }
    [NotMapped]
    public class vManPowerPlans
    {
        public int ID { get; set; }
        public string RefNum { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
    }
    [NotMapped]
    public class vManPowerPlanDetailsValidateResult
    {
        public string RefNum { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Position { get; set; }
    }

}
