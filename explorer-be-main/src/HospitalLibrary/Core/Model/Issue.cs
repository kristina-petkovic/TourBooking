using System;
using System.Collections.Generic;
using HospitalLibrary.Core.Model.Enum;

namespace HospitalLibrary.Core.Model
{
    public class Issue : Entity
    {
        public int AuthorId { get; set; }
        public int TourId { get; set; }
        public int TouristId { get; set; }
        
        public string Name { get; set; }
        public string Text { get; set; }
        public IssueStatus Status { get; set; }
        public List<IssueStatusChange> StatusChanges { get; set; } // Track status changes
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}