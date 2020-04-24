using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [NotMapped]
    public class MRFLookup
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string MRFNumber { get; set; }
        public int ID_MRF { get; set; }
        public DateTime PostingDate { get; set; }
    }
}
