using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tPayrollItemRate")]
    public class tPayrollItemRate
    {
        public int ID { get; set; }
        public int ID_Parameter { get; set; }
        public int ID_PayrollItem { get; set; }
        public decimal Rate { get; set; }
        public decimal Amt { get; set; }
        public int? SeqNo { get; set; }
        public bool? IsActive { get; set; }
        public string Comment { get; set; }
    }

    [NotMapped]
    public class vPayrollItemRate
    {
        public int ID { get; set; }
        public int ID_Parameter { get; set; }
        public int ID_PayrollItem { get; set; }
        public string PayrollItem { get; set; }
        public decimal Rate { get; set; }
        public decimal Amt { get; set; }
        public int? SeqNo { get; set; }
        public bool? IsActive { get; set; }
        public string Comment { get; set; }
    }
}
