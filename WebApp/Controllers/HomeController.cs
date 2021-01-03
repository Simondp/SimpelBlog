using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpelBlog.Logging;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISimpelLogger _logger;

        public HomeController(ISimpelLogger logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
	   _logger.LogDebug("Hit controller"); 	
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}

