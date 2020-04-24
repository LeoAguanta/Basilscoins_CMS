using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tEmployee")]
    public class tEmployee
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string BankAcctNo { get; set; }
        public string AccessNo { get; set; }
        public bool? IsRequiredToLog { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? RegularizationDate { get; set; }
        public int? ID_Persona { get; set; }
        public int ID_EmployeeStatus { get; set; }
        public int? ID_PayrollScheme { get; set; }
        public int? ID_PayrollFrequency { get; set; }
        public int? ID_CompanyBankAcct { get; set; }
        public int? ID_TaxExemption { get; set; }
        public int? ID_PaymentMode { get; set; }
        public int? ID_Parameter { get; set; }
        public int? ID_LeaveParameter { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateTimeCreated { get; set; } = DateTime.Now;
        public decimal MonthlyRate { get; set; }
        public decimal DailyRate { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal DMonthlyRate { get; set; }
        public decimal DDailyRate { get; set; }
        public decimal DHourlyRate { get; set; }
        public decimal TaxRate { get; set; }
        public bool IsTerminated { get; set; } = false;
        public bool IsHired { get; set; } = false;
        public decimal PrevEmpTaxableAmt { get; set; }
        public decimal PrevEmpWitholdingTax { get; set; }
        public DateTime? PrevEmpEndDate { get; set; }
        public int ID_Currency { get; set; }
        public decimal Prev13thMonth { get; set; }
        public decimal PrevCompensation { get; set; }
        public decimal NonTaxPrevContribution { get; set; }
        public decimal NonTaxPrev13thMonth { get; set; }
        public decimal NonTaxPrevCompensation { get; set; }
        public decimal DailySMW { get; set; }
        public decimal MonthlySMW { get; set; }
        public bool? MWE { get; }
        public string CompanyEmail { get; set; }
        public string CardNo { get; set; }
        public int ID_PayrollStatus { get; set; }
        public int? ID_PayrollClassification { get; set; }
        public int? ID_FacultyType { get; set; }
        public int? ID_FacultyInstitute { get; set; }
        public int ID_AccountNumberType { get; set; }
        public DateTime? HiredDate { get; set; }
        public int ID_Company { get; set; }
        public int? ID_Org { get; set; }
        public string CompanyMobileNo { get; set; }
        public string CompanyPhoneNo { get; set; }
    }
    [NotMapped]
    public class vEmployee
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string BankAcctNo { get; set; }
        public string AccessNo { get; set; }
        public bool? IsRequiredToLog { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? RegularizationDate { get; set; }
        public int ID_Persona { get; set; }
        public int ID_EmployeeStatus { get; set; }
        public int? ID_PayrollScheme { get; set; }
        public int? ID_PayrollFrequency { get; set; }
        public int? ID_CompanyBankAcct { get; set; }
        public int? ID_TaxExemption { get; set; }
        public int? ID_PaymentMode { get; set; }
        public int? ID_Parameter { get; set; }
        public int? ID_LeaveParameter { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public decimal MonthlyRate { get; set; }
        public decimal DailyRate { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal DMonthlyRate { get; set; }
        public decimal DDailyRate { get; set; }
        public decimal DHourlyRate { get; set; }
        public decimal TaxRate { get; set; }
        public bool IsTerminated { get; set; }
        public bool IsHired { get; set; }
        public decimal PrevEmpTaxableAmt { get; set; }
        public decimal PrevEmpWitholdingTax { get; set; }
        public DateTime? PrevEmpEndDate { get; set; }
        public int ID_Currency { get; set; }
        public decimal Prev13thMonth { get; set; }
        public decimal PrevCompensation { get; set; }
        public decimal NonTaxPrevContribution { get; set; }
        public decimal NonTaxPrev13thMonth { get; set; }
        public decimal NonTaxPrevCompensation { get; set; }
        public decimal DailySMW { get; set; }
        public decimal MonthlySMW { get; set; }
        public bool MWE { get; }
        public string CompanyEmail { get; set; }
        public string CardNo { get; set; }
        public int ID_PayrollStatus { get; set; }
        public int? ID_PayrollClassification { get; set; }
        public int? ID_FacultyType { get; set; }
        public int? ID_FacultyInstitute { get; set; }
        public int ID_AccountNumberType { get; set; }
        public DateTime? HiredDate { get; set; }
        public int ID_Company { get; set; }
        public int? ID_Org { get; set; }
        public string CompanyMobileNo { get; set; }
        public string CompanyPhoneNo { get; set; }
        //view
        public string Persona { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Company { get; set; }
        public string Branch { get; set; }
        public string Division { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        public string JobClassGroup { get; set; }
        public string JobClass { get; set; }
        public string Designation { get; set; }
        public string CostCenter { get; set; }
        public string EmployeeStatus { get; set; }
        public string PayrollScheme { get; set; }
        public string PayrollFrequency { get; set; }
        public string CompanyBankAcct { get; set; }
        public string TaxExemption { get; set; }
        public string PaymentMode { get; set; }
        public string Parameter { get; set; }
        public string LeaveParameter { get; set; }
        //public string WeeklySchedule { get; set; }
        public string Currency { get; set; }
        //public string CompanyBankAcct2 { get; set; }
        public string PayrollStatus { get; set; }
        public string PayrollClassification { get; set; }
        public string FacultyType { get; set; }
        public string FacultyInstitute { get; set; }
        public string AccountNumberType { get; set; }
        public string MiddleInitial { get; set; }
    }

    [NotMapped]

    public class vEmployeeDesignation {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public int ID_Company { get; set; }
    }

    [NotMapped]
    public class vEmployeeRecordList {
        public int ID { get; set; }
        public int ID_Company { get; set; }
        public string Code { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string EmployeeStatus { get; set; }
        public string PayrollScheme { get; set; }
        public string PayrollFrequency { get; set; }
        public string Parameter { get; set; }
        public string LeaveParameter { get; set; }
        public string Branch { get; set; }
        public string Division { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        public string JobClassGroup { get; set; }
        public string JobClass { get; set; }
        public string Designation { get; set; }
    }   
}
