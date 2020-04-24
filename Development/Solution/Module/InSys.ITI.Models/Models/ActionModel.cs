using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace InSys.ITI.Models.Models
{
    public class ActionModel
    {
        public string Name { get; set; }
        public JObject Parameter { get; set; }
    }
}
