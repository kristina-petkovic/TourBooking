using System.Collections.Generic;
using HospitalLibrary.Core.Model;
using HospitalLibrary.DTOs;

namespace HospitalLibrary.Core.Service.IService
{
    public interface IPurchaseService
    {
        void Buy(CartDTO dto);
        IEnumerable<Purchase> GetAllByTouristId(int id);
    }
}