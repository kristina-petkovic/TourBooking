using System.Collections.Generic;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Repository.IRepository
{
    public interface IPurchaseRepository
    {
        IEnumerable<Purchase> GetAll();
        public IEnumerable<Purchase> GetAllByAuthorId(int id);
        public IEnumerable<Purchase> GetAllByTourId(int id);
        Purchase GetById(int id);
        void Create(Purchase room);
        void Update(Purchase room);
        void Delete(Purchase room);
        IEnumerable<Purchase> GetAllByTouristId(int id);
    }
}