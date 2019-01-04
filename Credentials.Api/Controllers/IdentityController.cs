using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Credentials.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class IdentityController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}