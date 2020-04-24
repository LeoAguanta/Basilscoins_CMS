using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tTraining")]
    public class tTraining
    {
        public int ID { get; set; }
        public string ReferenceNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Provider { get; set; }
        public int ID_Company { get; set; }
        public string Location { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? ApplicationExpirationDate { get; set; }
        public double? CourseRating { get; set; }
        public int? ExpectedAttendees { get; set; }
        public string Attachments { get; set; }
        public string PenaltyForWithdrawal { get; set; }
        public string PenaltyForNoShow { get; set; }
        public bool WithCertification { get; set; }
        public decimal Price { get; set; }
        public string PreRequisite { get; set; }
        public string Code { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool Posted { get; set; }
        public int ID_FilingStatus { get; set; }
        public int? BondInMonths { get; set; }
        public int? ID_PaymentTerms { get; set; }
        public bool HideTrainingBond { get; set; }
        public int ID_Employee { get; set; }
        public int ApproverLevel { get; set; }
        public decimal? TrainingBondAmount { get; set; }
        public string ConductedBy { get; set; }
    }

    [NotMapped]
    public class vTraining
    {
        public int ID { get; set; }
        public string ReferenceNumber { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Provider { get; set; }
        public string CoveredDate { get; set; }
        public string StartAt { get; set; }
        public string Status { get; set; }
        public bool Posted { get; set; }
        public int ID_FilingStatus { get; set; }
        public DateTime? ApplicationClosed { get; set; }
        public int? ID_Company { get; set; }
    }
    [NotMapped]
    public class vTraining2
    {
        public int ID { get; set; }
        public string ReferenceNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Provider { get; set; }
        public int? ID_Company { get; set; }
        public string Location { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? ApplicationExpirationDate { get; set; }
        public double? CourseRating { get; set; }
        public int? ExpectedAttendees { get; set; }
        public string Attachments { get; set; }
        public string PenaltyForWithdrawal { get; set; }
        public string PenaltyForNoShow { get; set; }
        public bool WithCertification { get; set; }
        public decimal Price { get; set; }
        public string PreRequisite { get; set; }
        public string Code { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool Posted { get; set; }
        public int ID_FilingStatus { get; set; }
        public int? BondInMonths { get; set; }
        public int? ID_PaymentTerms { get; set; }
        public bool HideTrainingBond { get; set; }
        //view
        public string Company { get; set; }
        public string FilingStatus { get; set; }
        public int? ID_Status { get; set; }
        public string ReasonForNotAttending { get; set; }
        public int? ID_EmployeeFilingStatus { get; set; }
        public int? ApproverLevel { get; set; }
        public string Employee { get; set; }
        public int? ID_Employee { get; set; }
        public string EmployeeFilingStatus { get; set; }
    }
    [NotMapped]
    public class vTraining3
    {
        public int ID { get; set; }
        public string ReferenceNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Provider { get; set; }
        public int? ID_Company { get; set; }
        public string Location { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? ApplicationExpirationDate { get; set; }
        public double? CourseRating { get; set; }
        public int? ExpectedAttendees { get; set; }
        public string Attachments { get; set; }
        public string PenaltyForWithdrawal { get; set; }
        public string PenaltyForNoShow { get; set; }
        public bool WithCertification { get; set; }
        public decimal Price { get; set; }
        public string PreRequisite { get; set; }
        public string Code { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool Posted { get; set; }
        public int ID_FilingStatus { get; set; }
        public int? BondInMonths { get; set; }
        public int? ID_PaymentTerms { get; set; }
        public bool HideTrainingBond { get; set; }
        //view
        public string Company { get; set; }
        public string FilingStatus { get; set; }
        public int? ID_Status { get; set; }
        public string ReasonForNotAttending { get; set; }
        public int? ID_EmployeeFilingStatus { get; set; }
        public int? ApproverLevel { get; set; }
        public string Approver1 { get; set; }
        public string Approver2 { get; set; }
        public string Approver3 { get; set; }
        public int? ID_Approver1 { get; set; }
        public int? ID_Approver2 { get; set; }
        public int? ID_Approver3 { get; set; }
        public int? ID_Level { get; set; }
        public string Employee { get; set; }
        public int? ID_Employee { get; set; }
        public string EmployeeFilingStatus { get; set; }
    }

    [NotMapped]
    public class vTrainingSummary {
        public int ID { get; set; }
        public int ID_Company { get; set; }
        public string ReferenceNumber { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public double? CourseRating { get; set; }
        public string Status { get; set; }

    }

}
