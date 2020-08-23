using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using CitiesAPI.ASP.NET.CORE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public IActionResult CreatePointOfInterest(int cityId, 
             [FromBody] PointOfInterestCreationDto pointOfInterestCreationDto)
        {
            if(pointOfInterestCreationDto.name == pointOfInterestCreationDto.description)
            {
                ModelState.AddModelError(
                    "description",
                    "Please provide another value, name and description should not be thesame");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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

        [HttpPut("{id}")]
        public IActionResult UpdatePointOfInterest(int cityid, int id, 
            [FromBody] PointOfInterestUpdateDto pointOfInterestUpdateDto)
        {
            if (pointOfInterestUpdateDto.name == pointOfInterestUpdateDto.description)
            {
                ModelState.AddModelError(
                    "description",
                    "Please provide another value, name and description should not be thesame");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityid);
            if(city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(
                p => p.Id == id);
            if (pointOfInterestFromStore == null)
                return NotFound();
            pointOfInterestFromStore.Name = pointOfInterestUpdateDto.name;
            pointOfInterestFromStore.Name = pointOfInterestUpdateDto.name;

            return NoContent();

        }

        public IActionResult partiallyUpadatePointOfInterest(int cityid, int id,
            [FromBody] JsonPatchDocument<PointOfInterestUpdateDto> patchDoc)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityid);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(
                p => p.Id == id);
            if (pointOfInterestFromStore == null)
                return NotFound();

            var pointOfInterestPatch = new PointOfInterestUpdateDto()
            {
                name = pointOfInterestFromStore.Name,
                description = pointOfInterestFromStore.Description
            };
            patchDoc.ApplyTo(pointOfInterestPatch, ModelState);

            if (pointOfInterestPatch.name == pointOfInterestPatch.description)
            {
                ModelState.AddModelError(
                    "description",
                    "Please provide another value, name and description should not be thesame");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!TryValidateModel(pointOfInterestPatch))
                return BadRequest();

            pointOfInterestFromStore.Name = pointOfInterestPatch.name;
            pointOfInterestFromStore.Name = pointOfInterestPatch.name;

            return NoContent();
        }
    }
}