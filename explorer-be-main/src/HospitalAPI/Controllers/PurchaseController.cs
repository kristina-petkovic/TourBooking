using HospitalLibrary.Core.Service.IService;
using HospitalLibrary.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        public PurchaseController(IPurchaseService roomService)
        {
            _purchaseService = roomService;
        }
        
        [Authorize(Policy = "TouristPolicy")]
        [HttpPost("buy")]
        public void BuyToursFromCart(CartDTO dto)
        {
            _purchaseService.Buy(dto);
        }

        [AllowAnonymous]
        [HttpGet("id{id}")]
        public ActionResult GetAllByTouristId(int id)
        {
            return Ok(_purchaseService.GetAllByTouristId(id));
        }
    }
}