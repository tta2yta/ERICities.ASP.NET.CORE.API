using CitiesAPI.ASP.NET.CORE.Context;
using CitiesAPI.ASP.NET.CORE.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesAPI.ASP.NET.CORE.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;

        public CityInfoRepository( CityInfoContext context)
        {
            _context = context;
        }
        public IEnumerable<City> GetCities()
        {
            return _context.Cities.OrderBy(c=> c.Name).ToList();
        }

        public City GetCity(int cityId)
        {
            throw new NotImplementedException();
        }

        public PointOfInterest GetPointOfInterest(int cityId, int pointOfInterstId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PointOfInterest> GetPointOfInterests(int cityId)
        {
            throw new NotImplementedException();
        }
    }
}
