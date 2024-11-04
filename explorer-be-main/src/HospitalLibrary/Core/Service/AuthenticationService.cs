using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enum;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.Core.Service.IService;

namespace HospitalLibrary.Core.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtManagerRepository _jWtManager;

        public AuthenticationService(IJwtManagerRepository jWtManager)
        {
            _jWtManager = jWtManager;
        }

        public Tokens Authenticate(int userId, string name, Role r)
        {
            return _jWtManager.Authenticate(userId, name, r);
        }
    }
}