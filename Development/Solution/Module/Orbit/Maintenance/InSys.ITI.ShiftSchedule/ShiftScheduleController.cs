﻿using InSys.ITI.Controller;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace InSys.ITI.ShiftSchedule
{
    [Route("api/[controller]/[action]")]
    public class ShiftScheduleController : BaseController
    {
        public ShiftScheduleController(IHostingEnvironment hostingEnvironment, IAntiforgery _antiForgery) : base(hostingEnvironment, _antiForgery)
        {
        }
    }
}
