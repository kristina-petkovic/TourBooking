using System.Collections.Generic;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Repository.IRepository
{
    public interface IIssueRepository
    {
        IEnumerable<Issue> GetAll();
        Issue GetById(int id);
        Issue Create(Issue room);
        void Update(Issue room);
        void Delete(Issue room);
    }
}