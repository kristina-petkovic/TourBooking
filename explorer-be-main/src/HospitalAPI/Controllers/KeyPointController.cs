using System.Threading.Tasks;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service.IService;
using HospitalLibrary.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyPointController : ControllerBase
    {
        private readonly IKeyPointService _keyPointService;

        public KeyPointController(IKeyPointService roomService)
        {
            _keyPointService = roomService;
        }
        
       
        
        [Authorize(Policy = "AuthorPolicy")]
        [HttpPost("createkeypoint")]
        public object Create(KeyPointDTO dto)
        {
            return _keyPointService.Create(dto);
        }
        
    }
}