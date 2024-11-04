using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.Core.Service.IService;
using HospitalLibrary.DTOs;

namespace HospitalLibrary.Core.Service
{
    public class KeyPointService : IKeyPointService
    {
        private readonly IKeyPointRepository _repository;

        public KeyPointService(IKeyPointRepository repository)
        {
            _repository = repository;
        }

        public object Create(KeyPointDTO dto)
        {
            var newKP = new KeyPoint
            {
                Deleted = false, 
                Order = dto.Order,
                Name = dto.Name,
                Description = dto.Description,
                Image = dto.Image,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                TourId = dto.TourId
            };

            return _repository.Create(newKP);
        }

        public object GetByTourId(int tourId)
        {
            return _repository.GetByTourId(tourId);
        }
    }
}