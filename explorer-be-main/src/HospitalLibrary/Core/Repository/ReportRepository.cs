using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Core.Repository
{
    public class ReportRepository :IReportRepository
    {
        private readonly HospitalDbContext _context;

        public ReportRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Report> GetAll()
        {
            return _context.Reports.ToList(); 
        }

        public IEnumerable<Report> GetAllByAuthorId(int id)
        {
            return _context.Reports.Where(x=> x.AuthorId == id).ToList();
        }

        public Report GetById(int id)
        {
            return _context.Reports.Find(id);
        }
  

        public Report Create(Report r)
        {
            _context.Reports.Add(r);
            _context.SaveChanges(); 
            return r;
        }

        public void Update(Report r)
        {
            _context.Entry(r).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Report room)
        {
            var r = GetById(room.Id);
            r.Deleted = true;
            Update(r);
        }
    }
}