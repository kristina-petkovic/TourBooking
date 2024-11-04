using System;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enum;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Settings
{
    public class HospitalDbContext : DbContext
    {
     
        public DbSet<User> Users { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<KeyPoint> KeyPoints { get; set; }
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
       //     modelBuilder.Entity<KeyPoint>().Property(l => l.Latitude).HasColumnType("float");
        //    modelBuilder.Entity<KeyPoint>().Property(l => l.Longitude).HasColumnType("float");

             modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "kpetkovic18@gmail.com",
                    Username = "tourist1",
                    Role = Role.Tourist,
                    Password =  BCrypt.Net.BCrypt.HashPassword("1"),
                    FirstName = "Kristina",
                    LastName = "Petkovic",
                    IssueCount = 0,
                    Malicious = false,
                    Blocked = false,
                    AuthorPoints = 0,
                    TopAuthor = false,
                    Deleted = false
                },
                new User
                {
                    Id = 2,
                    Email = "kpetkovic18@gmail.com",
                    Username = "author2",
                    Role = Role.Author,
                    Password =  BCrypt.Net.BCrypt.HashPassword("2"),
                    FirstName = "Bjanka",
                    LastName = "Tijodorovic",
                    IssueCount = 0,
                    Malicious = false,
                    Blocked = false,
                    AuthorPoints = 0,
                    TopAuthor = false,
                    Deleted = false
                },
                new User
                {
                    Id = 3,
                    Email = "kpetkovic18@gmail.com",
                    Username = "admin3",
                    Role = Role.Admin,
                    Password =  BCrypt.Net.BCrypt.HashPassword("1"),
                    FirstName = "Jelena",
                    LastName = "Petkovic",
                    IssueCount = 0,
                    Malicious = false,
                    Blocked = false,
                    AuthorPoints = 0,
                    TopAuthor = false,
                    Deleted = false
                }
             );

             modelBuilder.Entity<Tour>().HasData(new Tour()
             { 
                 Id = 1,
                Name = "tura autora broj 1",
                Description = "prva tura",
                Difficulty = TourDifficulty.Easy,
                TicketCount = 150,
                Price = 10,
                Status = TourStatus.Published,
                AuthorId = 2
             },
                 new Tour()
                 { 
                     Id = 2,
                     Name = "tura autora broj 1",
                     Description = "druga tura",
                     Difficulty = TourDifficulty.Easy,
                     TicketCount = 150,
                     Price = 10,
                     Status = TourStatus.Draft,
                     AuthorId = 2
                 },new Tour()
                 { 
                     Id = 3,
                     Name = "tura autora broj 1",
                     Description = "treca tura",
                     Difficulty = TourDifficulty.Hard,
                     TicketCount = 150,
                     Price = 10,
                     Status = TourStatus.Published,
                     AuthorId = 2
                 });

             modelBuilder.Entity<KeyPoint>().HasData(
                 new KeyPoint()
                 {
                     Id = 1,
                     Deleted = false,
                     TourId = 1,
                     Order = 1,
                     Name = "kp ture 1",
                     Description = "spomenik kulture",
                     Image = "spomenik3.jpg",
                     Latitude = 38.8951 ,
                     Longitude = -77.0364 
                 },    new KeyPoint()
                 {
                     Id = 2,
                     Deleted = false,
                     TourId = 1,
                     Order = 2,
                     Name = "kp ture 1, drugi po redu",
                     Description = "spomenik kulture",
                     Image = "spomenik3.jpg",
                     Latitude = 39.8951 ,
                     Longitude = -77.0364 
                 });
             modelBuilder.Entity<Interest>().HasData(
                 new Interest()
                 {
                     Id = 1,
                     Deleted = false,
                     TouristId = 1,
                     InterestTypeName = InterestType.food
                 },
                 new Interest()
                     {
                         Id = 2,
                         Deleted = false,
                         TouristId = 1,
                         InterestTypeName = InterestType.nature
                     },
                 new Interest()
                 {
                     Id = 3,
                     Deleted = false,
                     TourId = 1,
                     InterestTypeName = InterestType.food
                 }
             );


             modelBuilder.Entity<CartItem>().HasData(
              new CartItem()
                 {
                     Id = 1,
                     Deleted = false,
                     TourId = 1,
                     TouristId = 1,
                     Count = 1
                 });

             modelBuilder.Entity<Purchase>().HasData(new Purchase()
             {
                 Id = 1,
                 AuthorId = 2,
                 TourId = 1,
                 TourName = "tura autora broj 1",
                 TouristId = 1,
                 Price = 10,
                 PurchaseDate = new DateTime(2023, 1, 1) 

             });
            base.OnModelCreating(modelBuilder);
        }
    }
}
