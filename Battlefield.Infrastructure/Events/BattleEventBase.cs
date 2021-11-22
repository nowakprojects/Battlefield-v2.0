namespace Battlefield.Infrastructure.Events
{
    public abstract record BattleEventBase(
        Core.Domain.Battle Battlefield);
        //: IBattleEvents;
}
