using System.Collections.Generic;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Repository.IRepository
{
    public interface ICartRepository
    {
        IEnumerable<CartItem> GetAll();
        CartItem GetById(int id);
        void Create(CartItem room);
        void Update(CartItem room);
        void Delete(CartItem room);
        IEnumerable<CartItem> GetByTouristId(int id);
        void DeleteByTourId(int id);
        CartItem GetByTourId(int cartItemTourId);
    }
}