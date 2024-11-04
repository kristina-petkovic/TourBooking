using System;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Core.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly HospitalDbContext _context;

        public CartRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CartItem> GetAll()
        {
            return _context.CartItems.ToList();
        }

        public CartItem GetById(int id)
        {
            return _context.CartItems.Find(id);
        }

        public void Create(CartItem room)
        {
            _context.CartItems.Add(room);
            _context.SaveChanges();
        }

        public void Update(CartItem room)
        {
            _context.Entry(room).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(CartItem room)
        {
            var cartItem = _context.CartItems.FirstOrDefault(x => x.Id == room.Id);

            if (cartItem != null)
            {
                cartItem.Deleted = true;
                Update(cartItem);
            }
            else
            {
                throw new ArgumentException($"No cart item found with TourId: {room.Id}");
            }
        }

        public IEnumerable<CartItem> GetByTouristId(int id)
        {
            return _context.CartItems.Where(c => c.TouristId == id && c.Deleted == false) ;
        }

        public void DeleteByTourId(int id)
        {
            var cartItem = _context.CartItems.FirstOrDefault(x => x.TourId == id);

            if (cartItem != null)
            {
                cartItem.Deleted = true;
                Update(cartItem);
            }
            else
            {
                throw new ArgumentException($"No cart item found with TourId: {id}");
            }

        }

        public CartItem GetByTourId(int cartItemTourId)
        {
            return _context.CartItems.FirstOrDefault(c => c.TourId == cartItemTourId && c.Deleted == false);

        }
    }
}