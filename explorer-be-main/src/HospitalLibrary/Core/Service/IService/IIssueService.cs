using System.Collections.Generic;
using HospitalLibrary.Core.Model;
using HospitalLibrary.DTOs;

namespace HospitalLibrary.Core.Service.IService
{
    public interface IIssueService
    {
        Issue Create(IssueDTO dto);
        IEnumerable<Issue> GetAllByAuthorId(int id);
       
        object Resolve(int issueId,int d);
        object Revision(int issue, int i);
        object Decline(int id, int u);
        object? GetAllRevision(int id);
        object? PendingAgain(int issue, int id);
        object? GetAllByTouristId(int id);
    }
}