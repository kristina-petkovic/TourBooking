using System;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enum;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.Core.Service.IService;
using HospitalLibrary.DTOs;

namespace HospitalLibrary.Core.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IAuthenticationService _authenticationService;

        public UserService(IUserRepository userRepository, 
            IAuthenticationService authenticationService,
            IEmailService emailService)
        {
            _emailService = emailService;
            _authenticationService = authenticationService;
            _userRepository = userRepository;
        }

        public bool ExistsByUsername(string username)
        {
            var users = _userRepository.GetAll().ToList();
            return users.Any(user => user.Username != null && user.Username == username);
        }

        public void Create(RegistrationDto dto)
        {
             
            var newUser = new User
            {
                Username = dto.Username,
                Role = Role.Tourist,
                Malicious = false,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                TopAuthor = false,
                AuthorPoints = 0,
                Blocked = false,
                IssueCount = 0,
                Deleted = false

            };

            _userRepository.Create(newUser);
        }
        

        public User GetByUsernameAndPassword(string username, string password)
        {
            return _userRepository.GetByUsernameAndPassword(username, password);
        }

        public IEnumerable<User> GetAllTopAuthors()
        {
            return _userRepository.GetAllTopAuthors();
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public object Block(int userId)
        {
            var u = _userRepository.GetById(userId);
            u.Blocked = true;
            _emailService.SendBlockedMail(u);
            return _userRepository.Update(u);
        }

        public object Unblock(int userId)
        {
            var u = _userRepository.GetById(userId);
            u.Blocked = false;
            return _userRepository.Update(u);
        }

        public object GetById(int userId)
        {
            return _userRepository.GetById(userId);
        }

        public object AuthenticateUser(AuthenticationDto authenticationDto)
        {
            var user = GetByUsername(authenticationDto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(authenticationDto.Password, user.Password))
            {
                throw new Exception("Username or password is incorrect");
            }

            
            if (user.Blocked)
            {
                throw new Exception("User blocked");
            }
            
            var token = _authenticationService.Authenticate(user.Id, user.FirstName, user.Role);

            var role = user.Role switch
            {
                Role.Tourist => 0,
                Role.Author => 1,
                _ => 2
            };

            var dto = new AuthenticatedUserDto
            {
                Username = user.Username,
                Id = user.Id,
                Token = token.Token,
                Role = role,
            };
            return dto;
        }


        public User GetByUsername(string authenticationDtoUsername)
        {
            return _userRepository.GetByUsername(authenticationDtoUsername);
        }
    }
}