using Battlefield.Core.Domain;

namespace Battlefield.Core.Events.Battlefield;
public record BattlefieldCreated(Battle Battle) : IEvent;
