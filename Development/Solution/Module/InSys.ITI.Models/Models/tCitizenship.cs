﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tCitizenship")]
    public class tCitizenship
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}
