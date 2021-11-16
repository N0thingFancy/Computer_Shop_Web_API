using AutoMapper;
using Kompiuteriu_Web.Auth;
using Kompiuteriu_Web.Auth.Model;
using Kompiuteriu_Web.Data.Dtos.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kompiuteriu_Web.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<RestUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenManager _tokenManager;
        public AuthController(UserManager<RestUser> userManager, IMapper mapper, ITokenManager tokenManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenManager = tokenManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var user = await _userManager.FindByNameAsync(registerUserDto.UserName);
            if (user != null)
                return BadRequest("User already exists");

            var newUser = new RestUser
            {
                Email = registerUserDto.Email,
                UserName = registerUserDto.UserName
            };
            var createUser = await _userManager.CreateAsync(newUser, registerUserDto.Password);
            if (!createUser.Succeeded)
                return BadRequest("Could not create a user");

            await _userManager.AddToRoleAsync(newUser, UserRoles.SimpleUser);
            return CreatedAtAction(nameof(Register), _mapper.Map<UserDto>(newUser));
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
                return BadRequest("User name or password is invalid");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
                return BadRequest("User name or password is invalid");

            var accessToken = await _tokenManager.CreateAccessTokenAsync(user);
            
            return Ok(new SuccessfulLoginResponseDto(accessToken));
        }
    }
}
