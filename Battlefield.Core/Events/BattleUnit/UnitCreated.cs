using Battlefield.Core.Domain;

namespace Battlefield.Core.Events.BattleUnit;
public record UnitCreated(Guid UnitId, Guid BattleId, Coordinates Coordinates, string UnitType, Player Owner) : IEvent;
