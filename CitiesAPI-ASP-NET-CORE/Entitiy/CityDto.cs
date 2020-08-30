using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesAPI.ASP.NET.CORE.Entitiy
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPointOfInterest
        {
            get
            {
             return   PointOfInterest.
            }
        }
    }
}
