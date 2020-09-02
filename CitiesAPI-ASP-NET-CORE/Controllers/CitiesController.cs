using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CitiesAPI.ASP.NET.CORE.Models;
using CitiesAPI.ASP.NET.CORE.Services;
using AutoMapper;

namespace CitiesAPI.ASP.NET.CORE.Controllers
{
  //[Route("api/[controller]")]
    [Route("api/cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper;
        }
     
        [HttpGet]
        public ActionResult GetCities()
        {
            //  return Ok(CitiesDataStore.Current.Cities);

            var cityEntities = _cityInfoRepository.GetCities();
          /*  var results = new List<CityWithouPointOfInterest>();

            foreach( var cityEntity in cityEntities)
            {
                results.Add(new CityWithouPointOfInterest()
                {
                    Id=cityEntity.Id,
                    name=cityEntity.Name,
                    description=cityEntity.Description
                });
            }*/
            return Ok(_mapper.Map<IEnumerable<CityWithouPointOfInterest>>(cityEntities));
        }

        [HttpGet("{id}")]
        public IActionResult Getcity(int id, bool includePointOfInterest=false)
        {
            var city = _cityInfoRepository.GetCity(id, includePointOfInterest);
            if (city == null)
                return NotFound();
            if(includePointOfInterest)
            {
                /*var cityresult = new CityDto()
                {
                    Id = city.Id,
                    Name = city.Name,
                    Description = city.Description
                };

                foreach(var poi in city.pointOfInterests)
                {
                    cityresult.PointsOfInterest.Add(new PointOfInterestDto()
                    {
                        Id = poi.Id,
                        Name = poi.Name,
                        Description = poi.Description
                    });
                }*/
                // return Ok(cityresult);
                return Ok(_mapper.Map<CityDto>(city));

            }

            /* var cityWithOutPointOfInterest = new CityWithouPointOfInterest()
             {
                 Id = city.Id,
                 name = city.Name,
                 description = city.Description
             };

             return Ok(cityWithOutPointOfInterest);*/
            return Ok(_mapper.Map<CityWithouPointOfInterest>(city));
        }
    }
}