using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enum;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.Core.Service;

namespace ExplorerTests
{
    public class InterestServiceTests
    {
        private readonly Mock<IInterestRepository> _repositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly InterestService _interestService;

        public InterestServiceTests()
        {
            _repositoryMock = new Mock<IInterestRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _interestService = new InterestService(_repositoryMock.Object, _userRepositoryMock.Object);
        }

        [Fact]
        public void CreateMultiple_ShouldCallRepositoryWithCorrectParameters()
        {
            const int userId = 1;
            var interests = new List<Interest>
            {
                new Interest { InterestTypeName = InterestType.art, TouristId = userId },
                new Interest { InterestTypeName = InterestType.food, TouristId = userId }
            };

            _interestService.CreateMultiple(userId, interests);

            _repositoryMock.Verify(r => r.CreateMultiple(userId, interests), Times.Once);
        }

        [Fact]
        public void CreateMultipleWithInterestString_ShouldCallRepositoryWithCorrectParameters()
        {
            const int userId = 1;
            var interests = new List<string> { "art", "food" };

            _interestService.CreateMultipleWithInterestString(userId, interests);

            _repositoryMock.Verify(r => r.CreateMultipleWithInterestString(userId, interests), Times.Once);
        }

        [Fact]
        public void CreateMultipleForTour_ShouldCallRepositoryWithCorrectParameters()
        {
            int tourId = 1;
            var interests = new List<string> { "art", "food" };

            _interestService.CreateMultipleForTour(tourId, interests);

            _repositoryMock.Verify(r => r.CreateMultipleForTour(tourId, interests), Times.Once);
        }

        [Fact]
        public void Create_ShouldReturnCreatedInterest()
        {
            var interest = new Interest
            {
                InterestTypeName = InterestType.art,
                TouristId = 1
            };
            var createdInterest = new Interest { Id = 1, InterestTypeName = InterestType.art, TouristId = 1 };
            _repositoryMock.Setup(r => r.Create(It.IsAny<Interest>())).Returns(createdInterest);
            _repositoryMock.Setup(r => r.GetAllByTouristId(interest.TouristId)).Returns(new List<Interest>());

            var result = _interestService.Create(interest);

            Assert.NotNull(result);
            Assert.Equal(createdInterest, result);
            _repositoryMock.Verify(r => r.Create(It.IsAny<Interest>()), Times.Once);
        }

        [Fact]
        public void Delete_ShouldMarkInterestAsDeleted()
        {
            int interestId = 1;
            var interest = new Interest { Id = interestId, Deleted = false };
            _repositoryMock.Setup(r => r.GetById(interestId)).Returns(interest);

            _interestService.Delete(interestId);

            Assert.True(interest.Deleted);
            _repositoryMock.Verify(r => r.Update(interest), Times.Once);
        }

        [Fact]
        public void GetAllByTouristId_ShouldReturnInterestsForTourist()
        {
            int touristId = 1;
            var interests = new List<Interest>
            {
                new Interest { TouristId = touristId, InterestTypeName = InterestType.art },
                new Interest { TouristId = touristId, InterestTypeName = InterestType.food }
            };
            _repositoryMock.Setup(r => r.GetAllByTouristId(touristId)).Returns(interests);

            var result = _interestService.GetAllByTouristId(touristId);

            Assert.NotNull(result);
            Assert.Equal(interests, result);
        }

        [Fact]
        public void GetAll_ShouldReturnAllInterests()
        {
            var interests = new List<Interest>
            {
                new Interest { InterestTypeName = InterestType.art },
                new Interest { InterestTypeName = InterestType.food }
            };
            _repositoryMock.Setup(r => r.GetAll()).Returns(interests);

            var result = _interestService.GetAll();

            Assert.NotNull(result);
            Assert.Equal(interests, result);
        }

        [Fact]
        public void GetInterestNamesByTouristId_ShouldReturnDistinctInterestNames()
        {
            int touristId = 1;
            var interests = new List<Interest>
            {
                new Interest { TouristId = touristId, InterestTypeName = InterestType.art },
                new Interest { TouristId = touristId, InterestTypeName = InterestType.food },
                new Interest { TouristId = touristId, InterestTypeName = InterestType.art } // Duplicate interest type
            };
            _repositoryMock.Setup(r => r.GetAllByTouristId(touristId)).Returns(interests);

            var result = _interestService.GetInterestNamesByTouristId(touristId);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(InterestType.art, result);
            Assert.Contains(InterestType.food, result);
        }

        [Fact]
        public void GetAllByTourId_ShouldReturnInterestsForTour()
        {
            int tourId = 1;
            var interests = new List<Interest>
            {
                new Interest { TourId = tourId, InterestTypeName = InterestType.art },
                new Interest { TourId = tourId, InterestTypeName = InterestType.food }
            };
            _repositoryMock.Setup(r => r.GetAllByTourId(tourId)).Returns(interests);

            var result = _interestService.GetAllByTourId(tourId);

            Assert.NotNull(result);
            Assert.Equal(interests, result);
        }

        [Fact]
        public void GetToursIdsByInterestTypes_ShouldReturnCorrectTourIds()
        {
            var interestTypes = new List<InterestType> { InterestType.art, InterestType.food };
            var interests = new List<Interest>
            {
                new Interest { TourId = 1, InterestTypeName = InterestType.art },
                new Interest { TourId = 2, InterestTypeName = InterestType.food },
                new Interest { TourId = 3, InterestTypeName = InterestType.nature } 
            };
            _repositoryMock.Setup(r => r.GetAll()).Returns(interests);

            var result = _interestService.GetToursIdsByInterestTypes(interestTypes);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(1, result);
            Assert.Contains(2, result);
        }

       
    }
}
