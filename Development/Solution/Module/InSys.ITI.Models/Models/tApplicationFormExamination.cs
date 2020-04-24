using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tApplicationFormExamination")]
    public class tApplicationFormExamination
    {
        public int ID { get; set; }
        public int ID_Exams { get; set; }
        public int? ID_ExamAndInterviewStatus { get; set; }
        public int ID_AssignedEmployee { get; set; }
        public int ID_ApplicationForm { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime? ExpectedStartTime { get; set; }
        public DateTime? ExpectedEndTime { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public DateTime? ActualEndTime { get; set; }
        public string Notes { get; set; }
        public int? TotalScore { get; set; }
        public bool IsNotify { get; set; }
        public string Attachment { get; set; }
        public decimal PercentageBased { get; set; } = 0;
        public bool IsDone { get; set; } = false;
    }
    [NotMapped]
    public class vApplicationFormExamination
    {
        public int ID { get; set; }
        public int ID_Exams { get; set; }
        public int? ID_ExamAndInterviewStatus { get; set; }
        public int ID_AssignedEmployee { get; set; }
        public int ID_ApplicationForm { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime? ExpectedStartTime { get; set; }
        public DateTime? ExpectedEndTime { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public DateTime? ActualEndTime { get; set; }
        public string Notes { get; set; }
        public int? TotalScore { get; set; }
        public bool IsNotify { get; set; }
        public string Attachment { get; set; }
        public decimal PercentageBased { get; set; } = 0;
        public bool IsDone { get; set; } = false;
        public string AssignedEmployee { get; set; }
        public int? ID_Receiver { get; set; }
    }


}
