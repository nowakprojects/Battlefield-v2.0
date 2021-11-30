using Battlefield.Core.Domain;

namespace Battlefield.Infrastructure.DTO;

public record BattleUnitDto(
    Guid Id,
    string TypeName,
    Coordinates Position,
    TileSize Size);
