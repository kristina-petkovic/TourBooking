using System;

namespace HospitalLibrary.Core.Model
{
    public class Report : Entity
    {
        
        public int AuthorId { get; set; }
        public DateTime Date { get; set; }
        public int SoldToursCount { get; set; }
        public double TotalProfit { get; set; }
        public string SalesIncreasePercentage  { get; set; }
        
        public int TopSellingTourId { get; set; }
        public int TopSellingTourCount { get; set; }        

        public int LeastSellingTourId { get; set; }
        public int LeastSellingTourCount { get; set; }       
        

    }
}