using System;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.Core.Service.IService;
using HospitalLibrary.DTOs;

namespace HospitalLibrary.Core.Service
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _repository;
        private readonly IEmailService _emailService;
        private readonly ITourRepository _tourrepository;
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;
        public PurchaseService(IPurchaseRepository repository, 
            IUserRepository userRepository,
            ICartRepository cartRepository, ITourRepository tourRepository, IEmailService emailService)
        {
            _repository = repository;
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _tourrepository = tourRepository;
            _emailService = emailService;
        }

        public void Buy(CartDTO dto)
        {
            foreach (var cartItem in dto.CartItems)
            {
                    var price = cartItem.Count * cartItem.Tour.Price;
                    var p = new Purchase()
                    {
                        TourId = cartItem.Tour.Id,
                        Count = cartItem.Count,
                        TourName = cartItem.Tour.Name,
                        AuthorId = cartItem.Tour.AuthorId,
                        TouristId = dto.TouristId,
                        Price = price,
                        PurchaseDate = DateTime.Now
                    };
                _repository.Create(p);
                var t = _tourrepository.GetById(p.TourId);
                t.TicketCount -= cartItem.Count;
                _tourrepository.Update(t);
                _emailService.PurchaseMail(p, _userRepository.GetById(cartItem.TouristId));
                
                
                cartItem.Deleted = true;
                _cartRepository.Update(cartItem);
            }
            
        }

        public IEnumerable<Purchase> GetAllByTouristId(int id)
        {
            return _repository.GetAllByTouristId(id);
        }
    }
}