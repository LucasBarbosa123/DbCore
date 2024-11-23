using DbCoreDatabase.Data;
using Microsoft.AspNetCore.Mvc;

namespace DbCore.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("Test")]
        public IActionResult Test()
        {
            return Ok("Opa!!");
        }
    }
}
