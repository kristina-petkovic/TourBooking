using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Core.Repository
{
    public class IssueRepository : IIssueRepository
    {
        private readonly HospitalDbContext _context;

        public IssueRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Issue> GetAll()
        {
            return _context.Issues.ToList();
        }

        public Issue GetById(int id)
        {
            return _context.Issues.Find(id);
        }

        public Issue Create(Issue i)
        {
            _context.Issues.Add(i);
            _context.SaveChanges();
            return i;
        }

        public void Update(Issue t)
        {
            _context.Entry(t).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Issue room)
        {
            throw new System.NotImplementedException();
        }
    }
}