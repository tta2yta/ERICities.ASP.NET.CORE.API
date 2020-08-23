using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesAPI.ASP.NET.CORE.Models
{
    public class PointOfInterestCreationDto
    {
        [Required(ErrorMessage ="You should provide name value")]
        [MaxLength(50)]
        public string name { get; set; }

        [MaxLength(200)]
        public string description { get; set; }
    }
}
