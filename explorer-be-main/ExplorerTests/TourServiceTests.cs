using System;
using System.Collections.Generic;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enum;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.Core.Service;
using HospitalLibrary.Core.Service.IService;
using HospitalLibrary.DTOs;
using Moq;
using Xunit;

namespace ExplorerTests
{
  public class TourServiceTests
{
    private readonly Mock<ITourRepository> _repositoryMock;
    private readonly Mock<IKeyPointRepository> _kprepositoryMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<IReportService> _reportServiceMock;
    private readonly Mock<IInterestService> _interestServiceMock;
    private readonly TourService _tourService;

    public TourServiceTests()
    {
        _repositoryMock = new Mock<ITourRepository>();
        _kprepositoryMock = new Mock<IKeyPointRepository>();
        _userServiceMock = new Mock<IUserService>();
        _reportServiceMock = new Mock<IReportService>();
        _interestServiceMock = new Mock<IInterestService>();

        _tourService = new TourService(
            _repositoryMock.Object,
            _kprepositoryMock.Object,
            _reportServiceMock.Object,
            _userServiceMock.Object,
            _interestServiceMock.Object
        );
    }
    [Fact]
    public void Publish_Should_Set_Status_To_Published_When_Enough_KeyPoints()
    {
        var tourId = 1;
        var tour = new Tour { Id = tourId, Status = TourStatus.Draft };
        _repositoryMock.Setup(r => r.GetById(tourId)).Returns(tour);
        _kprepositoryMock.Setup(kp => kp.GetByTourId(tourId)).Returns(new List<KeyPoint> { new KeyPoint(), new KeyPoint() });

        _tourService.Publish(tourId);

        Assert.Equal(TourStatus.Published, tour.Status);
        _repositoryMock.Verify(r => r.Update(tour), Times.Once);
    }

    [Fact]
    public void Publish_Should_Not_Set_Status_To_Published_When_Not_Enough_KeyPoints()
    {
        var tourId = 1;
        var tour = new Tour { Id = tourId, Status = TourStatus.Draft };
        _repositoryMock.Setup(r => r.GetById(tourId)).Returns(tour);
        _kprepositoryMock.Setup(kp => kp.GetByTourId(tourId)).Returns(new List<KeyPoint> { new KeyPoint() });

        _tourService.Publish(tourId);
        Assert.Equal(TourStatus.Draft, tour.Status);
        _repositoryMock.Verify(r => r.Update(It.IsAny<Tour>()), Times.Never);
    }

    [Fact]
    public void GetById_Should_Return_Null_If_Tour_Is_Deleted()
    {
        var tourId = 1;
        var tour = new Tour { Id = tourId, Deleted = true };
        _repositoryMock.Setup(r => r.GetById(tourId)).Returns(tour);
        
        var result = _tourService.GetById(tourId);
        
        Assert.Null(result);
    }

    [Fact]
    public void GetById_Should_Return_TourDTO_If_Tour_Is_Not_Deleted()
    {
       
        var tourId = 1;
        var tour = new Tour { Id = tourId, Deleted = false };
        _repositoryMock.Setup(r => r.GetById(tourId)).Returns(tour);
        _kprepositoryMock.Setup(kp => kp.GetByTourId(tourId)).Returns(new List<KeyPoint>());

        
        var result = _tourService.GetById(tourId);

        
        Assert.NotNull(result);
        Assert.Equal(tour.Id, result.Id);
    }

    [Fact]
    public void Archive_Should_Set_Status_To_Archived()
    {
        
        var tourId = 1;
        var tour = new Tour { Id = tourId, Status = TourStatus.Published };
        _repositoryMock.Setup(r => r.GetById(tourId)).Returns(tour);

        _tourService.Archive(tourId);

       
        Assert.Equal(TourStatus.Archived, tour.Status);
        _repositoryMock.Verify(r => r.Update(tour), Times.Once);
    }

   
}

}