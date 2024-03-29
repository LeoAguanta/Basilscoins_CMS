﻿using InSys.ITI.Controller;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using z.Data;

namespace InSys.ITI.CivilStatus
{
    [Route("api/[controller]/[action]")]
    public class CivilStatusController : BaseController
    {

        public CivilStatusController(IHostingEnvironment hostingEnvironment, IAntiforgery _antiForgery) : base(hostingEnvironment, _antiForgery) { }
    }
}
