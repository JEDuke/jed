using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using jamesethanduke.Models;
using Microsoft.AspNetCore.Authorization;

namespace jamesethanduke.Controllers
{
    [AllowAnonymous]
    public class PrivacyController : Controller
    {
        private readonly ILogger<AboutController> _logger;

        public PrivacyController(ILogger<AboutController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}