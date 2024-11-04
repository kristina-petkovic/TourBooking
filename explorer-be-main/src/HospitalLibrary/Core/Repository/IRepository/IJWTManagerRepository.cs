using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enum;

namespace HospitalLibrary.Core.Repository.IRepository
{
    public interface IJwtManagerRepository
    {
        Tokens Authenticate(int userId, string name, Role r);
    }
}