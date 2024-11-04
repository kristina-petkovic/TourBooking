using System.Collections.Generic;
using HospitalLibrary.Core.Model;
using HospitalLibrary.DTOs;

namespace HospitalLibrary.Core.Repository.IRepository
{
    public interface ITourRepository
    {
        IEnumerable<Tour> GetAll();
        Tour GetById(int id);
        Tour Create(Tour room);
        void Update(Tour room);
        void Delete(Tour room);
        IEnumerable<Tour> GetByAuthorId(int authorId);
    }
}