namespace CitiesAPI.ASP.NET.CORE.Entitiy
{
    public class PointOfInterest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public City  City { set; get; }
        public int CityId { get; set; }
    }
}