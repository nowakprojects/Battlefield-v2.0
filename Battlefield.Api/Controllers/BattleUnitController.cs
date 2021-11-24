using Battlefield.Infrastructure.Commands;
using Battlefield.Infrastructure.Commands.BattleUnit;
using Battlefield.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Battlefield.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BattleUnitController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IBattlefieldRepository _battleRepo;
    private readonly ICommandDispatcher _commandDispatcher;

    public BattleUnitController(ILogger<WeatherForecastController> logger,
        IBattlefieldRepository battleRepo, ICommandDispatcher commandDispatcher)
    {
        _logger = logger;
        _battleRepo = battleRepo;
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("")]
    public async Task<IActionResult> Post([FromBody] CreateUnit command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return Created($"creature/{command}", null);
    }
}

