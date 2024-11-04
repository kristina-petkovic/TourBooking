using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Core.Repository
{
    public class KeyPointRepository : IKeyPointRepository
    {
        private readonly HospitalDbContext _context;

        public KeyPointRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public IEnumerable<KeyPoint> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public KeyPoint GetById(int id)
        {
            return _context.KeyPoints.Find(id);
        }

        public IEnumerable<KeyPoint> GetByTourId(int tourId)
        {
            return _context.KeyPoints.Where(x => x.TourId == tourId).ToList();
        }

        public object Create(KeyPoint room)
        {
            _context.KeyPoints.Add(room);
            _context.SaveChanges();
            return room;
        }

        public void Update(KeyPoint room)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(KeyPoint room)
        {
            throw new System.NotImplementedException();
        }
    }
}