using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tPersona")]
    public class tPersona
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string NickName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Father { get; set; }
        public string Mother { get; set; }
        public string Spouse { get; set; }
        public DateTime? SpouseBirthDate { get; set; }
        public string SpouseEmployer { get; set; }
        public string SpouseOccupation { get; set; }
        public string SSSNo { get; set; }
        public string PhilHealthNo { get; set; }
        public string HDMFNo { get; set; }
        public string TINNo { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string EmailAddress { get; set; }
        public string AlternateEmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string ImageFile { get; set; }
        public string ImageFile_GUID { get; set; }
        public int? ID_Gender { get; set; }
        public int? ID_Religion { get; set; }
        public int? ID_Nationality { get; set; }
        public int? ID_Citizenship { get; set; }
        public int? ID_CivilStatus { get; set; }
        public int? ID_BloodType { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string GSISNo { get; set; }
        public int? ID_SSSStatus { get; set; }
        public decimal? SalaryDesired { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public string ACRNo { get; set; }
        public string WorkPermitNo { get; set; }
        public string PassportNo { get; set; }
        public string Suffix { get; set; }
        public string FatherOccupation { get; set; }
        public DateTime? FatherBirthDate { get; set; }
        public string MotherOccupation { get; set; }
        public DateTime? MotherBirthDate { get; set; }
        public int? ID_Designation { get; set; }
        public int? ID_Designation2 { get; set; }
        public string ResumeFile { get; set; }
        public string ResumeFile_GUID { get; set; }
        public int? ID_ApplicationForm { get; set; }
        public int? ID_Company { get; set; }
    }

    public class vPersonaLookUp
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class vPersona
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string NickName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Father { get; set; }
        public string Mother { get; set; }
        public string Spouse { get; set; }
        public DateTime? SpouseBirthDate { get; set; }
        public string SpouseEmployer { get; set; }
        public string SpouseOccupation { get; set; }
        public string SSSNo { get; set; }
        public string PhilHealthNo { get; set; }
        public string HDMFNo { get; set; }
        public string TINNo { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string EmailAddress { get; set; }
        public string AlternateEmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string ImageFile { get; set; }
        public string ImageFile_GUID { get; set; }
        public int? ID_Gender { get; set; }
        public int? ID_Religion { get; set; }
        public int? ID_Nationality { get; set; }
        public int? ID_Citizenship { get; set; }
        public int? ID_CivilStatus { get; set; }
        public int? ID_BloodType { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string GSISNo { get; set; }
        public int? ID_SSSStatus { get; set; }
        public decimal? SalaryDesired { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public string ACRNo { get; set; }
        public string WorkPermitNo { get; set; }
        public string PassportNo { get; set; }
        public string Suffix { get; set; }
        public string FatherOccupation { get; set; }
        public DateTime? FatherBirthDate { get; set; }
        public string MotherOccupation { get; set; }
        public DateTime? MotherBirthDate { get; set; }
        public int? ID_Designation { get; set; }
        public int? ID_Designation2 { get; set; }
        public string ResumeFile { get; set; }
        public string ResumeFile_GUID { get; set; }
        public int? ID_ApplicationForm { get; set; }
        public int? ID_Company { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public string Citizenship { get; set; }
        public string CivilStatus { get; set; }
        public string BloodType { get; set; }
        public string SSSStatus { get; set; }
    }
}
