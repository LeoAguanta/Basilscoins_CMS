using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tManPowerPlanDetails")]
    public class tManPowerPlanDetails
    {
        public int ID { get; set; }
        public int ID_ManPowerPlans { get; set; }
        public int ID_Designation { get; set; }
        public int Plantilla { get; set; }
        public int HeadCount { get; set; }
        public int ActiveVacancies { get; set; }
        public string Remarks { get; set; }
        public string AdditionalInfo { get; set; }
        public string FileName { get; set; }
    }
    [NotMapped]
    public class vManPowerPlanDetails
    {
        public int ID { get; set; }
        public int ID_ManPowerPlans { get; set; }
        public int ID_Designation { get; set; }
        public string Position { get; set; }
        public int Plantilla { get; set; }
        public int HeadCount { get; set; }
        public int Vacancy { get; set; }
        public int OnHold { get; set; }
        public int Replacement { get; set; }
        public int ActiveVacancies { get; set; }
        public string Remarks { get; set; }
        public string AdditionalInfo { get; set; }
        public string FileName { get; set; }
    }
    [NotMapped]
    public class tPostManPowerPlanDetails {
        public int ID { get; set; }
        public int ID_ManPowerPlans { get; set; }
        public int ID_Designation { get; set; }
        public string Position { get; set; }
        public int Plantilla { get; set; }
        public int HeadCount { get; set; }
        public int Vacancy { get; set; }
    }
}
