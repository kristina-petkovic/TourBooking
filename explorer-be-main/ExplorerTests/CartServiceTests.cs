
using System;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.Core.Service;
using HospitalLibrary.Core.Service.IService;
using HospitalLibrary.DTOs;
using Moq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace ExplorerTests
{
  public class CartServiceTests
    {
        private readonly Mock<ICartRepository> _repositoryMock;
        private readonly Mock<ITourService> _tourServiceMock;
        private readonly CartService _cartService;

        public CartServiceTests()
        {
            _repositoryMock = new Mock<ICartRepository>();
            _tourServiceMock = new Mock<ITourService>();
            _cartService = new CartService(_repositoryMock.Object, _tourServiceMock.Object);
        }

        [Fact]
        public void GetByTouristId_ShouldReturnCartDTO_WhenTouristIdIsValid()
        {
            int touristId = 1;
            var cartItems = new List<CartItem>
            {
                new CartItem { TourId = 1, Count = 2, Deleted = false }
            };
            var tour = new Tour { Id = 1, Price = 100 };
            _repositoryMock.Setup(r => r.GetByTouristId(touristId)).Returns(cartItems.AsQueryable());
            _tourServiceMock.Setup(t => t.Get(It.IsAny<int>())).Returns(tour);

            var result = _cartService.GetByTouristId(touristId);

            Assert.NotNull(result);
            Assert.Equal(touristId, result.TouristId);
            Assert.Equal(200.0, result.Price);
        }

        [Fact]
        public void AddToCart_ShouldCreateNewCartItem_WhenTourIdIsNotFound()
        {
            var dto = new CartDTO { TouristId = 1, TourId = 1 };
            var tour = new Tour { Id = 1, Price = 100 };
            _repositoryMock.Setup(r => r.GetByTourId(dto.TourId)).Returns((CartItem)null);
            _tourServiceMock.Setup(t => t.Get(dto.TourId)).Returns(tour);

            _cartService.AddToCart(dto);

            _repositoryMock.Verify(r => r.Create(It.IsAny<CartItem>()), Times.Once);
        }

        [Fact]
        public void AddToCart_ShouldIncreaseCartItemCount_WhenTourIdIsFound()
        {
            var dto = new CartDTO { TouristId = 1, TourId = 1 };
            var existingCartItem = new CartItem { TourId = 1, Count = 1 };
            var tour = new Tour { Id = 1, Price = 100 };

            _repositoryMock.Setup(r => r.GetByTouristId(dto.TouristId)).Returns(new List<CartItem> { existingCartItem }.AsQueryable());
            _tourServiceMock.Setup(t => t.Get(dto.TourId)).Returns(tour);

            _cartService.AddToCart(dto);

            Assert.Equal(2, existingCartItem.Count);
            _repositoryMock.Verify(r => r.Update(existingCartItem), Times.Once);
        }

        [Fact]
        public void DeleteById_ShouldDeleteCartItem_WhenIdIsValid()
        {
            var cartItem = new CartItem { Id = 1 };
            _repositoryMock.Setup(r => r.GetById(cartItem.Id)).Returns(cartItem);

            _cartService.DeleteById(cartItem.Id);

            _repositoryMock.Verify(r => r.Delete(cartItem), Times.Once);
        }

       
        [Fact]
        public void Decrease_ShouldDecreaseCount_WhenTourIdIsValid()
        {
            var cartItem = new CartItem { Id = 1, Count = 2 };
            _repositoryMock.Setup(r => r.GetById(cartItem.Id)).Returns(cartItem);

            _cartService.Decrease(cartItem.Id);

            Assert.Equal(1, cartItem.Count);
            _repositoryMock.Verify(r => r.Update(cartItem), Times.Once);
        }
    }
}