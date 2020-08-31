using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitiesAPI.ASP.NET.CORE.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CitiesAPI.ASP.NET.CORE.Controllers
{
    [Route("api/testdatabase")]
    [ApiController]
    public class DummpyController : ControllerBase
    {

        private readonly CityInfoContext ctx;

        public DummpyController(CityInfoContext _ctx)
        {
            ctx = _ctx ?? throw new ArgumentNullException(nameof(_ctx));
        }

        [HttpGet]
        public IActionResult Testdb()
        {
            return Ok();
        }
    }
}
