using System.Threading.Tasks;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService roomService)
        {
            _reportService = roomService;
        }
        
        
        
        
        [Authorize(Policy = "AuthorPolicy")]
        [HttpGet("report{authorId}")]
        public Report CreateReport(int authorId)
        {
            return _reportService.CreateReport(authorId);
        }
        [Authorize(Policy = "AuthorPolicy")]
        [HttpGet("get{authorId}")]
        public Report GetByAuthorId(int authorId)
        {
            return _reportService.LastReport(authorId);
        }
        
    }
}