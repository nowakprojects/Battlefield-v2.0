using Battlefield.Core.Domain.Orders;

namespace Battlefield.Core.Events.BattleUnit;
public record UnitOrderChanged(Guid UnitId,IOrder Order) : IEvent;