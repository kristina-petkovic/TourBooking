using HospitalLibrary.Core.Model.Enum;

namespace HospitalLibrary.Core.Model
{
    public class Interest : Entity
    {
        //u db contexu napuni primerima sa trello Model kartice
        public InterestType InterestTypeName { get; set; }
        public int TouristId { get; set; }
        public int TourId { get; set; }
    }
}