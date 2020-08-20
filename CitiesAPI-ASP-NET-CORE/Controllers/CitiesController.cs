using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CitiesAPI.ASP.NET.CORE.Models;

namespace CitiesAPI.ASP.NET.CORE.Controllers
{
  //[Route("api/[controller]")]
    [Route("api/cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {

      //  [HttpGet("api/cities")]
     // [HttpGet]
        /*  public JsonResult GetCities()
          {
              return new JsonResult(new List<object>(){
                     new {id=1, name="Asmara"},
                     new{id=2, name="Keren"}
              }); 
          }*/
        [HttpGet]
        public ActionResult GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public IActionResult Getcity(int id)
        {
            var returnCity = CitiesDataStore.Current.Cities.
                FirstOrDefault(c => c.Id == id);
            if (returnCity == null)
            {
                return NotFound();
            }
            return Ok(returnCity);
        }
    }
}