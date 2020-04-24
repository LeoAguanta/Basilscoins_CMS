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

namespace InSys.ITI.Nationality
{
    [Route("api/[controller]/[action]")]
    public class NationalityController : BaseController
    {

        public NationalityController(IHostingEnvironment hostingEnvironment, IAntiforgery _antiForgery) : base(hostingEnvironment, _antiForgery) { }

    }
}
