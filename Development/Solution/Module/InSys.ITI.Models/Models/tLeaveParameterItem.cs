using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tLeaveParameterItem")]
    public class tLeaveParameterItem
    {
        public int ID { get; set; }
        public int ID_LeavePayrollItem { get; set; }
        public decimal InitialValue { get; set; }
        public int ID_LeaveAccrualType { get; set; }
        public int ID_LeaveParameterItemReferenceDate { get; set; }
        public int ID_LeaveParameter { get; set; }
        public int? SeqNo { get; set; }
        public bool? IsActive { get; set; }
        public string Comment { get; set; }
        public int? ID_AccrualOption { get; set; }
        public int? AccrualDay { get; set; }
    }
}
