using Battlefield.Core.Domain.Creatures;

namespace Battlefield.Core.Extensions;
public static class Extensions
{
    public static ICreature ConvertStringToCreature(this string name)
    {
        var type = typeof(ICreature).Assembly
                        .GetTypes()
                        .FirstOrDefault(x => x.Name == name);

        if (type is null || !typeof(ICreature).IsAssignableFrom(type))
        {
            throw new Exception($"invalid creatureType: '{name}'");
        }
        var unitType = Activator.CreateInstance(type) as ICreature;
        if(unitType is null)
        {
            throw new Exception($"cannot create instance of: '{name}'");
        }
        return unitType;
    }
}