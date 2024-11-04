using HospitalLibrary.Core.Model.Enum;

namespace HospitalLibrary.DTOs
{
    public class IssueDTO
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int TourId { get; set; }
        public int TouristId { get; set; }
        public int AdminId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public IssueStatus Status { get; set; }
    }
}