using AutoMapper;
using Kompiuteriu_Web.Auth.Model;
using Kompiuteriu_Web.Data.Dtos.Auth;
using Kompiuteriu_Web.Data.Dtos.Shops;
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
    [ApiController]
    [Route("api/shops")]
    public class ShopsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IShopsRepository _shopsRepository;

        private readonly IAuthorizationService _authorizationService;

        public ShopsController(IMapper mapper, IShopsRepository shopsRepository, IAuthorizationService authorizationService)
        {
            _mapper = mapper;
            _shopsRepository = shopsRepository;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IEnumerable<ShopDto>> GetAllAsync()
        {
            var shops = await _shopsRepository.GetAllAsync();
            return shops.Select(shop => _mapper.Map<ShopDto>(shop));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShopDto>> GetAsync(int id)
        {
            var shop = await _shopsRepository.GetAsync(id);
            if (shop == null) return NotFound($"Shop with id '{id}' was not found");

            return Ok(_mapper.Map<ShopDto>(shop));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.SimpleUser)]
        public async Task<ActionResult<ShopDto>> PostAsync(CreateShopDto shopDto, int shopId)
        {
            var authorizationResult = User.Identity.IsAuthenticated;
            if (!authorizationResult)
            {
                return Unauthorized("Need to be logged in to do this");
            }

            var shop = _mapper.Map<Shop>(shopDto);

            shop.User_id_saved = User.FindFirst(CustomClaims.UserId).Value;
            await _shopsRepository.InsertAsync(shop);

            return Created($"/api/shops/{shopId}", _mapper.Map<ShopDto>(shop));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<ShopDto>> PutAsync(int id, UpdateShopDto shopDto)
        {
            var oldShop = await _shopsRepository.GetAsync(id);
            if (oldShop == null) return NotFound($"Shop with id '{id}' was not found");

            //var authorizationResult = await _authorizationService.AuthorizeAsync(User, oldShop, PolicyNames.SameUser);

            //if (!authorizationResult.Succeeded)
            //{
            //    //403
            //    return Forbid();
            //}

            _mapper.Map(shopDto, oldShop);

            await _shopsRepository.UpdateAsync(oldShop);

            return Ok(_mapper.Map<ShopDto>(oldShop));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var shop = await _shopsRepository.GetAsync(id);
            if (shop == null) return NotFound($"Shop with id '{id}' was not found");

            await _shopsRepository.DeleteAsync(shop);

            return NoContent();
        }
    }
}
