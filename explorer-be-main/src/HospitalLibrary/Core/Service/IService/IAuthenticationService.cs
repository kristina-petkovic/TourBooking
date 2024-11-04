using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enum;

namespace HospitalLibrary.Core.Service.IService
{
    public interface IAuthenticationService
    {
        Tokens Authenticate(int userId, string name, Role r);
    }
}