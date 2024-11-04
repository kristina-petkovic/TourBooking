using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enum;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Core.Repository
{
   
    public class UserRepository : IUserRepository
    {
        private readonly HospitalDbContext _context;

        public UserRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public void Delete(User room)
        {
            var u = GetById(room.Id);
            u.Deleted = true;
            Update(u);
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            return GetAll().FirstOrDefault(u => u.Password.Equals(password) && u.Username.Equals(username));
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return user;
        }



        public User GetByUsername(object username)
        {
            return GetAll().FirstOrDefault(u =>  u.Username.Equals(username));

        }
        

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

      

        public IEnumerable<User> GetAllTopAuthors()
        {
            return _context.Users.ToList().Where(t=> t.Role == Role.Author && t.TopAuthor);
        }
    }

}