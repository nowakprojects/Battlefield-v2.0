using AutoMapper;
using Battlefield.Infrastructure.CommandHandlers;
using Battlefield.Infrastructure.Commands.BattleUnit;
using Battlefield.Infrastructure.DTO;
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
    private readonly IMapper _mapper;

    public BattleUnitController(
        ILogger<WeatherForecastController> logger,
        IBattlefieldRepository battleRepo, 
        ICommandDispatcher commandDispatcher,
        IMapper mapper)
    {
        _logger = logger;
        _battleRepo = battleRepo;
        _commandDispatcher = commandDispatcher;
        _mapper = mapper;
    }
    
    [HttpGet("{battleId}")]
    public async Task<IActionResult> GetAsync(Guid battleId)
    {
        var battle = await _battleRepo.GetAsync(battleId);
        if (battle is null)
            return NotFound();

        var unitsDto = battle.Units.Select(x => _mapper.Map<BattleUnitDto>(x));
        return new JsonResult(unitsDto);
    }
    [HttpGet("{battleId}/{unitId}")]
    public async Task<IActionResult> Get(Guid battleId, Guid unitId)
    {
        var battle = await _battleRepo.GetAsync(battleId);
        var unit = battle.Units.FirstOrDefault(x => x.Id == unitId);
        return new JsonResult(_mapper.Map<BattleUnitDto>(unit));

    }
    [HttpGet("{battleId}/{x}/{y}")]
    public async Task<IActionResult> Get(Guid battleId, int x, int y)
    {
        var battle = await _battleRepo.GetAsync(battleId);
        var unit = battle.TileMap[x,y].Unit;
        return new JsonResult(_mapper.Map<BattleUnitDto>(unit));

    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUnit command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return Created($"creature/{command}", null);
    }
    [HttpPost("{battleId}/attack")]
    public async Task<IActionResult> GiveOrder([FromBody] GiveAttackOrder command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return NoContent();
    }
}

