using AutoMapper;
using Battlefield.Core.Domain;
using Battlefield.Infrastructure.DTO;

namespace Battlefield.Infrastructure.Mappers;

public class AtuoMapperConfig
{
    public static IMapper Initializate()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Battle, BattleDto>();
            cfg.CreateMap<Tile, TileDto>();
        }).CreateMapper();
    }
}

