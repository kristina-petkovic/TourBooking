using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Core.Repository
{
    public class PurchaseRepository: IPurchaseRepository

    {
        private readonly HospitalDbContext _context;
        
        public PurchaseRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Purchase> GetAll()
        {
            return _context.Purchases.ToList();
        }
        public IEnumerable<Purchase> GetAllByAuthorId(int id)
        {
            return _context.Purchases.ToList().Where(x => x.AuthorId == id);
        }

        public IEnumerable<Purchase> GetAllByTourId(int id)
        {
           
            return _context.Purchases.ToList().Where(x => x.TourId == id);
        }

        public Purchase GetById(int id)
        {
            return _context.Purchases.Find(id);
        }

        public void Create(Purchase p)
        {
            _context.Purchases.Add(p);
            _context.SaveChanges();
        }

        public void Update(Purchase room)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Purchase room)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Purchase> GetAllByTouristId(int id)
        {
            return _context.Purchases.ToList().Where(x => x.TouristId == id);
        }
    }
}