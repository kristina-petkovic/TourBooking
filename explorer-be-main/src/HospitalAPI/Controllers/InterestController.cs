using System.Threading.Tasks;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enum;
using HospitalLibrary.Core.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestController : ControllerBase
    {
        private readonly IInterestService _interestService;

        public InterestController(IInterestService roomService)
        {
            _interestService = roomService;
        }
        
        
        [Authorize(Policy = "TouristPolicy")]
        [HttpPost("newUserInterest")]
        public async Task<IActionResult> Create(Interest i)
        {
            return Ok(_interestService.Create(i));
        }
        
      
        
        [Authorize(Policy = "TouristPolicy")]
        [HttpPost("delete{id}")]
        public void Delete(int id)
        {
            _interestService.Delete(id);
        }
        
        [AllowAnonymous]
        [HttpGet("tourist")]
        public ActionResult GetAllByTouristId(int id)
        {
            return Ok(_interestService.GetAllByTouristId(id));
        }
        [AllowAnonymous]
        [HttpGet("tour")]
        public ActionResult GetAllByTourId(int id)
        {
            return Ok(_interestService.GetAllByTourId(id));
        }
    }
}