using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tKioskMrf")]
    public class tKioskMRF
    {
        public int ID { get; set; }
        public DateTime PostingDate { get; set; }
        public string MRFNumber { get; set; }
        public string Description { get; set; }
        public int ID_Company { get; set; }
        public int? ID_Department { get; set; }
        public int ID_RecruitmentStatus { get; set; }
        public int ID_EmployeeStatus { get; set; }
        public int ID_Designation { get; set; }
        public int? ID_JobClass { get; set; }
        public int RequestHeadCount { get; set; }
        public string Qualification { get; set; }
        public string WorkExperience { get; set; }
        public string OtherDetails { get; set; }
        public int? ID_NatureOfRequests { get; set; }
        public int? ReplacementReason { get; set; }
        public int? ID_PersonToBeReplaced { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public int? ID_CreatedBy { get; set; }
        public int? ID_ModifiedBy { get; set; }
        public bool Posted { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ID_RequestedBy { get; set; }
        public DateTime? RequestedDate { get; set; }
        public int? ID_ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string Attachment { get; set; }
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public int? FemaleCount { get; set; }
        public int? MaleCount { get; set; }
        public int? NoGenderPreference { get; set; }
        public bool IsBudgeted { get; set; }
        public string LocationStoreName { get; set; }
        public string LocationStoreCode_CostCenter { get; set; }
        public string LocationBranchArea { get; set; }
        public string LocationReportingSchedule { get; set; }
        public string LocationAddress { get; set; }
        public bool PostForJobPosting { get; set; }
        public int? ApproverLevel { get; set; }
    }
    [NotMapped]
    public class vKioskMRF
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public int? ID_Designation { get; set; }
        public int? HeadCount { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public int? ID_UserCreatedBy { get; set; }
        public int? ID_RecruitmentStatus { get; set; }
        public bool? IsActive { get; set; }
        //view
        public string Designation { get; set; }
        public string RecruitmentStatus { get; set; }
        public string Description { get; set; }
        public string JobSummary { get; set; }
        public string WorkExperience { get; set; }
        public string RequiredLicenses { get; set; }
        public string Qualifications { get; set; }
        public string Requirements { get; set; }
    }
}
