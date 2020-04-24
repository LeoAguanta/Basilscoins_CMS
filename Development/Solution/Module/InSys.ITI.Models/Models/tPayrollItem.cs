using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tPayrollItem")]
    public class tPayrollItem
    { 
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? ID_Income { get; set; }
        public int ID_PayrollItemType { get; set; }
        public int ID_PayrollItemCategory { get; set; }
        public int? ID_PayrollItemGroup { get; set; }
        public int? ID_NormalBalance { get; set; }
        public bool? IsActive { get; set; }
        public string Comment { get; set; }
        public int? Priority { get; set; }
        public int? ID_Transaction_Created { get; set; }
        public int? ID_Transaction_Modified { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeModified { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? ForSSS { get; set; }
        public bool? ForPHIC { get; set; }
        public bool? ForHDMF { get; set; }
        public bool ForTMonth { get; set; }
        public bool ForGSIS { get; set; }
        public int? BonusPriority { get; set; }
        public string SpecialGL { get; set; }
        public int SeqNo { get; set; }

    }
}
