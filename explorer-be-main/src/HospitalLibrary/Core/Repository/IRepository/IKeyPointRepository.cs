using System.Collections.Generic;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Repository.IRepository
{
    public interface IKeyPointRepository
    {
        IEnumerable<KeyPoint> GetAll();
        KeyPoint GetById(int id);
        IEnumerable<KeyPoint> GetByTourId(int tourId);
        object Create(KeyPoint room);
        void Update(KeyPoint room);
        void Delete(KeyPoint room);
    }
}