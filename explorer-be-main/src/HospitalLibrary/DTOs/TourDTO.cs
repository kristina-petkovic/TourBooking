using System.Collections.Generic;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HospitalLibrary.DTOs
{
    public class TourDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TourDifficulty Difficulty { get; set; }
        
        public string Difficult { get; set; }
        public List<string> Interests { get; set; }
        public int Price { get; set; }
        public int AuthorId { get; set; }
        public List<KeyPoint> KeyPoints { get; set; }
        
        public bool NoSalesInLastThreeMonths { get; set; }
        public TourStatus Status  { get; set; }  
        public int TicketCount { get; set; }
        public int Id { get; set; }
    }
}