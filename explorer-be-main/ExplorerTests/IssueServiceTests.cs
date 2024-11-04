using System;
using System.Collections.Generic;
using System.Linq;
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
    public class IssueServiceTests
    {
        private readonly IssueService _issueService;
        private readonly Mock<IIssueRepository> _issueRepositoryMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public IssueServiceTests()
        {
            _issueRepositoryMock = new Mock<IIssueRepository>();
            _emailServiceMock = new Mock<IEmailService>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _issueService = new IssueService(_issueRepositoryMock.Object, _emailServiceMock.Object, _userRepositoryMock.Object);
        }

        [Fact]
        public void Create_ShouldCreateAndReturnIssue_WhenCalledWithValidDto()
        {
            
            var issueDto = new IssueDTO
            {
                AuthorId = 1,
                TouristId = 2,
                TourId = 3,
                Text = "Issue description"
            };

            var createdIssue = new Issue
            {
                Id = 1,
                AuthorId = issueDto.AuthorId,
                TouristId = issueDto.TouristId,
                TourId = issueDto.TourId,
                Text = issueDto.Text,
                Status = IssueStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            _issueRepositoryMock.Setup(r => r.Create(It.IsAny<Issue>())).Returns(createdIssue);

            var result = _issueService.Create(issueDto);

            Assert.Equal(createdIssue, result);
         //   _emailServiceMock.Verify(e => e.SendIssueEMail(It.IsAny<Issue>()), Times.Once);
            _issueRepositoryMock.Verify(r => r.Create(It.Is<Issue>(i =>
                i.AuthorId == issueDto.AuthorId && i.TouristId == issueDto.TouristId &&
                i.TourId == issueDto.TourId && i.Status == IssueStatus.Pending &&
                i.StatusChanges.Count == 1 && i.StatusChanges.First().Status == IssueStatus.Pending &&
                i.StatusChanges.First().ChangedBy == issueDto.TouristId)), Times.Once);
        }

        [Fact]
        public void GetAllByAuthorId_ShouldReturnIssues_WhenCalledWithValidAuthorId()
        {
            
            var authorId = 1;
            var issues = new List<Issue>
            {
                new Issue { Id = 1, AuthorId = authorId, TourId = 1, Text = "Issue 1" },
                new Issue { Id = 2, AuthorId = authorId, TourId = 2, Text = "Issue 2" }
            };

            _issueRepositoryMock.Setup(r => r.GetAll()).Returns(issues.AsQueryable());

            
            var result = _issueService.GetAllByAuthorId(authorId);

            
            Assert.Equal(issues.Count, result.Count());
            Assert.All(result, issue => Assert.Equal(authorId, issue.AuthorId));
        }

        [Fact]
        public void Resolve_ShouldUpdateIssueStatusToResolved_WhenCalledWithValidIssueIdAndUserId()
        {
            
            var issueId = 1;
            var loggedUserId = 2;
            var issue = new Issue { Id = issueId, Status = IssueStatus.Pending };

            _issueRepositoryMock.Setup(r => r.GetById(issueId)).Returns(issue);

            var result = _issueService.Resolve(issueId, loggedUserId);

            Assert.Equal(IssueStatus.Resolved, issue.Status);
            Assert.NotNull(issue.UpdatedAt);
            Assert.Contains(issue.StatusChanges, sc => sc.Status == IssueStatus.Resolved && sc.ChangedBy == loggedUserId);
            _issueRepositoryMock.Verify(r => r.Update(issue), Times.Once);
        }

    
        [Fact]
        public void Decline_ShouldUpdateIssueStatusToDeclinedAndMarkTouristAsMalicious_WhenCalledWithValidIssueId()
        {
            
            var issueId = 1;
            var loggedUserId = 2;
            var issue = new Issue { Id = issueId, Status = IssueStatus.Pending, TouristId = 3 };

            var tourist = new User { Id = issue.TouristId, IssueCount = 10 };

            _issueRepositoryMock.Setup(r => r.GetById(issueId)).Returns(issue);
            _userRepositoryMock.Setup(r => r.GetById(issue.TouristId)).Returns(tourist);

            
            var result = _issueService.Decline(issueId, loggedUserId);

            
            Assert.Equal(IssueStatus.Declined, issue.Status);
            Assert.Contains(issue.StatusChanges, sc => sc.Status == IssueStatus.Declined && sc.ChangedBy == loggedUserId);
            _userRepositoryMock.Verify(r => r.Update(It.Is<User>(u => u.Id == issue.TouristId && u.Malicious == true)), Times.Once);
            _issueRepositoryMock.Verify(r => r.Update(issue), Times.Once);
        }

       
    }
}