using System;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enum;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.Core.Service.IService;
using HospitalLibrary.DTOs;
using MailKit;

namespace HospitalLibrary.Core.Service
{
    public class IssueService : IIssueService
    {
        private readonly IIssueRepository _repository;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;

        public IssueService(IIssueRepository repository, IEmailService emailService, IUserRepository userRepository)
        {
            _emailService = emailService;
            _userRepository = userRepository;
            _repository = repository;
        }

        public Issue Create(IssueDTO dto)
        {
            var i = new Issue { Deleted = false, TourId = dto.TourId,
                TouristId = dto.TouristId, Text = dto.Text, 
                Name = dto.Name,
                Status = IssueStatus.Pending,
                AuthorId = dto.AuthorId,
                CreatedAt = DateTime.UtcNow,
                StatusChanges = new List<IssueStatusChange>
                {
                    new()
                    {
                        Status = IssueStatus.Pending,
                        ChangedAt = DateTime.UtcNow,
                        ChangedBy = dto.TouristId
                    }
                }
            };
            var newIssue = _repository.Create(i);
            _emailService.SendIssueEMail(newIssue, _userRepository.GetById(dto.AuthorId));

            return newIssue;
            
        }

        public IEnumerable<Issue> GetAllByAuthorId(int id)
        {
            return _repository.GetAll().Where(i => i.AuthorId == id);
        }

        public object Resolve(int issueId, int loggedUserId)
        {
            var i = _repository.GetById(issueId);
            i.Status = IssueStatus.Resolved;
            i.UpdatedAt = DateTime.UtcNow;
            i.StatusChanges ??= new List<IssueStatusChange>();

            i.StatusChanges.Add(new IssueStatusChange
            {
                IssueId = issueId,
                Status = IssueStatus.Resolved,
                ChangedAt = DateTime.UtcNow,
                ChangedBy = loggedUserId
            });
            _repository.Update(i);
            return i;
        }

        public object Revision(int issue, int loggedUserId)
        {
            var i = _repository.GetById(issue);
            i.Status = IssueStatus.Revision;
            i.UpdatedAt = DateTime.UtcNow;

            
            i.StatusChanges ??= new List<IssueStatusChange>();
            i.StatusChanges.Add(new IssueStatusChange
            {
                IssueId = i.Id,
                Status = IssueStatus.Revision,
                ChangedAt = DateTime.UtcNow,
                ChangedBy = loggedUserId
            });

            _repository.Update(i);
            return i;
        }

        public object Decline(int id, int loggedUserId)
        {
            var i = _repository.GetById(id);
            if (i == null) return null;
            i.Status = IssueStatus.Declined;
            MaliciousUser(i.TouristId);
            
            i.StatusChanges ??= new List<IssueStatusChange>();
            i.StatusChanges.Add(new IssueStatusChange
            {
                IssueId = i.Id,
                Status = IssueStatus.Declined,
                ChangedAt = DateTime.UtcNow,
                ChangedBy = loggedUserId
            });
            
            _repository.Update(i);
            return i;

        }

        public object GetAllRevision(int id)
        {
            return _repository.GetAll().Where(x => x.Status == IssueStatus.Revision);
        }

        public object PendingAgain(int issue, int id)
        {
            var i = _repository.GetById(issue);
            i.Status = IssueStatus.Pending;
            i.UpdatedAt = DateTime.UtcNow;
            MaliciousUser(i.AuthorId);

            i.StatusChanges ??= new List<IssueStatusChange>();
              i.StatusChanges.Add(new IssueStatusChange
            {
                IssueId = i.Id,
                Status = IssueStatus.Pending,
                ChangedAt = DateTime.UtcNow,
                ChangedBy = id
            });

            _repository.Update(i);
            return i;
        }

        public object GetAllByTouristId(int id)
        {
            
            return _repository.GetAll().Where(i => i.TouristId == id);
        }

        private void MaliciousUser(int userId)
        {
            var u = _userRepository.GetById(userId);
            u.IssueCount += 1;
            if (u.IssueCount > 9)
            {
                u.Malicious = true;
            }

            _userRepository.Update(u);
        }
    }
}