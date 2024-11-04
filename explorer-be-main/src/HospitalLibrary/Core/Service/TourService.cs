using System;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enum;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.Core.Service.IService;
using HospitalLibrary.DTOs;

namespace HospitalLibrary.Core.Service
{
    public class TourService : ITourService
    {
        private readonly ITourRepository _repository;
        private readonly IKeyPointRepository _kprepository;
        private readonly IUserService _userService;
        private readonly IReportService _reportService;
        private readonly IInterestService _interestService;


        public TourService(ITourRepository repository, IKeyPointRepository kprepository,
            IReportService rs, IUserService userService, IInterestService interestService)
        {
            _repository = repository;
            _userService = userService;
            _kprepository = kprepository;
            _reportService = rs;
            _interestService = interestService;
        }
        public Tour Create(TourDTO dto)
        {
            var difficulty = dto.Difficult switch
            {
                "hard" => TourDifficulty.Hard,
                "medium" => TourDifficulty.Medium,
                _ => TourDifficulty.Easy
            };

            var newTour = new Tour
            {
                Deleted = false, 
                Name = dto.Name,
                TicketCount = dto.TicketCount,
                Description = dto.Description,
                Difficulty = difficulty,
                AuthorId = dto.AuthorId,
                Price = dto.Price,
                Status = TourStatus.Draft,
                NoSalesInLastThreeMonths = true
            };
            
            return _repository.Create(newTour);
        }

        public void Publish(int tourId)
        {
            if (_kprepository.GetByTourId(tourId).Count() <= 1) return ;
            var t = _repository.GetById(tourId);
            t.Status = TourStatus.Published;
            _repository.Update(t);
            
        }

        public TourDTO GetById(int tourId)
        {
            var t = _repository.GetById(tourId);
            return t.Deleted ? null : Map(t);
        }

        public Tour Get(int tourId)
        {
            return _repository.GetById(tourId);
        }

        public void Update(Tour t)
        {
           _repository.Update(t);
        }

        private TourDTO Map(Tour t)
        {
            var kp = (List<KeyPoint>)_kprepository.GetByTourId(t.Id);
            kp = kp.OrderBy(k => k.Order).ToList();
            var dto = new TourDTO()
            {
                Name = t.Name,
                Description = t.Description,
                Difficulty = t.Difficulty,
                AuthorId = t.AuthorId,
                Price = t.Price,
                NoSalesInLastThreeMonths = t.NoSalesInLastThreeMonths,
                TicketCount = t.TicketCount,
                Id = t.Id,
                Status = t.Status,
                KeyPoints = kp
            };
            
            return dto;
        }
        
        private List<TourDTO> MapList(IEnumerable<Tour> tours)
        {
            return tours.Select(tour => Map(tour)).ToList();
        }

        public List<TourDTO> GetByAuthorId(int authorId)
        {
            _reportService.NoSalesInLastThreeMonths(authorId);
            return _repository.GetByAuthorId(authorId).Select(t => Map(t)).ToList();
        }

        public void Archive(int tourId)
        {
            var t = _repository.GetById(tourId);
            t.Status = TourStatus.Archived;
            _repository.Update(t);

        }

        public object GetAll()
        {
            return MapList(_repository.GetAll().Where(x=>x.Status == TourStatus.Published));
        }

        public object FilterByStatus(string status)
        {
            return MapList(_repository.GetAll())
                .Where(t => t.Status.ToString().ToLower().Equals(status.ToLower()));
        }

        public object GetAllByTopAuthors()
        {
            var topAuthors = _userService.GetAllTopAuthors();
            var topAuthorIds = topAuthors.Select(a => a.Id).ToList(); 

            var allTours = _repository.GetAll().Where(t => t.Status == TourStatus.Published);
            var topAuthorTours = allTours.Where(tour => topAuthorIds.Contains(tour.AuthorId)).ToList();
            return MapList(topAuthorTours);
        }

        public List<TourDTO> FindTours(int touristId)
        {

            var interestTypes = _interestService.GetInterestNamesByTouristId(touristId);
            var toursIdsWithMatchingInterest = _interestService.GetToursIdsByInterestTypes(interestTypes);
            
            var tours = _repository.GetAll().Where(tour => toursIdsWithMatchingInterest.Contains(tour.Id) && tour.Status == TourStatus.Published).ToList();
    
            return MapList(tours);
        }

        public IEnumerable<TourDTO> FindRecommendedByDifficulty(int id, int difficulty)
        {
            var d = difficulty switch
            {
                1 => TourDifficulty.Medium,
                2 => TourDifficulty.Hard,
                _ => TourDifficulty.Easy
            };

            return FindTours(id).Where(t => t.Difficulty == d);
        }

        public object NoSales(int authorId)
        {
            return GetByAuthorId(authorId).Where(x => x.NoSalesInLastThreeMonths && x.Status
            != TourStatus.Archived);
        }
    }
}