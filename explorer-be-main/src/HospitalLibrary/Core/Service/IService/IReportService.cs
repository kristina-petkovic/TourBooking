using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Service.IService
{
    public interface IReportService
    {
        Report CreateReport(int authorId);
        public void NoSalesInLastThreeMonths(int authorId);
        public int LastMonthTopAuthorId();
        
        Report LastReport(int authorId);
        User TopAuthorThisMonth();
    }
}