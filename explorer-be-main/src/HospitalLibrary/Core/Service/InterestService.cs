using System;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enum;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.Core.Service.IService;

namespace HospitalLibrary.Core.Service
{
    public class InterestService: IInterestService
    {
        private readonly IInterestRepository _repository;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        
        public InterestService(IInterestRepository repository,IUserRepository userRepository, IEmailService emailService)
        {
            _repository = repository;
            _userRepository = userRepository;
            _emailService = emailService;
        }
        public InterestService(IInterestRepository repository,IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }
        public void CreateMultiple(int userId, List<Interest> dtoInterests)
        {
            _repository.CreateMultiple(userId, dtoInterests);
        }

        public void CreateMultipleWithInterestString(int userId, IEnumerable<string> dtoInterests)
        {
            _repository.CreateMultipleWithInterestString(userId, dtoInterests);
        }

        public void CreateMultipleForTour(int tourId, IEnumerable<string> dtoInterests)
        {
            _repository.CreateMultipleForTour(tourId, dtoInterests);
        }

        public object Create(Interest dto)
        {
            var allInterests = _repository.GetAllByTouristId(dto.TouristId);
            var existing = allInterests.FirstOrDefault(i => i.InterestTypeName == dto.InterestTypeName);
            if (existing is { Deleted: true })
            {
                existing.Deleted = false;
                _repository.Update(existing);
            }
            else
            {
                var i = new Interest
                {
                    Deleted = false, 
                    InterestTypeName = dto.InterestTypeName,
                    TouristId = dto.TouristId,
                    TourId = 0
                };
                return _repository.Create(i);
            }
            return existing;
        }

        public void Delete(int id)
        {
            var i = _repository.GetById(id);
            i.Deleted = true;
            _repository.Update(i);
        }

        public object GetAllByTouristId(int id)
        {
            return _repository.GetAllByTouristId(id);
        }
        
        public IEnumerable<Interest> GetAll()
        {
            return _repository.GetAll();
        }

        public List<InterestType> GetInterestNamesByTouristId(int id)
        {
            var interests = _repository.GetAllByTouristId(id);
            return interests.Select(i => i.InterestTypeName).Distinct().ToList();
        }


        public object GetAllByTourId(int id)
        {
            return _repository.GetAllByTourId(id);
        }

        public IEnumerable<int> GetToursIdsByInterestTypes(List<InterestType> interestTypes)
        {
            var tourIds = (from interest in GetAll().
                Where(x => x.TourId != 0) where interestTypes.
                Contains(interest.InterestTypeName) select interest.TourId).ToList();
            return tourIds;
        }

        public object GetTouristsIdsByInterestTypes(List<string> dtoInterests, string name)
        {
            var touristIds = (from interest in GetAll().
                Where(x => x.TouristId != 0) where dtoInterests.
                Contains(interest.InterestTypeName.ToString()) select interest.TouristId).ToList();

            foreach (var user in touristIds.Select(id => _userRepository.GetById(id)))
            {
                _emailService.NotifyTouristAboutNewTour(user, name);
            }

            return touristIds;
        }
    }
}