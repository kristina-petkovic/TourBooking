using System.Collections.Generic;
using HospitalLibrary.DTOs;

namespace HospitalLibrary.Core.Service.IService
{
    public interface ICartService
    {
        CartDTO GetByTouristId(int id);
        void AddToCart(CartDTO dto);
        void DeleteById(int id);
        void DeleteByTourId(int id);
        void Decrease(int id);
    }
}