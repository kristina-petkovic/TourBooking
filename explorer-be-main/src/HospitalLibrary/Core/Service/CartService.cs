using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.Core.Service.IService;
using HospitalLibrary.DTOs;

namespace HospitalLibrary.Core.Service
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repository;
        private readonly ITourService _tService;

        
        public CartService(ICartRepository repository, ITourService tr)
        {
            _repository = repository;
            _tService = tr;
        }

        public CartDTO GetByTouristId(int id)
        {
            var cartItems = _repository.GetByTouristId(id).Where(x=>x.Deleted == false).ToList();
            return MapList(id, cartItems);
        }

        private CartDTO MapList(int touristId, List<CartItem> items)
        {
            foreach (var item in items)
            {
                item.Tour =  _tService.Get(item.TourId);
                Update(item);
            }
            var price = 0.0;
            foreach (var cartItem in items)
            {
                var itemCount = cartItem.Count;
                price += cartItem.Tour.Price * itemCount;
            }

            var cart = new CartDTO()
            {
                TouristId = touristId,
                CartItems = items,
                Price = price
            };
            
            return cart;
        }

        
        public void AddToCart(CartDTO dto)
        {
            var existingCartItems = _repository.GetByTouristId(dto.TouristId);
            var existingCartItem = existingCartItems.FirstOrDefault(item => item.TourId == dto.TourId);

            if (existingCartItem != null)
            {
                existingCartItem.Tour = _tService.Get(dto.TourId);
                existingCartItem.Count++;
                Update(existingCartItem);
            }
            else
            {
                _repository.Create(new CartItem
                {
                    TourId = dto.TourId,
                    Tour = _tService.Get(dto.TourId),
                    Deleted = false,
                    TouristId = dto.TouristId,
                    Count = 1
                });
            }
                
            
        }

        public void DeleteById(int id)
        {
            var c = _repository.GetById(id);
            _repository.Delete(c);
        }
        public void DeleteByTourId(int id)
        {
            var cartItem = _repository.GetByTourId(id);
            cartItem.Deleted = true;
            _repository.Update(cartItem);
        }

        public void Decrease(int id)
        {
            var c = _repository.GetById(id);
            c.Count--;
            Update(c);
        }

        public void Update(CartItem i)
        {
            _repository.Update(i);
        }

      
    }
}