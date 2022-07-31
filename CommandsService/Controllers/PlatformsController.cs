using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/commands/[controller]")]
    public class PlatformsController : Controller
    {
        [HttpPost]
        public IActionResult TestInboundConnection()
        {
            Console.WriteLine("---> Inbound POST # Command Service");
            return Ok("Inbound test from platform controller");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}