﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); //index.cshtml vznikol presunutim index.html z wwwroot (.html je validnym .cshtml suborom)
        }
    }
}
