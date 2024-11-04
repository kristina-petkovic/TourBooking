using System;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enum;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Core.Repository
{
    public class InterestRepository : IInterestRepository
    {
        private readonly HospitalDbContext _context;

        public InterestRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Interest> GetAll()
        {
            return _context.Interests.ToList();
        }

        public Interest GetById(int id)
        {
            return _context.Interests.Find(id);
        }

        public object Create(Interest room)
        {
            
            _context.Interests.Add(room);
            _context.SaveChanges();
            return room;
        }

        public void Update(Interest t)
        {
            _context.Entry(t).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Interest room)
        {
            var r = GetById(room.Id);
            r.Deleted = true;
            Update(r);
        }

        public void CreateMultiple(int userId, List<Interest> dtoInterests)
        {
            foreach (var i in dtoInterests)
            {
               i.Id = userId;
               _context.Interests.Add(i);
               _context.SaveChanges();
            }
           
        }
        
        public void CreateMultipleWithInterestString(int userId, IEnumerable<string> dtoInterests)
        {
            foreach (var newInterest in dtoInterests.Select(i => new Interest()
                     {
                         Deleted = false, 
                         TouristId = userId, 
                         TourId = 0,
                         InterestTypeName = (InterestType)Enum.Parse(typeof(InterestType), i)
                     }))
            {
                _context.Interests.Add(newInterest);
                _context.SaveChanges();
            }
        }

        public void CreateMultipleForTour(int tourId, IEnumerable<string> dtoInterests)
        {
            foreach (var newInterest in dtoInterests.Select(i => new Interest()
                     {
                         Deleted = false, 
                         TouristId = 0,
                         TourId = tourId, 
                         InterestTypeName = (InterestType)Enum.Parse(typeof(InterestType), i)
                     }))
            {
                _context.Interests.Add(newInterest);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Interest> GetAllByTouristId(int id)
        {
            return _context.Interests.ToList().Where(t=> t.TouristId == id && t.Deleted == false);
        }

        public IEnumerable<Interest> GetAllByTourId(int id)
        {
            return _context.Interests.ToList().Where(t=> t.TourId == id && t.Deleted == false);
        }
    }
}