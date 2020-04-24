using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tCompanyBankAcct")]
    public class tCompanyBankAcct
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public int ID_Company { get; set; }
        public int ID_Bank { get; set; }
        public string No { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string AttentionHeader { get; set; }
        public int? SeqNo { get; set; }
        public bool? IsActive { get; set; }
        public string Comment { get; set; }
    }
}
