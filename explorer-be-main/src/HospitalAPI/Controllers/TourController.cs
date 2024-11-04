using System.Collections.Generic;
using System.Linq;
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
    public class TourController : ControllerBase
    {
        private readonly ITourService _service;
        private readonly IInterestService _interestService;
        private readonly IKeyPointService _keyPointService;
        public TourController(ITourService roomService, 
            IKeyPointService keyPointService, IInterestService interestService)
        {
            _service = roomService;
            _keyPointService = keyPointService;
            _interestService = interestService;
        }
        
       
        [Authorize(Policy = "AuthorPolicy")]
        [HttpPost("createtour")]
        public async Task<IActionResult> Create(TourDTO dto)
        {
            
            Tour t = _service.Create(dto);
            _interestService.CreateMultipleForTour(t.Id, dto.Interests);
            var touristIdsWithSameInterests = _interestService.GetTouristsIdsByInterestTypes(dto.Interests, dto.Name);
            return Ok(t);
        }
        
        [Authorize(Policy = "AuthorPolicy")]
        [HttpPost("publish{tourId:int}")]
        public async Task<IActionResult> Publish(int tourId)
        {
            var kp = (IEnumerable<KeyPoint>)_keyPointService.GetByTourId(tourId);
            if (kp.Count() <= 1)
            {
                return BadRequest("The tour cannot be published because the associated items are not sufficient.");
            }
            _service.Publish(tourId);
            return Ok();
        }
        
        [Authorize(Policy = "AuthorPolicy")]
        [HttpPost("archive{tourId:int}")]
        public async Task<IActionResult> Archive(int tourId)
        {
            _service.Archive(tourId);
            return Ok();
        }


        
        [HttpPost("get{tourId:int}")]
        public ActionResult<TourDTO> GetById(int tourId)
        {
           return  _service.GetById(tourId);
        }

        [Authorize(Policy = "AuthorPolicy")]
        [HttpPost("author{authorId:int}")]
        public List<TourDTO> GetByAuthorId(int authorId)
        {
            return  _service.GetByAuthorId(authorId);
        }
        
        
        
        
        
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetAllPublished()
        {
            return Ok(_service.GetAll());
        }
        
        
        [AllowAnonymous]
        [HttpGet("recommend{touristId:int}")]
        public ActionResult RecommendedTours(int touristId)
        {
            return Ok(_service.FindTours(touristId));
        }
        
        
        [AllowAnonymous]
        [HttpGet("nosalestours/{authorId:int}")]
        public ActionResult NoSales(int authorId)
        {
            return Ok(_service.NoSales(authorId));
        }

        
        [Authorize(Policy = "TouristPolicy")]
        [HttpGet("recommend{touristId:int}/difficulty{difficulty:int}")]
        public ActionResult RecommendedToursByDifficulty(int touristId, int difficulty)
        {
            return Ok(_service.FindRecommendedByDifficulty(touristId,difficulty));
        }

        [AllowAnonymous]
        [HttpGet("allbytop")]
        public ActionResult GetAllByTopAuthors()
        {
            return Ok(_service.GetAllByTopAuthors());
        }
        
        
        [AllowAnonymous]
        [HttpGet("filter")]
        public ActionResult<List<TourDTO>> FilterByStatus(string status)
        {
            var result = _service.FilterByStatus(status);
            return Ok(result);
        }
        
    }
}