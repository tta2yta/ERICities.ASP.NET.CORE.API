using AutoMapper;
using CitiesAPI.ASP.NET.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesAPI.ASP.NET.CORE.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<Entitiy.City, CityWithouPointOfInterest>();       }
    }
}
