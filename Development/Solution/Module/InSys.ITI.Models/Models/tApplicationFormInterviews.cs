using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tApplicationFormInterviews")]
    public class tApplicationFormInterviews
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
        public int ID_InterviewerStatus { get; set; }
        public bool IsDone { get; set; } = false;
    }
    [NotMapped]
    public class vApplicationFormInterviews
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
        public bool IsDone { get; set; } = false;
        public string AssignedEmployee { get; set; }
        //public string Designation { get; set; }
        public int? ID_Receiver { get; set; }
        public int ID_InterviewerStatus { get; set; }
    }
}
