using System;
using HospitalLibrary.Core.Model.Enum;

namespace HospitalLibrary.Core.Model
{
    public class IssueStatusChange
    {
        public int Id { get; set; }
        public int IssueId { get; set; }
        public IssueStatus Status { get; set; }
        public DateTime ChangedAt { get; set; }
        public int ChangedBy { get; set; } // ID of the user who changed the status
    }
}