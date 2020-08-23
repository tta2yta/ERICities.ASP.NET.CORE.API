using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using CitiesAPI.ASP.NET.CORE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CitiesAPI.ASP.NET.CORE.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPointsOfInterest(int cityid)
        {
            var city = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityid);

            if (city == null)
                return NotFound();

            return Ok(city.PointsOfInterest);


        }
        //   [HttpGet("{id}", Name = "GetPointOfInterest")]
        [HttpGet("{id}")]
        public IActionResult GetPointOfInterest(int cityid, int id)
        {
            var city = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityid);

            if (city == null)
                return NotFound();

            var pointOfInterest = city.PointsOfInterest
                .FirstOrDefault(p => p.Id == id);

            if (pointOfInterest == null)
                return NotFound();
                    
           return Ok(pointOfInterest);


        }

          [HttpPost]
        public IActionResult CreatePointOfInterest(int cityId, PointOfInterestCreationDto pointOfInterestCreationDto)
        {
            var city = CitiesDataStore.Current.Cities
                .SingleOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();

            var newPointOfInterestId = CitiesDataStore.Current.Cities
                .SelectMany(p => p.PointsOfInterest).Max(c => c.Id);

            var newPointOfInterest = new PointOfInterestDto()
            {
                Id = ++newPointOfInterestId,
                Name = pointOfInterestCreationDto.name,
                Description = pointOfInterestCreationDto.description
            };
            city.PointsOfInterest.Add(newPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                new { city, id = newPointOfInterest.Id }, newPointOfInterest);
        }
    }
}