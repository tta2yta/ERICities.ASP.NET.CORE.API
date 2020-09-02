using AutoMapper;
using CitiesAPI.ASP.NET.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesAPI.ASP.NET.CORE.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<Entitiy.PointOfInterest, PointOfInterestDto>();
        }
    }
}
