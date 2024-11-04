using HospitalLibrary.Core.Model;
using HospitalLibrary.DTOs;

namespace HospitalLibrary.Core.Service.IService
{
    public interface IKeyPointService
    {
        object Create(KeyPointDTO newKp);
        object GetByTourId(int tourId);
    }
}