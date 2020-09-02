using CitiesAPI.ASP.NET.CORE.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesAPI.ASP.NET.CORE.Services
{
    public interface ICityInfoRepository
    {
        IEnumerable<City> GetCities();
        City GetCity(int cityId, bool includePointOfInterest);
        IEnumerable<PointOfInterest> GetPointOfInterests(int cityId);
        PointOfInterest GetPointOfInterest(int cityId, int pointOfInterstId );
        bool CityExists(int cityid);
        void AddPointOfInterest(int cityId, PointOfInterest pointOfInterest);
        void UpdatePointOfInterest(int cityId, PointOfInterest pointOfInterest);
        bool Save();
    }
}
