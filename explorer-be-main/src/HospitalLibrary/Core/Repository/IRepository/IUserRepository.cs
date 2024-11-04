using System.Collections.Generic;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Repository.IRepository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        public IEnumerable<User> GetAllTopAuthors();
        User GetById(int id);
        void Create(User room);
        User Update(User room);
        void Delete(User room);
        User GetByUsernameAndPassword(string username, string password);
        User GetByUsername(object username);
    }
}