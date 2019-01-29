using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ResourceOwnerPasswords.APi.Controllers
{
    [Route("Controller")]
    [Authorize]
    public class ApiController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return new JsonResult(from c in User.Claims select new { c.Subject, c.Value });
        }
    }
}