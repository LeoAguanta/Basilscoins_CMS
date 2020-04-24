using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tCompanyRefNum")]
    public class tCompanyRefNum
    {
        public int ID { get; set; }
        public int ID_Company { get; set; }
        public string TableName { get; set; }
        public string Prefix { get; set; }
        public string AdditionalPrefix { get; set; }
        public int MaxLength { get; set; }
        public int LastRefNum { get; set; }
    }
}
