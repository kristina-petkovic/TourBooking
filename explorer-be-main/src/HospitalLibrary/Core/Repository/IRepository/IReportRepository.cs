using System.Collections.Generic;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Repository.IRepository
{
    public interface IReportRepository
    {
        IEnumerable<Report> GetAll();
        IEnumerable<Report> GetAllByAuthorId(int id);
        Report GetById(int id);
        Report Create(Report room);
        void Update(Report room);
        void Delete(Report room);
    }
}