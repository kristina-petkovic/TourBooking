using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalAPI.Controllers;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enum;
using HospitalLibrary.Core.Service.IService;
using HospitalLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ExplorerTests
{
      public class TourServiceE2ETests
    {
        private readonly Mock<ITourService> _mockTourService;
        private readonly Mock<IInterestService> _mockInterestService;
        private readonly Mock<IKeyPointService> _mockKeyPointService;
        private readonly TourController _controller;

        public TourServiceE2ETests()
        {
            _mockTourService = new Mock<ITourService>();
            _mockInterestService = new Mock<IInterestService>();
            _mockKeyPointService = new Mock<IKeyPointService>();  
            _controller = new TourController(_mockTourService.Object, _mockKeyPointService.Object, _mockInterestService.Object);
        }

        [Fact]
        public async Task CreateTour_ShouldReturnOk_WhenValidData()
        {
            var tourDto = new TourDTO
            {
                Name = "Test Tour",
                TicketCount = 100,
                Description = "Test Description",
                Difficulty = TourDifficulty.Medium,
                AuthorId = 1,
                Price = 50,
                Interests = new List<string> { "food", "nature" }
            };
            var newTour = new Tour
            {
                Id = 1,
                Name = "Test Tour",
                TicketCount = 100,
                Description = "Test Description",
                Difficulty = TourDifficulty.Easy,
                AuthorId = 1,
                Price = 50,
                Status = TourStatus.Draft,
                Deleted = false
            };

            _mockTourService.Setup(s => s.Create(It.IsAny<TourDTO>())).Returns(newTour);
            _mockInterestService.Setup(s => s.CreateMultipleForTour(It.IsAny<int>(), It.IsAny<List<string>>()));

            var result = await _controller.Create(tourDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Tour>(okResult.Value);
            Assert.Equal("Test Tour", returnValue.Name);
            Assert.Equal(100, returnValue.TicketCount);
        }

        [Fact]
        public async Task PublishTour_ShouldReturnOk_WhenTourExists()
        {
            const int tourId = 1;
            _mockKeyPointService.Setup(s => s.GetByTourId(tourId)).Returns(new List<KeyPoint> { new KeyPoint(), new KeyPoint() });  
            _mockTourService.Setup(s => s.Publish(tourId)).Verifiable();

            var result = await _controller.Publish(tourId);

            var okResult = Assert.IsType<OkResult>(result);
            _mockTourService.Verify(s => s.Publish(tourId), Times.Once);
        }

        [Fact]
        public async Task ArchiveTour_ShouldReturnOk_WhenTourExists()
        {
            _mockTourService.Setup(s => s.Archive(1)).Verifiable();

            var result = await _controller.Archive(1);

            var okResult = Assert.IsType<OkResult>(result);
            _mockTourService.Verify(s => s.Archive(1), Times.Once);
        }

        [Fact]
        public void GetByAuthorId_ShouldReturnListOfTours_WhenToursExist()
        {
            int authorId = 1;
            var tours = new List<TourDTO>
            {
                new TourDTO { Id = 1, Name = "Tour 1", AuthorId = authorId },
                new TourDTO { Id = 2, Name = "Tour 2", AuthorId = authorId }
            };

            _mockTourService.Setup(s => s.GetByAuthorId(authorId)).Returns(tours);

            var result = _controller.GetByAuthorId(authorId);

            Assert.Equal(2, result.Count);
            Assert.Equal("Tour 1", result[0].Name);
        }

        [Fact]
        public void GetAllPublished_ShouldReturnListOfPublishedTours()
        {
            var tours = new List<TourDTO>
            {
                new TourDTO { Id = 1, Name = "Published Tour 1", Status = TourStatus.Published},
                new TourDTO { Id = 2, Name = "Published Tour 2", Status = TourStatus.Published }
            };

            _mockTourService.Setup(s => s.GetAll()).Returns(tours);

            var result = _controller.GetAllPublished();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<TourDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public void RecommendedTours_ShouldReturnListOfRecommendedTours_WhenTouristExists()
        {
            int touristId = 1;
            var recommendedTours = new List<TourDTO>
            {
                new TourDTO { Id = 1 },
                new TourDTO { Id = 2 }
            };

            _mockTourService.Setup(s => s.FindTours(touristId)).Returns(recommendedTours);

            var result = _controller.RecommendedTours(touristId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<TourDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }
 }
}
