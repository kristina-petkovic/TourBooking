using System.Collections.Generic;
using HospitalLibrary.Core.Model.Enum;

namespace HospitalLibrary.Core.Model
{
    public class Tour: Entity
    {
       
        public string Name { get; set; }
       public string Description { get; set; }
       public TourDifficulty Difficulty { get; set; }
       public int TicketCount { get; set; }
       public int Price { get; set; }
       public TourStatus Status { get; set; }
       public int AuthorId { get; set; }
       public bool NoSalesInLastThreeMonths { get; set; }
    }
}