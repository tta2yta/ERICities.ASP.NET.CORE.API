using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CitiesAPI.ASP.NET.CORE.Entitiy;
using CitiesAPI.ASP.NET.CORE.Models;
using CitiesAPI.ASP.NET.CORE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace CitiesAPI.ASP.NET.CORE.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {

        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _localMailService;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointsOfInterestController (ILogger<PointsOfInterestController> logger,
            IMailService localMailService, ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _localMailService = localMailService ?? throw new ArgumentNullException(nameof(logger));
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetPointsOfInterest(int cityid)
        {
            try
            {
              
                if (! _cityInfoRepository.CityExists(cityid))
                {
                    _logger.LogInformation($"City with citi id {cityid} is not found ");
                    return NotFound();
                }

                var pointOfinterestForCity = _cityInfoRepository.GetPointOfInterests(cityid);
                /*var results = new List<PointOfInterestDto>();
                foreach(var poi in pointOfinterestForCity)
                {
                    results.Add(new PointOfInterestDto()
                    {
                        Id = poi.Id,
                        Name = poi.Name,
                        Description = poi.Description
                    });
                }
                return Ok(results);*/
                return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointOfinterestForCity));
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Exception raised while getting point of interest from city");
                return StatusCode(500, "A proplem arises while handling you request");
            }


        }
        //   [HttpGet("{id}", Name = "GetPointOfInterest")]
        [HttpGet("{id}")]
        public IActionResult GetPointOfInterest(int cityid, int id)
        {
            if (!_cityInfoRepository.CityExists(cityid))
            {
                _logger.LogInformation($"City with citi id {cityid} is not found ");
                return NotFound();
            }

            var pointOfInterest = _cityInfoRepository.GetPointOfInterest(cityid, id);
            if (pointOfInterest == null)
                return NotFound();

            /*  var result = new PointOfInterestDto()
              {
                  Id = pointOfInterest.Id,
                  Name = pointOfInterest.Name,
                  Description = pointOfInterest.Description
              };

              return Ok(result);*/
            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));

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

           /* var city = CitiesDataStore.Current.Cities
                .SingleOrDefault(c => c.Id == cityId);*/

            if (! _cityInfoRepository.CityExists(cityId))
                return NotFound();
            /*
                        var newPointOfInterestId = CitiesDataStore.Current.Cities
                            .SelectMany(p => p.PointsOfInterest).Max(c => c.Id);*/
            var finalPointOfInterest = _mapper.Map<PointOfInterest>(pointOfInterestCreationDto);
            _cityInfoRepository.AddPointOfInterest(cityId, finalPointOfInterest);
            _cityInfoRepository.Save();

            var createdPointofInterest = _mapper.Map<PointOfInterestDto>(finalPointOfInterest);

          /*  var newPointOfInterest = new PointOfInterestDto()
            {
                Id = ++newPointOfInterestId,
                Name = pointOfInterestCreationDto.name,
                Description = pointOfInterestCreationDto.description
            };
            city.PointsOfInterest.Add(newPointOfInterest);*/

            return CreatedAtRoute("GetPointOfInterest",
                new { cityId, id = createdPointofInterest.Id }, createdPointofInterest);
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
            pointOfInterestFromStore.Description = pointOfInterestUpdateDto.description;

            return NoContent();

        }

        [HttpPatch("{id}")]
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
            pointOfInterestFromStore.Description = pointOfInterestPatch.description;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult UpdatePointOfInterest(int cityid, int id)
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

            city.PointsOfInterest.Remove(pointOfInterestFromStore);
            _localMailService.send("Point of Interst deleted",
                $"Point of Interest {pointOfInterestFromStore.Name} with id {pointOfInterestFromStore.Id}");

            return NoContent();
        }
    }
}