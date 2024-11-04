using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.DTOs;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Core.Repository
{
    public class TourRepository : ITourRepository
    {
        private readonly HospitalDbContext _context;

        public TourRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Tour> GetAll()
        {
            return _context.Tours.ToList();
        }

        public Tour GetById(int id)
        {
            return _context.Tours.Find(id);
        }

        public Tour Create(Tour room)
        {
            _context.Tours.Add(room);
            _context.SaveChanges();
            return room;
        }

        public void Update(Tour t)
        {
            _context.Entry(t).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Tour room)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Tour> GetByAuthorId(int authorId)
        {
            return _context.Tours.Where(x => x.AuthorId == authorId).ToList();
        }
    }
}