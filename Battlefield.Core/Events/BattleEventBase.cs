namespace Battlefield.Core.Events
{
    public abstract record BattleEventBase(
        Domain.Battle Battlefield);
    //: IBattleEvents;
}
