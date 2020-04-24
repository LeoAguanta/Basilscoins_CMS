using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tApplicationForm")]
    public class tApplicationForm
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public DateTime? TransDate { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Suffix { get; set; }
        public string NickName { get; set; }
        public string UploadedResume { get; set; }
        public string UploadedResume_GUID { get; set; }
        public int ID_Designation { get; set; }
        public int? ID_Designation2 { get; set; }
        public int? ID_MRF { get; set; }
        public DateTime? AvailableStartDate { get; set; }
        public decimal? DesiredPay { get; set; }
        public int? ID_SourcingPartner { get; set; }
        public string SourcingPartner { get; set; }
        public string SSSNo { get; set; }
        public string HDMFNo { get; set; }
        public string PhilHealthNo { get; set; }
        public string TINNo { get; set; }
        public string GSISNo { get; set; }
        public string UMID { get; set; }
        public DateTime? BirthDate { get; set; } = DateTime.Now;
        public int? Age { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string ImageFile { get; set; }
        public string ImageFile_GUID { get; set; }
        public int? ID_Salutation { get; set; }
        public int? ID_Gender { get; set; }
        public int? ID_Nationality { get; set; }
        public int? ID_Citizenship { get; set; }
        public int? ID_Religion { get; set; }
        public int? ID_CivilStatus { get; set; }
        public int? ID_BloodType { get; set; }
        public int? ID_RecruitmentStatus { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DatetimeCreated { get; set; }
        public int? ID_UserCreatedBy { get; set; }
        public string Nationality_Others { get; set; }
        public string Citizenship_Others { get; set; }
        public string Religion_Others { get; set; }
        public int? ID_BirthPlace { get; set; }
        public bool IsEndorsed { get; set; }
        public int? ID_PreparedBy { get; set; }
        public string RefNum { get; set; }
        public bool IsHired { get; set; } = false;
    }
    [NotMapped]
    public class vApplicationForm
    {
        public int ID { get; set; }
        public int ID_Designation { get; set; }
        public DateTime? TransDate { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public string MRFNumber { get; set; }
        public string Status { get; set; }
        public string RefNum { get; set; }
        public int? ID_RecruitmentStatus { get; set; }
        public string Designation { get; set; }
        public string MiddleInitial { get; set; }
    }
    [NotMapped]
    public class vApplicationForm2
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public DateTime? TransDate { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Suffix { get; set; }
        public string NickName { get; set; }
        public string UploadedResume { get; set; }
        public string UploadedResume_GUID { get; set; }
        public int ID_Designation { get; set; }
        public int? ID_Designation2 { get; set; }
        public int? ID_MRF { get; set; }
        public DateTime? AvailableStartDate { get; set; }
        public decimal? DesiredPay { get; set; }
        public int? ID_SourcingPartner { get; set; }
        public string SSSNo { get; set; }
        public string HDMFNo { get; set; }
        public string PhilHealthNo { get; set; }
        public string TINNo { get; set; }
        public string GSISNo { get; set; }
        public string UMID { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Age { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string ImageFile { get; set; }
        public string ImageFile_GUID { get; set; }
        public int? ID_Salutation { get; set; }
        public int? ID_Gender { get; set; }
        public int? ID_Nationality { get; set; }
        public int? ID_Citizenship { get; set; }
        public int? ID_Religion { get; set; }
        public int? ID_CivilStatus { get; set; }
        public int? ID_BloodType { get; set; }
        public int? ID_RecruitmentStatus { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DatetimeCreated { get; set; }
        public int? ID_UserCreatedBy { get; set; }
        public string Nationality_Others { get; set; }
        public string Citizenship_Others { get; set; }
        public string Religion_Others { get; set; }
        public int? ID_BirthPlace { get; set; }
        public bool IsEndorsed { get; set; }
        public int? ID_PreparedBy { get; set; }
        public string RefNum { get; set; }
        public string MRFNumber { get; set; }
        public bool IsHired { get; set; } = false;
        //view
        public string Designation { get; set; }
        [NotMapped]
        public string Designation2 { get; set; }
        [NotMapped]
        public string SourcingPartner2 { get; set; }
        [NotMapped]
        public string Salutation { get; set; }
        [NotMapped]
        public string Gender { get; set; }
        [NotMapped]
        public string Nationality { get; set; }
        [NotMapped]
        public string Citizenship { get; set; }
        [NotMapped]
        public string Religion { get; set; }
        [NotMapped]
        public string CivilStatus { get; set; }
        [NotMapped]
        public string BloodType { get; set; }
        [NotMapped]
        public string RecruitmentStatus { get; set; }
        [NotMapped]
        public string BirthPlace { get; set; }
        public string PassportNo { get; set; }
        public string DriversLicenseNo { get; set; }
    }
}
