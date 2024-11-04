using System;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.Core.Service;
using HospitalLibrary.Core.Service.IService;
using HospitalLibrary.DTOs;
using Moq;
using Xunit;

namespace ExplorerTests
{
    public class PurchaseServiceTests
    {
        private readonly Mock<IPurchaseRepository> _mockPurchaseRepository;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly Mock<ITourRepository> _mockTourRepository;
        private readonly Mock<ICartRepository> _mockCartRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly PurchaseService _purchaseService;

        public PurchaseServiceTests()
        {
            _mockPurchaseRepository = new Mock<IPurchaseRepository>();
            _mockEmailService = new Mock<IEmailService>();
            _mockTourRepository = new Mock<ITourRepository>();
            _mockCartRepository = new Mock<ICartRepository>();
            _mockUserRepository = new Mock<IUserRepository>();

            _purchaseService = new PurchaseService(
                _mockPurchaseRepository.Object,
                _mockUserRepository.Object,
                _mockCartRepository.Object,
                _mockTourRepository.Object,
                _mockEmailService.Object
            );
        }

        [Fact]
        public void Buy_ShouldCreatePurchaseAndUpdateCart()
        {
            
            var tour = new Tour
            {
                Id = 1,
                Name = "Tour 1",
                Price = 100,
                TicketCount = 10,
                AuthorId = 1
            };

            var cartItem = new CartItem
            {
                Tour = tour,
                Count = 2,
                Deleted = false
            };

            var cartDto = new CartDTO
            {
                TouristId = 1,
                CartItems = new List<CartItem> { cartItem }
            };

            _mockTourRepository.Setup(repo => repo.GetById(tour.Id)).Returns(tour);
            _mockCartRepository.Setup(repo => repo.Update(It.IsAny<CartItem>())).Verifiable();
            _mockPurchaseRepository.Setup(repo => repo.Create(It.IsAny<Purchase>())).Verifiable();
            _mockUserRepository.Setup(repo => repo.GetById(cartDto.TouristId)).Returns(new User());

           
            _purchaseService.Buy(cartDto);

            
            _mockPurchaseRepository.Verify(repo => repo.Create(It.Is<Purchase>(p =>
                p.TourId == tour.Id &&
                p.Count == cartItem.Count &&
                p.Price == tour.Price * cartItem.Count &&
                p.TourName == tour.Name &&
                p.AuthorId == tour.AuthorId &&
                p.TouristId == cartDto.TouristId
            )), Times.Once);

            _mockTourRepository.Verify(repo => repo.Update(It.Is<Tour>(t =>
                t.Id == tour.Id &&
                t.TicketCount == 8 // 10 - 2
            )), Times.Once);

            _mockEmailService.Verify(service => service.PurchaseMail(It.IsAny<Purchase>(), It.IsAny<User>()), Times.Once);

            _mockCartRepository.Verify(repo => repo.Update(It.Is<CartItem>(c => c.Deleted)), Times.Once);
        }

        [Fact]
        public void GetAllByTouristId_ShouldReturnPurchases()
        {
            
            var purchases = new List<Purchase>
            {
                new Purchase { TourId = 1, Count = 1, Price = 100, TouristId = 1 },
                new Purchase { TourId = 2, Count = 2, Price = 200, TouristId = 1 }
            };

            _mockPurchaseRepository.Setup(repo => repo.GetAllByTouristId(1)).Returns(purchases);

            var result = _purchaseService.GetAllByTouristId(1);

            Assert.Equal(2, result.Count());
            Assert.Contains(result, p => p.TourId == 1);
            Assert.Contains(result, p => p.TourId == 2);
        }
    }
}