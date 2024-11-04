using System.Collections.Generic;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Repository.IRepository
{
    public interface IInterestRepository
    {
        IEnumerable<Interest> GetAll();
        Interest GetById(int id);
        object Create(Interest room);
        void Update(Interest room);
        void Delete(Interest room);
        void CreateMultiple(int userId, List<Interest> dtoInterests);
        public void CreateMultipleWithInterestString(int userId, IEnumerable<string> dtoInterests);
        //CreateMultipleForTour
        public void CreateMultipleForTour(int tourId, IEnumerable<string> dtoInterests);

        IEnumerable<Interest> GetAllByTouristId(int id);
        IEnumerable<Interest> GetAllByTourId(int id);
    }
}