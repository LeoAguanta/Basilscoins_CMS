﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tLeaveDayType")]
    public class tLeaveDayType
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
