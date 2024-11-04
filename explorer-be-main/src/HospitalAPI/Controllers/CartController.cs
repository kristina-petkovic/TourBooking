using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Core.Service.IService;
using HospitalLibrary.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        
        
        [Authorize(Policy = "TouristPolicy")]
        [HttpPost("createtour")]
        public void AddToCart(CartDTO dto)
        {
            
            _cartService.AddToCart(dto);
        }
        
        [HttpPost("tourist{id:int}")]
        public CartDTO GetByTouristId(int id)
        {
            return  _cartService.GetByTouristId(id);
        }
        
        [Authorize(Policy = "TouristPolicy")]
        [HttpPost("deleteitem{id:int}")]
        public void DeleteCartItem(int id)
        {
            _cartService.DeleteById(id);
        }
         
        [AllowAnonymous]
        [HttpPost("decrease{id:int}")]
        public void Decrease(int id)
        {
            _cartService.Decrease(id);
        }
    }
}