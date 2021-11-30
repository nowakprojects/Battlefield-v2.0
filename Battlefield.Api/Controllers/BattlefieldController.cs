using Battlefield.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Battlefield.Infrastructure.Repositories;
using Battlefield.Infrastructure.Commands;
using Battlefield.Infrastructure.Commands.BattleUnit;
using Battlefield.Infrastructure.Commands.Battlefield;
using Battlefield.Infrastructure.DTO;
using AutoMapper;

namespace Battlefield.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BattlefieldController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IBattlefieldRepository _battleRepo;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IMapper _mapper;

    public BattlefieldController(ILogger<WeatherForecastController> logger,
        IBattlefieldRepository battleRepo, ICommandDispatcher commandDispatcher,
        IMapper mapper)
    {
        _logger = logger;
        _battleRepo = battleRepo;
        _commandDispatcher = commandDispatcher;
        _mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IEnumerable<BattleDto>> GetAsync()
    {
        var battles = await _battleRepo.BrowseAsync();
        var battlesDto = new HashSet<BattleDto>();
        foreach (var battle in battles)
            battlesDto.Add(_mapper.Map<BattleDto>(battle));
        return battlesDto;
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

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateBattlefiled command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return Created("battlefield/", null);
    }



}

