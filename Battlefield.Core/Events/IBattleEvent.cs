namespace Battlefield.Core.Events
{
    interface IBattleEvents : IEvent
    {
        Domain.Battle Battlefield { get; set; }
    }
}
