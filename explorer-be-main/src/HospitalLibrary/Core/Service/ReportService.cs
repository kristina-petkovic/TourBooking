using System;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.Core.Service.IService;

namespace HospitalLibrary.Core.Service
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _repository;
        private readonly ITourRepository _tourRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPurchaseRepository _purchaseRepository;

        public ReportService(IReportRepository repository, 
            ITourRepository tourRepository,
            IPurchaseRepository purchaseRepository,
            IUserRepository userRepository)
        {
            _repository = repository;
            _tourRepository = tourRepository;
            _purchaseRepository = purchaseRepository;
            _userRepository = userRepository;
        }
        
        
        public Report CreateReport(int authorId)
        {
            var report = new Report{AuthorId = authorId, Date = DateTime.Now};

            var allPurchasesFromLastMonth = _purchaseRepository.GetAllByAuthorId(authorId).Where(p => p.PurchaseDate >= DateTime.Now.AddDays(-30))
                .ToList();

            
            var totalPurchaseSum = allPurchasesFromLastMonth.Sum(p => p.Count * p.Price);
            
            var (topTourId, topPurchaseCount) = TopSellingTour(allPurchasesFromLastMonth);
            var (leastTourId, leastPurchaseCount) = LeastSellingTour(allPurchasesFromLastMonth);
            
            
         
            report.SoldToursCount = allPurchasesFromLastMonth.Count;
            report.TotalProfit = totalPurchaseSum;
            report.TopSellingTourId = topTourId;
            report.TopSellingTourCount = topPurchaseCount;
            report.LeastSellingTourId = leastTourId;
            report.LeastSellingTourCount = leastPurchaseCount;
            
            
            var lastReport = LastReport(authorId);
            if (lastReport is { TotalProfit: > 0 })
            {
                var increase = (totalPurchaseSum - lastReport.TotalProfit) / lastReport.TotalProfit * 100;
                report.SalesIncreasePercentage = increase.ToString("F2") + "%"; 
            }
            else
            {
                report.SalesIncreasePercentage = "N/A";
            }
            
            
            
            

            return _repository.Create(report);
        }

        public User TopAuthorThisMonth()
        {
            var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var topAuthorId = LastMonthTopAuthorId();
            var author = _userRepository.GetById(topAuthorId);
            if (author == null) return null;
            author.AuthorPoints += 1;
            if (author.AuthorPoints > 4)
            {
                author.TopAuthor = true;
            }

            _userRepository.Update(author);

            return author;
        }

        public int LastMonthTopAuthorId()
        {
            return _repository.GetAll().Where(p => p.Date >= DateTime.Now.AddDays(-30)).ToList().MaxBy(r => r.SoldToursCount)!.AuthorId;;
        }


        private (int TourId, int TotalTourCount) TopSellingTour(IEnumerable<Purchase> purchasesFromLastMonth)
        {
            var purchasesGroupedByTour = purchasesFromLastMonth
                .GroupBy(p => p.TourId)
                .Select(g => new 
                { 
                    TourId = g.Key, 
                    TotalTourCount = g.Sum(p => p.Count) 
                })
                .ToList();

            var topSellingTour = purchasesGroupedByTour.MaxBy(g => g.TotalTourCount);
    
            return topSellingTour != null ? (topSellingTour.TourId, topSellingTour.TotalTourCount) : (0, 0);
        }


        private (int TourId, int TotalTourCount) LeastSellingTour(IEnumerable<Purchase> purchasesFromLastMonth)
        {
            var purchasesGroupedByTour = purchasesFromLastMonth
                .GroupBy(p => p.TourId)
                .Select(g => new 
                { 
                    TourId = g.Key, 
                    TotalTourCount = g.Sum(p => p.Count) 
                })
                .ToList();

            var leastSellingTour = purchasesGroupedByTour.MinBy(g => g.TotalTourCount);

            return leastSellingTour != null ? (leastSellingTour.TourId, leastSellingTour.TotalTourCount) : (0, 0);
        }

        public void NoSalesInLastThreeMonths(int authorId)
        {
            
            var allPurchasesFromLastThreeMonths = _purchaseRepository
                .GetAllByAuthorId(authorId)
                .Where(p => p.PurchaseDate >= DateTime.Now.AddMonths(-3))
                .ToList();

             var soldTourIds = allPurchasesFromLastThreeMonths
                .Select(p => p.TourId)
                .Distinct()
                .ToHashSet(); 

            
            var allTours = _tourRepository.GetByAuthorId(authorId).ToList();

            
            var noSalesTours = allTours
                .Where(tour => !soldTourIds.Contains(tour.Id))
                .ToList();
            
            var soldTours = allTours
                .Where(tour => soldTourIds.Contains(tour.Id))
                .ToList();
            foreach (var tour in noSalesTours)
            {
                tour.NoSalesInLastThreeMonths = true;
                _tourRepository.Update(tour);
            }
            foreach (var tour in soldTours)
            {
                tour.NoSalesInLastThreeMonths = false;
                _tourRepository.Update(tour);
            }
        }

      

        public Report LastReport(int authorId)
        {
            return _repository.GetAllByAuthorId(authorId).MaxBy(r => r.Date); 
        }
    }
}