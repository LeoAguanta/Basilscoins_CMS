using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tKioskApplicationFormExamination")]
    public class tKioskApplicationFormExamination
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
        public string Attachment { get; set; }
        public bool IsNotify { get; set; }
    }
    [NotMapped]
    public class vKioskApplicationFormExamination
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
        public string Attachment { get; set; }
        public bool IsNotify { get; set; }
        public string AssignedEmployee { get; set; }
    }


}
