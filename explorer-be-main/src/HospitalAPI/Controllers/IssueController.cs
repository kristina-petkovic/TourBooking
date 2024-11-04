using System.Threading.Tasks;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using HospitalLibrary.Core.Service.IService;
using HospitalLibrary.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService _issueService;

        public IssueController(IIssueService roomService)
        {
            _issueService = roomService;
        }

        [Authorize(Policy = "TouristPolicy")]
        [HttpPost("createissue")]
        public async Task<IActionResult> Create(IssueDTO dto)
        {
            
            return Ok(_issueService.Create(dto));
        }

        [Authorize(Policy = "AuthorPolicy")]
        [HttpGet ("author{id}")]

        public ActionResult GetAllByAuthorId(int id)
        {
            return Ok(_issueService.GetAllByAuthorId(id));
        }
        [Authorize(Policy = "TouristPolicy")]
        [HttpGet ("tourist{id}")]

        public ActionResult GetAllByTouristId(int id)
        {
            return Ok(_issueService.GetAllByTouristId(id));
        }
        
        [Authorize(Policy = "AdminPolicy")]
        [HttpGet ("admin{id}")]

        public ActionResult GetAllRevisionForAdmin(int id)
        {
            return Ok(_issueService.GetAllRevision(id));
        }

        
        [Authorize(Policy = "AuthorPolicy")]
        [HttpPost("resolve{issueId:int}/loggeduser{id}")]
        public async Task<IActionResult> Resolve(int issueId, int id)
        {
            return Ok(_issueService.Resolve(issueId, id));
        }


        [Authorize(Policy = "AuthorPolicy")]
        [HttpPost("revision{issueId}/loggeduser{id}")]
        public async Task<IActionResult> Revision(int issueId, int id)
        {
            return Ok(_issueService.Revision(issueId, id));
        }
        
        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("pending{issueId}/loggeduser{id}")]
        public async Task<IActionResult> PendingAgain(int issueId, int id)
        {
            return Ok(_issueService.PendingAgain(issueId, id));
        }
        

        [AllowAnonymous] 
        [HttpPost("decline{id:int}/loggeduser{userid}")]
        public async Task<IActionResult> Decline(int id,int userid)
        {
            return Ok(_issueService.Decline(id, userid));
        }
        
    }
}