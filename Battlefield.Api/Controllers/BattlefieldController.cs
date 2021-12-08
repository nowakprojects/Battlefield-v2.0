using Battlefield.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Battlefield.Infrastructure.Repositories;
using Battlefield.Infrastructure.Commands.BattleUnit;
using Battlefield.Infrastructure.Commands.Battlefield;
using Battlefield.Infrastructure.DTO;
using AutoMapper;
using Battlefield.Infrastructure.CommandHandlers;
using Battlefield.Infrastructure.EventHandlers;

namespace Battlefield.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BattlefieldController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IBattlefieldRepository _battleRepo;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IMapper _mapper;
    private readonly ITimeTicker _timeTicker;

    public BattlefieldController(ILogger<WeatherForecastController> logger,
        IBattlefieldRepository battleRepo, ICommandDispatcher commandDispatcher,
        IMapper mapper, ITimeTicker timeTicker)
    {
        _logger = logger;
        _battleRepo = battleRepo;
        _commandDispatcher = commandDispatcher;
        _mapper = mapper;
        _timeTicker = timeTicker;
    }

    [HttpGet("")]
    public async Task<IEnumerable<BattleDto>> GetAsync()
    {
        var battles = await _battleRepo.BrowseAsync();
        return battles.Select(b => _mapper.Map<BattleDto>(b));
    }


    [HttpGet("{battleId}")]
    public async Task<IActionResult> Get(Guid battleId)
    {
        var battle = await _battleRepo.GetAsync(battleId);
        if (battle == null)
        {
            return NotFound();
        }
        return new JsonResult(_mapper.Map<BattleDto>(battle));
    }

    [HttpPost("Create")]
    public async Task<IActionResult> PostAsync([FromBody] CreateBattlefiled command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return Created("battlefield/", null);
    }
    [HttpPost("Start")]
    public async Task<IActionResult> PostAsync([FromBody] StartBattle command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    [HttpPost("StartAny")]
    public async Task<IActionResult> PostAsync()
    {
        var battle = await _battleRepo.GetAsync("name1");
        var battleId = battle.Id;
        var command = new StartBattle()
        {
            BattleId = battleId
        };
        await _commandDispatcher.DispatchAsync(command);
        return NoContent();
    }



}

