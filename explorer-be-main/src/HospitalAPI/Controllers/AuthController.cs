using System;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enum;
using HospitalLibrary.Core.Service;
using HospitalLibrary.Core.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate(AuthenticationDto authenticationDto)
        {
            
            try
            {
                var authenticatedUser = _userService.AuthenticateUser(authenticationDto);
                return Ok(authenticatedUser);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
           
        }
    }
}