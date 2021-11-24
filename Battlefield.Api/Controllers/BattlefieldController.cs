using Battlefield.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Battlefield.Infrastructure.Repositories;
using Battlefield.Infrastructure.Commands;
using Battlefield.Infrastructure.Commands.BattleUnit;
using Battlefield.Infrastructure.Commands.Battlefield;

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
            return  await _battleRepo.BrowseAsync();
        }
        
        
        [HttpGet("{battleId}")]
        public async Task<IActionResult> Get(Guid battleId)
        {
            var battle = await _battleRepo.GetAsync(battleId);
            if (battle == null)
            {
                return NotFound();
            }
            return new JsonResult(battle);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateBattlefiled command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Created("battlefield/",null);
        }
        
        
        
    }
}
