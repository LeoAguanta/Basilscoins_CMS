using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tPayrollItemSetup")]
    public class tPayrollItemSetup
    {
        public int ID { get; set; }
        public int? ID_Parameter { get; set; }
        public int ID_PayrollItem { get; set; }
        public int? ID_Employee { get; set; }
        public int? ID_PayrollItemSetupOption { get; set; }
        public decimal Amt { get; set; }
        public bool Period1 { get; set; }
        public bool Period2 { get; set; }
        public bool Period3 { get; set; }
        public bool Period4 { get; set; }
        public bool Period5 { get; set; }
        public bool Period6 { get; set; }
        public bool Period7 { get; set; }
        public bool Period8 { get; set; }
        public bool Period9 { get; set; }
        public int? SeqNo { get; set; }
        public bool? IsActive { get; set; }
        public string Comment { get; set; }
        public int? ID_Transaction_Created { get; set; }
        public int? ID_Transaction_Modified { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
    }


    public class vPayrollItemSetup
    {
        public int ID { get; set; }
        public int? ID_Parameter { get; set; }
        public int ID_PayrollItem { get; set; }
        public string PayrollItem { get; set; }
        public int? ID_Employee { get; set; }
        public int? ID_PayrollItemSetupOption { get; set; }
        public decimal Amt { get; set; }
        public bool Period1 { get; set; }
        public bool Period2 { get; set; }
        public bool Period3 { get; set; }
        public bool Period4 { get; set; }
        public bool Period5 { get; set; }
        public bool Period6 { get; set; }
        public bool Period7 { get; set; }
        public bool Period8 { get; set; }
        public bool Period9 { get; set; }
        public int? SeqNo { get; set; }
        public bool? IsActive { get; set; }
        public string Comment { get; set; }
        public int? ID_Transaction_Created { get; set; }
        public int? ID_Transaction_Modified { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
    }
}
