using Battlefield.Core.Domain;

namespace Battlefield.Infrastructure.DTO;
public record class TileDto(
    Guid UnitId,
    bool Blocked,
    Coordinates Coordinates);

