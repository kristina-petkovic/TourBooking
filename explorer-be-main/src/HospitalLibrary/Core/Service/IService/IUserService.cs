
using System.Collections.Generic;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.DTOs;

namespace HospitalLibrary.Core.Service.IService
{
    public interface IUserService
    {
        User GetByUsername(string authenticationDtoUsername);
        bool ExistsByUsername(string newUserUsername);
        void Create(RegistrationDto newUser);
        User GetByUsernameAndPassword(string newUserUsername, string newUserPassword);
        public IEnumerable<User> GetAllTopAuthors();
        public IEnumerable<User> GetAll();
        object Block(int userId);
        object Unblock(int userId);
        object GetById(int userId);
        object? AuthenticateUser(AuthenticationDto authenticationDto);
    }
}