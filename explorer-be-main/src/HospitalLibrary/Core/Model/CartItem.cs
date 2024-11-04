using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalLibrary.Core.Model
{
    public class CartItem : Entity
    {
        public int TouristId { get; set; }
        public int TourId { get; set; }
        [ForeignKey(("TourId"))]
        public Tour Tour { get; set; }
        public int Count { get; set; }
    }
}