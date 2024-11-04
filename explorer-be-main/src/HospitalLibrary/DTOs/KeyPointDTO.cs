namespace HospitalLibrary.DTOs
{
    public class KeyPointDTO
    {
        
        public int TourId { get; set; }

        public int Order { get; set; }
        

        public string Name { get; set; }

        public string Filename { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}