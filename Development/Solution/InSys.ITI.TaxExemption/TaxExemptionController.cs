﻿using InSys.ITI.Controller;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;


namespace InSys.ITI.TaxExemption
{
    [Route("api/[controller]/[action]")]
    class TaxExemptionController : BaseController
    {
        public TaxExemptionController(IHostingEnvironment hostingEnvironment, IAntiforgery _antiForgery) : base(hostingEnvironment, _antiForgery)
        {
        }
    }
}
