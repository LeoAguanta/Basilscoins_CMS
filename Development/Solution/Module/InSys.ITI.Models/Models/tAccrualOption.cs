using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tAccrualOption")]
    public class tAccrualOption
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ImageFile { get; set; }
        public int? SeqNo { get; set; }
        public bool? IsActive { get; set; }
        public string Comment { get; set; }
    }
}
