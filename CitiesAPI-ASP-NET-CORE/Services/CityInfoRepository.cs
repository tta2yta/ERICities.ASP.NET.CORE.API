using CitiesAPI.ASP.NET.CORE.Context;
using CitiesAPI.ASP.NET.CORE.Entitiy;
using Microsoft.EntityFrameworkCore;
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

       

        public bool CityExists(int cityid)
        {
          return  _context.Cities.Any(c => c.Id == cityid);
        }

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.OrderBy(c=> c.Name).ToList();
        }

        public City GetCity(int cityId, bool includePointOfInterest)
        {
            if (includePointOfInterest)
            {
                return _context.Cities.Include(c => c.pointOfInterests)
                    .Where(p => p.Id == cityId).FirstOrDefault();
            }
            return _context.Cities.Where(c => c.Id == cityId).FirstOrDefault();
        }

        public PointOfInterest GetPointOfInterest(int cityId, int pointOfInterstId)
        {
            return _context.PointOfInterests.Where(c => c.CityId == cityId &&
            c.Id == pointOfInterstId).FirstOrDefault();
        }

        public IEnumerable<PointOfInterest> GetPointOfInterests(int cityId)
        {
            return _context.PointOfInterests.Where(c => c.CityId == cityId).ToList();
        }

        public void AddPointOfInterest(int cityId, PointOfInterest pointOfInterest)
        {
            var city = GetCity(cityId, false);
            city.pointOfInterests.Add(pointOfInterest);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
