using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tKioskApplicationFormInterviews")]
    public class tKioskApplicationFormInterviews
    {
        public int ID { get; set; }
        public int? ID_ExamAndInterviewStatus { get; set; }
        public int ID_AssignedEmployee { get; set; }
        public int? ID_ApplicationForm { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public DateTime? ScheduledTime { get; set; }
        public string Notes { get; set; }
        public string Attachment { get; set; }
        public bool IsEndorsedToNextProcedure { get; set; }
        public bool IsNotify { get; set; }
    }
    [NotMapped]
    public class vKioskApplicationFormInterviews
    {
        public int ID { get; set; }
        public int? ID_ExamAndInterviewStatus { get; set; }
        public int ID_AssignedEmployee { get; set; }
        public int? ID_ApplicationForm { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public DateTime? ScheduledTime { get; set; }
        public string Notes { get; set; }
        public string Attachment { get; set; }
        public bool IsNotify { get; set; }
        public string AssignedEmployee { get; set; }
        public bool IsEndorsedToNextProcedure { get; set; }
        public string Designation { get; set; }
    }
}
