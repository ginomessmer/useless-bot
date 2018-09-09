using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UselessBot.HttpRuntime.Controllers
{
    [Route("api/settings")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetSettings()
        {
            return Ok();
        }
    }
}
