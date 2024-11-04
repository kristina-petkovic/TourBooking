using System.Threading.Tasks;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enum;
using HospitalLibrary.Core.Service.IService;
using HospitalLibrary.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IInterestService _interestService;

        public UserController(IUserService roomService, IInterestService interestService)
        {
            _userService = roomService;
            _interestService = interestService;
        }
        
        
        [AllowAnonymous]
        [HttpPost("userRegistration")]
        public async Task<IActionResult> Register(RegistrationDto dto)
        {

            if (_userService.ExistsByUsername(dto.Username))
                return BadRequest(
                    "Username are already taken.");
           
           
            _userService.Create(dto);
            var user = _userService.GetByUsernameAndPassword(dto.Username, dto.Password);

            _interestService.CreateMultipleWithInterestString(user.Id, dto.Interests);
            return Ok(user);
        }
        
        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("block{userId:int}")]
        public async Task<IActionResult> Block(int userId)
        {
            return Ok( _userService.Block(userId));
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("unblock{userId:int}")]
        public async Task<IActionResult> Unblock(int userId)
        {
            return Ok( _userService.Unblock(userId));
        }
        
        
        [AllowAnonymous]
        [HttpGet("get{userId:int}")]
        public object GetById(int userId)
        {
            return Ok( _userService.GetById(userId));
        }
    }
}