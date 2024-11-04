using System.Collections.Generic;
using HospitalLibrary.Core.Model;
using HospitalLibrary.DTOs;

namespace HospitalLibrary.Core.Service.IService
{
    public interface ITourService
    {
        Tour Create(TourDTO newTour);
        void Publish(int tourId);
        TourDTO GetById(int tourId);
        Tour Get(int tourId);
        void Update(Tour t);
        List<TourDTO> GetByAuthorId(int authorId);
        void Archive(int tourId);
        object GetAll();
        object FilterByStatus(string status);
        object GetAllByTopAuthors();
        List<TourDTO>  FindTours(int touristId);
        IEnumerable<TourDTO> FindRecommendedByDifficulty(int id, int difficulty);
        object? NoSales(int authorId);
    }
}