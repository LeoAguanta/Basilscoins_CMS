using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tCurrencyRate")]
    public class tCurrencyRate
    {
        public int ID { get; set; }
        public int ID_Currency { get; set; }
        public decimal Rate { get; set; }
        public DateTime Date { get; set; }
        public bool? IsActive { get; set; }
        public string Comment { get; set; }
    }
}
