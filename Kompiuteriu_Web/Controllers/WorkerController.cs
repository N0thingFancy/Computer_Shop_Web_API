using AutoMapper;
using Kompiuteriu_Web.Auth.Model;
using Kompiuteriu_Web.Data.Dtos.Workers;
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
    [Route("api/shops/{shopId}/workers")]
    public class WorkersController : ControllerBase
    {
        private readonly IWorkersRepository _workersRepository;
        private readonly IMapper _mapper;
        private readonly IShopsRepository _shopsRepository;
        private readonly IAuthorizationService _authorizationService;
        public WorkersController(IWorkersRepository workersRepository, IMapper mapper, IShopsRepository shopsRepository, IAuthorizationService authorizationService)
        {
            _workersRepository = workersRepository;
            _mapper = mapper;
            _shopsRepository = shopsRepository;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IEnumerable<WorkerDto>> GetAllAsync(int shopId)
        {
            var workers = await _workersRepository.GetAllAsync(shopId);
            return workers.Select(worker => _mapper.Map<WorkerDto>(worker));
        }

        [HttpGet("{workerId}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<WorkerDto>> GetAsync(int shopId, int workerId)
        {
            var shop = await _shopsRepository.GetAsync(shopId);
            if (shop == null) return NotFound($"Shop with id '{shopId}' was not found");

            var worker = await _workersRepository.GetAsync(shopId, workerId);
            if (worker == null) return NotFound($"Worker with id '{workerId}' was not found");

            return Ok(_mapper.Map<WorkerDto>(worker));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<WorkerDto>> PostAsync(CreateWorkerDto workerDto, int shopId)
        {
            var authorizationResult = User.Identity.IsAuthenticated;
            if (!authorizationResult)
            {
                return Unauthorized("Need to be logged in to do this");
            }

            var shop = await _shopsRepository.GetAsync(shopId);
            if (shop == null) return NotFound($"Shop with id '{shopId}' was not found");

            var worker = _mapper.Map<Worker>(workerDto);
            worker.ShopId = shopId;

            worker.User_id_saved = User.FindFirst(CustomClaims.UserId).Value;

            await _workersRepository.InsertAsync(worker);

            return Created($"/api/shops/{shopId}/workers/{worker.id}", _mapper.Map<WorkerDto>(worker));
        }

        [HttpPut("{workerId}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<WorkerDto>> PutAsync(int shopId, int workerId, UpdateWorkerDto workerDto)
        {
            var shop = await _shopsRepository.GetAsync(shopId);
            if (shop == null) return NotFound($"Shop with id '{shopId}' was not found");

            var oldWorker = await _workersRepository.GetAsync(shopId, workerId);
            if (oldWorker == null)
                return NotFound($"Worker with id '{workerId}' was not found");

            _mapper.Map(workerDto, oldWorker);

            await _workersRepository.UpdateAsync(oldWorker);

            return Ok(_mapper.Map<WorkerDto>(oldWorker));
        }

        [HttpDelete("{workerId}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> DeleteAsync(int shopId, int workerId)
        {
            var shop = await _shopsRepository.GetAsync(shopId);
            if (shop == null) return NotFound($"Shop with id '{shopId}' was not found");

            var worker = await _workersRepository.GetAsync(shopId, workerId);
            if (worker == null) return NotFound($"Worker with id '{workerId}' was not found");

            await _workersRepository.DeleteAsync(worker);

            return NoContent();
        }
    }
}
