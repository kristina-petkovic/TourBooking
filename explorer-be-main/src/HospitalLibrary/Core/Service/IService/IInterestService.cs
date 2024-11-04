using System.Collections.Generic;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enum;

namespace HospitalLibrary.Core.Service.IService
{
    public interface IInterestService
    {
        void CreateMultiple(int userId, List<Interest> dtoInterests);
        
        void CreateMultipleWithInterestString(int userId, IEnumerable<string> dtoInterests);
        void CreateMultipleForTour(int tourId, IEnumerable<string> dtoInterests);

        object Create(Interest interest);
        void Delete(int id);
        object GetAllByTouristId(int id);
        public List<InterestType> GetInterestNamesByTouristId(int id);

        object GetAllByTourId(int id);
        IEnumerable<int> GetToursIdsByInterestTypes(List<InterestType> interestTypes);
        object GetTouristsIdsByInterestTypes(List<string> dtoInterests, string name);
    }
}