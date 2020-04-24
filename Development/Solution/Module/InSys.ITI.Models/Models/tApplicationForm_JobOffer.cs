using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tApplicationForm_JobOffer")]
    public class tApplicationForm_JobOffer
    {
        public int ID { get; set; }
        public int ID_ApplicationForm { get; set; }
        public int ID_Designation { get; set; }
        public int? ID_Department { get; set; }
        public int? ID_Division { get; set; }
        public int? ID_JobClass { get; set; }
        public int? ID_EmployeeStatus { get; set; }
        public int? ID_CostCenter { get; set; }
        public int? ID_NatureOfRequests { get; set; }
        public decimal Salary { get; set; } = 0;
        public decimal AnnualizedSalary { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Division { get; set; }
        public string JobClass { get; set; }
        public string EmployeeStatus { get; set; }
        public string CostCenter { get; set; }
        public string NatureOfRequests { get; set; }
        public string Notes { get; set; }
    }

    [NotMapped]
    public class vJobOffer_Default
    {
        public int ID_Designation { get; set; }
        public int? ID_Department { get; set; }
        public int? ID_Division { get; set; }
        public int? ID_JobClass { get; set; }
        public int? ID_EmployeeStatus { get; set; }
        public int? ID_NatureOfRequests { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Division { get; set; }
        public string JobClass { get; set; }
        public string EmployeeStatus { get; set; }
        public string NatureOfRequests { get; set; }
    }
}
