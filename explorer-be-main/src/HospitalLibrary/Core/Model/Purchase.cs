using System;

namespace HospitalLibrary.Core.Model
{
    public class Purchase : Entity
    {
     
        public int AuthorId { get; set; }
        public int TourId { get; set; }
        public string TourName { get; set; }
        public int TouristId { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}