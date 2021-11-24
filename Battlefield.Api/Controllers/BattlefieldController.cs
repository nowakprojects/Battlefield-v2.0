using Battlefield.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Battlefield.Infrastructure.Repositories;
using Battlefield.Infrastructure.Commands;
using Battlefield.Infrastructure.Commands.BattleUnit;

namespace Battlefield.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BattlefieldController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IBattlefieldRepository _battleRepo;
        private readonly ICommandDispatcher _commandDispatcher;

        public BattlefieldController(ILogger<WeatherForecastController> logger,
            IBattlefieldRepository battleRepo, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _battleRepo = battleRepo;
            _commandDispatcher = commandDispatcher;
        }
        [HttpGet]
        public async Task<IEnumerable<Battle>> GetAsync()
        {
            return await _battleRepo.BrowseAsync();
        }
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] CreateUnit command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Created($"battlefield/{command}", null);
        }

    }
}
