using AutoMapper;
using Kompiuteriu_Web.Auth.Model;
using Kompiuteriu_Web.Data.Dtos.Computers;
using Kompiuteriu_Web.Data.Entities;
using Kompiuteriu_Web.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kompiuteriu_Web.Controllers
{

    /*
     *  computer
        /api/computer/ GET ALL 200
        /api/computer/{id} GET 200
        /api/computer POST 201
        /api/computer/{id} PUT 200
        /api/topic/{id} DELETE 200/204

     */
    [ApiController]
    [Route("api/shops/{shopId}/computer")]
    public class ComputersController : ControllerBase
    {
        private readonly IComputersRepository _computersRepository;
        private readonly IMapper _mapper;
        private readonly IShopsRepository _shopsRepository;
        private readonly IAuthorizationService _authorizationService;
        public ComputersController(IComputersRepository computersRepository, IMapper mapper, IShopsRepository shopsRepository, IAuthorizationService authorizationService)
        {
            _computersRepository = computersRepository;
            _mapper = mapper;
            _shopsRepository = shopsRepository;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IEnumerable<ComputerDto>> GetAllAsync(int shopId)
        {
            var computers = await _computersRepository.GetAllAsync(shopId);
            return computers.Select(computer => _mapper.Map<ComputerDto>(computer));
        }

        [HttpGet("{computerId}")]
        public async Task<ActionResult<ComputerDto>> GetAsync(int shopId, int computerId)
        {
            var shop = await _shopsRepository.GetAsync(shopId);
            if (shop == null) return NotFound($"Shop with id '{shopId}' was not found");

            var computer = await _computersRepository.GetAsync(shopId, computerId);
            if (computer == null) return NotFound($"Computer with id {computerId} was not found");

            return Ok(_mapper.Map<ComputerDto>(computer));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.SimpleUser)]
        public async Task<ActionResult<ComputerDto>> PostAsync(CreateComputerDto computerDto, int shopId)
        {
            var authorizationResult = User.Identity.IsAuthenticated;
            if (!authorizationResult)
            {
                return Unauthorized("Need to be logged in to do this");
            }

            var shop = await _shopsRepository.GetAsync(shopId);
            if (shop == null) return NotFound($"Shop with id '{shopId}' was not found");

            var computer = _mapper.Map<Computer>(computerDto);
            computer.ShopId = shopId;
            computer.User_id_saved = User.FindFirst(CustomClaims.UserId).Value;

            await _computersRepository.InsertAsync(computer);

            return Created($"/api/shops/{shopId}/computers/{computer.id}", _mapper.Map<ComputerDto>(computer));
        }

        [HttpPut("{computerId}")]
        [Authorize(Roles = UserRoles.SimpleUser)]
        public async Task<ActionResult<ComputerDto>> PutAsync(int shopId, int computerId, UpdateComputerDto computerDto)
        {
            var shop = await _shopsRepository.GetAsync(shopId);
            if (shop == null) return NotFound($"Shop with id '{shopId}' was not found");

            var oldComputer = await _computersRepository.GetAsync(shopId, computerId);
            if (oldComputer == null)
                return NotFound($"Computer with id '{computerId}' was not found");

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, oldComputer, PolicyNames.SameUser);

            if (!authorizationResult.Succeeded)
            {
                //403
                return Forbid();
            }

            _mapper.Map(computerDto, oldComputer);

            await _computersRepository.UpdateAsync(oldComputer);

            return Ok(_mapper.Map<ComputerDto>(oldComputer));
        }

        [HttpDelete("{computerId}")]
        [Authorize(Roles = UserRoles.SimpleUser)]
        public async Task<ActionResult> DeleteAsync(int shopId, int computerId)
        {
            var shop = await _shopsRepository.GetAsync(shopId);
            if (shop == null) return NotFound($"Shop with id '{shopId}' was not found");

            var computer = await _computersRepository.GetAsync(shopId, computerId);
            if (computer == null) return NotFound($"Computer with id '{computerId}' was not found");

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, computer, PolicyNames.SameUser);

            if (!authorizationResult.Succeeded)
            {
                //403
                return Forbid();
            }

            await _computersRepository.DeleteAsync(computer);

            return NoContent();
        }
    }
}
