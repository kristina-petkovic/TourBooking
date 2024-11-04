using System.Collections.Generic;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.DTOs
{
    public class CartDTO
    {
        public List<CartItem> CartItems { get; set; }
        public double Price { get; set; }
        
        
        public int TourId { get; set; }
        public int TouristId { get; set; }
    }
}