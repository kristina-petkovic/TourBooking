using System;
using System.Collections.Generic;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.Core.Service;
using Moq;
using Xunit;

namespace ExplorerTests
{
    public class ReportServiceTests{
        private readonly Mock<IReportRepository> _mockReportRepository;
        private readonly Mock<ITourRepository> _mockTourRepository;
        private readonly Mock<IPurchaseRepository> _mockPurchaseRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly ReportService _reportService;

        public ReportServiceTests()
        {
            _mockReportRepository = new Mock<IReportRepository>();
            _mockTourRepository = new Mock<ITourRepository>();
            _mockPurchaseRepository = new Mock<IPurchaseRepository>();
            _mockUserRepository = new Mock<IUserRepository>();

            _reportService = new ReportService(
                _mockReportRepository.Object,
                _mockTourRepository.Object,
                _mockPurchaseRepository.Object,
                _mockUserRepository.Object
            );
        }

        [Fact]
        public void CreateReport_ShouldGenerateReport()
        {
            
            var authorId = 1;
            var purchases = new List<Purchase>
            {
                new Purchase { TourId = 1, Count = 5, Price = 100, PurchaseDate = DateTime.Now.AddDays(-15), AuthorId = authorId },
                new Purchase { TourId = 2, Count = 3, Price = 200, PurchaseDate = DateTime.Now.AddDays(-15), AuthorId = authorId }
            };

            var tours = new List<Tour>
            {
                new Tour { Id = 1, Name = "Tour 1", Price = 100, AuthorId = authorId },
                new Tour { Id = 2, Name = "Tour 2", Price = 200, AuthorId = authorId }
            };

            _mockPurchaseRepository.Setup(repo => repo.GetAllByAuthorId(authorId)).Returns(purchases);
            _mockTourRepository.Setup(repo => repo.GetByAuthorId(authorId)).Returns(tours);
            _mockReportRepository.Setup(repo => repo.Create(It.IsAny<Report>())).Returns((Report r) => r);

            
            var report = _reportService.CreateReport(authorId);

            
            Assert.NotNull(report);
            Assert.Equal(2, report.SoldToursCount);
            Assert.Equal(1100, report.TotalProfit);
            Assert.Equal(1, report.TopSellingTourId);
            Assert.Equal(5, report.TopSellingTourCount);
            Assert.Equal(2, report.LeastSellingTourId);
            Assert.Equal(3, report.LeastSellingTourCount);
        }

        [Fact]
        public void TopAuthorThisMonth_ShouldUpdateAuthorPoints()
        {
            const int topAuthorId = 1;
            var author = new User { Id = topAuthorId, AuthorPoints = 4 };

            _mockReportRepository.Setup(repo => repo.GetAll()).Returns(new List<Report>
            {
                new() { AuthorId = topAuthorId, Date = DateTime.Now.AddDays(-10), SoldToursCount = 5 }
            });
            _mockUserRepository.Setup(repo => repo.GetById(topAuthorId)).Returns(author);
            _mockUserRepository.Setup(repo => repo.Update(It.IsAny<User>())).Verifiable();

            
            var updatedAuthor = _reportService.TopAuthorThisMonth();

           
            Assert.NotNull(updatedAuthor);
            Assert.Equal(5, updatedAuthor.AuthorPoints);
            Assert.True(updatedAuthor.TopAuthor);
            _mockUserRepository.Verify(repo => repo.Update(It.Is<User>(u => u.AuthorPoints == 5 && u.TopAuthor)), Times.Once);
        }

        [Fact]
        public void NoSalesInLastThreeMonths_ShouldUpdateTours()
        {
           
            var authorId = 1;
            var allPurchases = new List<Purchase>
            {
                new Purchase { TourId = 1, PurchaseDate = DateTime.Now.AddMonths(-2) },
                new Purchase { TourId = 2, PurchaseDate = DateTime.Now.AddMonths(-4) }
            };

            var allTours = new List<Tour>
            {
                new Tour { Id = 1, NoSalesInLastThreeMonths = false },
                new Tour { Id = 2, NoSalesInLastThreeMonths = false }
            };

            _mockPurchaseRepository.Setup(repo => repo.GetAllByAuthorId(authorId)).Returns(allPurchases);
            _mockTourRepository.Setup(repo => repo.GetByAuthorId(authorId)).Returns(allTours);
            _mockTourRepository.Setup(repo => repo.Update(It.IsAny<Tour>())).Verifiable();

            
            _reportService.NoSalesInLastThreeMonths(authorId);

            
            _mockTourRepository.Verify(repo => repo.Update(It.Is<Tour>(t => t.Id == 1 && !t.NoSalesInLastThreeMonths)), Times.Once);
            _mockTourRepository.Verify(repo => repo.Update(It.Is<Tour>(t => t.Id == 2 && t.NoSalesInLastThreeMonths)), Times.Once);
        }
    }
}