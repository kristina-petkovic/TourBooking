using System.Threading.Tasks;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Service.IService
{
    public interface IEmailService
    {
        Task<bool> PurchaseMail(Purchase p, User u);
        Task<bool> SendIssueEMail(Issue issue, User u);
        Task<bool>  SendBlockedMail(User user);
        Task<bool> NotifyTouristAboutNewTour(User user, string name);
    }
}