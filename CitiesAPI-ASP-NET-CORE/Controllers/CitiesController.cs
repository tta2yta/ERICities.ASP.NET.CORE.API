using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CitiesAPI.ASP.NET.CORE.Models;
using CitiesAPI.ASP.NET.CORE.Services;

namespace CitiesAPI.ASP.NET.CORE.Controllers
{
  //[Route("api/[controller]")]
    [Route("api/cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }
     
        [HttpGet]
        public ActionResult GetCities()
        {
            //  return Ok(CitiesDataStore.Current.Cities);

            var cityEntities = _cityInfoRepository.GetCities();
            var results = new List<CityWithouPointOfInterest>();

            foreach( var cityEntity in cityEntities)
            {
                results.Add(new CityWithouPointOfInterest()
                {
                    Id=cityEntity.Id,
                    name=cityEntity.Name,
                    description=cityEntity.Description
                });
            }
            return Ok(results);
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