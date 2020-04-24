using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tEmployee_JobOffer")]
    public partial class tEmployee_JobOffer
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int? ID_ApplicationForm { get; set; }
        public decimal? Salary { get; set; }
        public decimal? AnnualizedSalary { get; set; }
        public int? ID_Designation { get; set; }
        public string Designation { get; set; }
        public int? ID_Department { get; set; }
        public string Department { get; set; }
        public int? ID_JobClass { get; set; }
        public string JobClass { get; set; }
        public int? ID_Division { get; set; }
        public string Division { get; set; }
        public int? ID_EmployeeStatus { get; set; }
        public string EmployeeStatus { get; set; }
        public int? ID_CostCenter { get; set; }
        public string CostCenter { get; set; }
        public int? ID_NatureOfRequests { get; set; }
        public string NatureOfRequests { get; set; }
        public string Notes { get; set; }
    }

    [NotMapped]
    public partial class vEmployee_JobOffer
    {
        public int ID_Designation { get; set; }
        public string Designation { get; set; }
        public int? ID_EmployeeStatus { get; set; }
        public string EmployeeStatus { get; set; }
        public int? ID_NatureOfRequests { get; set; }
        public string NatureOfRequests { get; set; }
    }
}
